namespace RequestObfuscator.Deserializers
{
    public interface IResponseSerializer
    {
        string Serialize<TDestination>(TDestination responseContent) where TDestination : class;
        TDestination Deserialize<TDestination>(string responseContent) where TDestination : class;
    }
}
