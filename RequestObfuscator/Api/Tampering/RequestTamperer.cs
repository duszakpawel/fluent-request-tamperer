using System;
using Fiddler;
using System.Linq;
using NLog;
using RequestObfuscator.Exceptions;

namespace RequestObfuscator.Api.Tampering
{
    internal abstract class RequestTampererBase<TRequest, TSharedState> : IRequestTamperer where TRequest : class where TSharedState : IApiSharedState
    {
        protected TSharedState _sharedState;
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        internal RequestTampererBase(TSharedState sharedState)
        {
            _sharedState = sharedState;
        }

        public void BeforeRequest(Session session)
        {
            try
            {
                TRequest request = DeserializeResponse(session);

                if(!WhenCondition(session, request, _sharedState))
                { 
                    return;
                }

                if (_sharedState.AbortRequest)
                {
                    Logger.Debug("Aborting request:");
                    LogRequest(session);

                    session.Abort();
                    Logger.Debug("Request aborted.");

                    return;
                }

                Logger.Debug("Tampering request:");
                LogRequest(session);

                TamperRules(session, _sharedState);

                if (request != null)
                {
                    TamperRequest(request);
                    SaveRequest(session, request);
                }

                Logger.Debug("Tampered request:");
                LogRequest(session);
            }
            catch (SerializationException e)
            {
                Logger.Fatal(e, "Error during deserialization.");
            }
            catch (Exception e)
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

        protected internal abstract TRequest DeserializeResponse(Session session);
        protected internal abstract void TamperRequest(TRequest request);
        protected internal abstract void SaveRequest(Session session, TRequest request);
        protected internal abstract bool WhenCondition(Session session, TRequest request, TSharedState sharedState);
        protected internal void TamperRules(Session session, IApiSharedState sharedState)
        {
            sharedState.TamperRules.ForEach(tamper =>
            {
                tamper.Transform(session);
            });
        }
    }

    internal class RequestTamperer<TRequest> : RequestTampererBase<TRequest, IApiSharedState<TRequest>> where TRequest : class
    {
        internal RequestTamperer(IApiSharedState<TRequest> sharedState) : base(sharedState)
        {
        }

        protected internal override TRequest DeserializeResponse(Session session)
        {
            return _sharedState.ResponseSerializer.Deserialize<TRequest>(session.GetRequestBodyAsString());
        }

        protected internal override void TamperRequest(TRequest request)
        {
            if (request != null)
            {
                _sharedState.TamperFunc?.Invoke(request);
            }
        }

        protected internal override void SaveRequest(Session session, TRequest request)
        {
            session.utilSetRequestBody(_sharedState.ResponseSerializer.Serialize(request));
        }

        protected internal override bool WhenCondition(Session session, TRequest request, IApiSharedState<TRequest> sharedState)
        {
            if (_sharedState.WhenConditions.Any(x => !x.IsMet(session)))
            {
                return false;
            }

            if (request != null && _sharedState.WhenCondition != null && !_sharedState.WhenCondition(request))
            {
                return false;
            }

            return true;
        }
    }

    internal class RequestTamperer : RequestTampererBase<string, IApiSharedState<string>>
    {
        internal RequestTamperer(IApiSharedState<string> sharedState) : base(sharedState)
        {
        }

        protected internal override string DeserializeResponse(Session session)
        {
            return session.GetRequestBodyAsString();
        }

        protected internal override void TamperRequest(string request)
        {
            if (request != null)
            {
                _sharedState.StrTamperFunc?.Invoke(request);
            }
        }

        protected internal override void SaveRequest(Session session, string request)
        {
            session.utilSetRequestBody(request);
        }

        protected internal override bool WhenCondition(Session session, string request, IApiSharedState<string> sharedState)
        {
            if (_sharedState.WhenConditions.Any(x => !x.IsMet(session)))
            {
                return false;
            }

            if (request != null && _sharedState.StrWhenCondition != null && !_sharedState.StrWhenCondition(request))
            {
                return false;
            }

            return true;
        }
    }
}
