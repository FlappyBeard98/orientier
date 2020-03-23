using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization;
using RestSharp.Serializers;

namespace OrientDb.Common
{
    /// <summary>
    /// Адаптер NewtonsoftJson для IRestSerializer
    /// </summary>
    /// <remarks>
    /// нужен так как в некоторых случаях сериализатор IRestClient не справляется
    /// </remarks>
    public class RestSerializer : IRestSerializer
    { 
        public class Serializer : ISerializer
        {
            public string Serialize(object obj) => JsonConvert.SerializeObject(obj);

            public string ContentType { get; set; } = "application/json";
        }
    
        public class Deserializer : IDeserializer
        {
            public T Deserialize<T>(IRestResponse response) => JsonConvert.DeserializeObject<T>(response.Content);
        }
        
        private static readonly ISerializer JsonSerializer = new Serializer();
        private static readonly IDeserializer JsonDeserializer = new Deserializer();
        public string Serialize(object obj) => JsonSerializer.Serialize(obj);

        public string ContentType { get; set; }
        public T Deserialize<T>(IRestResponse response) => JsonDeserializer.Deserialize<T>(response);

        public string Serialize(Parameter parameter) => Serialize(parameter.Value);

        public string[] SupportedContentTypes { get; } =
        {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
        };
        public DataFormat DataFormat => DataFormat.Json;
        
      
    }
}