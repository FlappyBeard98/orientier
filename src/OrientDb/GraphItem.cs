using Newtonsoft.Json;

namespace OrientDb
{
    public abstract class GraphItem
    {
        [JsonProperty("@rid")]public string Rid { get;internal set; }
        internal bool IsNew() => string.IsNullOrWhiteSpace(Rid);
    }
}