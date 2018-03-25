using System;
using Fiddler;
using System.Linq;
using NLog;
using RequestObfuscator.Exceptions;

namespace RequestObfuscator.Api.Tampering
{
    public class RequestTamperer<TRequest> : IRequestTamperer where TRequest : class
    {
        private readonly IApiSharedState<TRequest> _sharedState;
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
        public RequestTamperer(IApiSharedState<TRequest> sharedState)
        {
            _sharedState = sharedState;
        }

        public void BeforeRequest(Session session)
        {
            try
            {
                TRequest request = null;

                if (_sharedState.RequestType != typeof(object))
                {
                    request = _sharedState.ResponseSerializer.Deserialize<TRequest>(session.GetRequestBodyAsString());
                }

                if (_sharedState.WhenConditions.Any(x => !x.IsMet(session)))
                {
                    return;
                }

                if(request != null && _sharedState.WhenCondition != null && !_sharedState.WhenCondition(request))
                {
                    return;
                }

                Logger.Debug("Tampering request:");
                LogRequest(session);

                _sharedState.TamperRules.ForEach(tamper =>
                {
                    tamper.Transform(session);
                });

                if (request != null)
                {
                    _sharedState.TamperFunc?.Invoke(request);
                    session.utilSetRequestBody(_sharedState.ResponseSerializer.Serialize(request));
                }

                Logger.Debug("Tampered request:");
                LogRequest(session);
            }
            catch (SerializationException e)
            {
                Logger.Fatal(e, "Error during deserialization.");
            }
            catch(Exception e)
            {
                Logger.Fatal(e, "Unexpected exception occured.");
            }
        }

        private void LogRequest(Session session)
        {
            Logger.Debug(session.RequestHeaders);

            var requestBody = session.GetRequestBodyAsString();

            if (string.IsNullOrEmpty(requestBody))
            {
                Logger.Debug("Request body: N/A");
            }
            else
            {
                Logger.Debug("Request body: " + requestBody);
            }
        }
    }
}
