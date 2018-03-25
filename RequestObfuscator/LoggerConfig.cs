using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace RequestObfuscator
{
    public static class LoggerConfig
    {
        public static void Init()
        {
            var config = new LoggingConfiguration();

            var layout = new SimpleLayout
            {
                Text = "${date:format=T} | ${level} | ${message:withException=true}"
            };

            var target = new FileTarget
            {
                FileName = $"{nameof(RequestObfuscator)}.txt",
                Layout = layout
            };

            config.AddTarget("logfile", target);

            var rule = new LoggingRule("*", LogLevel.Debug, target);

            config.LoggingRules.Add(rule);

            var consoleTarget = new ColoredConsoleTarget
            {
                Layout = layout
            };

            config.AddTarget("console", consoleTarget);

            var consoleRule = new LoggingRule("*", LogLevel.Debug, consoleTarget);

            config.LoggingRules.Add(consoleRule);

            LogManager.Configuration = config;
        }
    }
}
