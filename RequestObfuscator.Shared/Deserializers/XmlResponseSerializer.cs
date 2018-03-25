using RequestObfuscator.Exceptions;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace RequestObfuscator.Deserializers
{
    public class XmlResponseSerializer : IResponseSerializer
    {
        public object Deserialize(Type destinationType, string responseContent)
        {
            try
            {
                var serializer = new XmlSerializer(destinationType);

                return serializer.Deserialize(new StringReader(responseContent));
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
                var serializer = new XmlSerializer(typeof(TDestination));

                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        serializer.Serialize(writer, responseContent);

                        return sww.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                throw new SerializationException($"Error during serialization: {e.Message}, of: {responseContent}", e);
            }
        }
    }
}
