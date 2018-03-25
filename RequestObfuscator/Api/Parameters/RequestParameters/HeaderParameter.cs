using Fiddler;

namespace RequestObfuscator.Api.Parameters.RequestParameters
{
    internal class HeaderParameter : KeyValueParameter<string, string>
    {
        internal HeaderParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }

        internal override bool IsMet(Session session)
        {
            return session.RequestHeaders.Exists(Key);
        }

        internal override Session Transform(Session session)
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
