using Fiddler;

namespace RequestObfuscator.Api.Parameters.RequestParameters
{
    public class HeaderParameter : KeyValueParameter<string, string>
    {
        public HeaderParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override bool IsMet(Session session)
        {
            return session.RequestHeaders.Exists(Key);
        }

        public override Session Transform(Session session)
        {
            if (session.RequestHeaders.Exists(Key))
            {
                session.RequestHeaders[Key] = Value;
            }
            else
            {
                session.RequestHeaders.Add(Key, Value);
            }

            return session;
        }
    }
}
