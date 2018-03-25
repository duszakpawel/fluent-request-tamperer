using Fiddler;
using NLog;
using RequestObfuscator.Api;
using RequestObfuscator.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RequestObfuscator
{
    public sealed class RequestObfuscator
    {
        private static readonly List<SessionStateHandler> _delegates = new List<SessionStateHandler>();
        private static NLog.Logger Logger;

        private RequestObfuscator() { }

        public static RequestObfuscator Instance
        {
            get
            {
                return RequestObfuscatorPool.Instance;
            }
            set
            {
            }
        }

        private class RequestObfuscatorPool
        {
            static RequestObfuscatorPool() { }

            internal static readonly RequestObfuscator Instance = new RequestObfuscator();
        }

        public static RequestObfuscator operator +(RequestObfuscator leftOperand, SessionStateHandler rightOperand)
        {
            FiddlerApplication.BeforeRequest += rightOperand;

            _delegates.Add(rightOperand);

            return leftOperand;
        }

        public static RequestObfuscator operator -(RequestObfuscator leftOperand, SessionStateHandler rightOperand)
        {
            _delegates.Remove(rightOperand);

            return leftOperand;
        }

        public void Start()
        {
            LoggerConfig.Init();
            Logger = LogManager.GetCurrentClassLogger();

            Logger.Info("Starting request obfuscator...");

            FiddlerApplication.Shutdown();

            InstallCertificate();

            var type = typeof(IApiClient);

            var apiClients = AppDomain.CurrentDomain
                                 .GetAssemblies()
                                 .SelectMany(s => s.GetTypes())
                                 .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);

            foreach (var api in apiClients)
            {
                var apiClient = Activator.CreateInstance(api) as IApiClient;
                var apiBuilder = new ApiBuilder();

                apiClient.Configure(apiBuilder);

                foreach (var handler in apiBuilder.Build())
                {
                    Instance += session => handler(session);
                }
            }

            const FiddlerCoreStartupFlags flags = FiddlerCoreStartupFlags.AllowRemoteClients | FiddlerCoreStartupFlags.CaptureLocalhostTraffic | FiddlerCoreStartupFlags.DecryptSSL | FiddlerCoreStartupFlags.MonitorAllConnections;
            const short proxyPort = 8888;

            FiddlerApplication.Startup(proxyPort, flags);

            Logger.Info("Started.");
        }

        public void Stop()
        {
            Logger.Info("Stopping request obfuscator...");

            foreach (var @delegate in _delegates)
            {
                Instance -= @delegate;
            }

            if (FiddlerApplication.IsStarted())
            {
                FiddlerApplication.Shutdown();
            }

            UninstallCertificate();

            _delegates.Clear();

            Logger.Info("Stopped.");
        }

        private bool InstallCertificate()
        {
            if (!CertMaker.rootCertExists())
            {
                if (!CertMaker.createRootCert())
                {
                    return false;
                }
            }

            if (!CertMaker.rootCertIsTrusted())
            {
                if (!CertMaker.trustRootCert())
                {
                    return false;
                }
            }

            return true;
        }

        private static bool UninstallCertificate()
        {
            if (CertMaker.rootCertExists())
            {
                if (!CertMaker.removeFiddlerGeneratedCerts(true))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
