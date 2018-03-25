namespace RequestObfuscator.Api.Parameters
{
    public abstract class KeyParameter<T> : Parameter
    {
        protected T Key { get; set; }
    }
}
