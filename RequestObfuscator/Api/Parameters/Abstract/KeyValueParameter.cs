namespace RequestObfuscator.Api.Parameters
{
    public abstract class KeyValueParameter<T,V> : Parameter
    {
        protected T Key { get; set; }
        protected V Value { get; set; }
    }
}
