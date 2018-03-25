using Newtonsoft.Json;
using RequestObfuscator.Exceptions;
using System;

namespace RequestObfuscator.Deserializers
{
    public class JsonResponseSerializer : IResponseSerializer
    {
        public object Deserialize(Type destinationType, string responseContent)
        {
            try
            {
                return JsonConvert.DeserializeObject(responseContent, destinationType);
            }
            catch (Exception e)
            {
                throw new SerializationException($"Error during deserialization: {e.Message}, of: {responseContent}", e);
            }
        }

        public TDestination Deserialize<TDestination>(string responseContent) where TDestination : class
        {
            return (TDestination)Deserialize(typeof(TDestination), responseContent);
        }

        public string Serialize<TDestination>(TDestination responseContent) where TDestination : class
        {
            try
            {
                return JsonConvert.SerializeObject(responseContent);
            }
            catch (Exception e)
            {
                throw new SerializationException($"Error during serialization: {e.Message}, of: {responseContent}", e);
            }
        }
    }
}
