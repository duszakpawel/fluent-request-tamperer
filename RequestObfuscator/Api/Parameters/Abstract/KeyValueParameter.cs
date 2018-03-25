namespace RequestObfuscator.Api.Parameters
{
    internal abstract class KeyValueParameter<T,V> : Parameter
    {
        protected internal T Key { get; set; }
        protected internal V Value { get; set; }
    }
}
