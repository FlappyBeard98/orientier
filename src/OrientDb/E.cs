using Newtonsoft.Json;

namespace OrientDb
{
    public class E : GraphItem
    {
        [JsonProperty("in")]internal string In {get; set;}
        [JsonProperty("out")]internal string Out {get; set;}
        public string ToRid => In;
        public string FromRid => Out;
        internal V FromV { get; set; }
        internal V ToV { get; set; }
    }
    
    public  class E<TFrom, TTo> : E where TFrom : V where TTo : V
    {
       
    }
}