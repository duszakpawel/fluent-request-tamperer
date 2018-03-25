namespace RequestObfuscator.Api.Parameters
{
    internal abstract class KeyParameter<T> : Parameter
    {
        protected internal T Key { get; set; }
    }
}
