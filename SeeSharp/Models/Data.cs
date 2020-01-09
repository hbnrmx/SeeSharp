using Newtonsoft.Json;
using osu.Framework.Bindables;
using osu.Framework.Lists;

namespace SeeSharp.Models
{
    public class Data
    {
        [JsonProperty("default_speed")]
        public float DefaultSpeed;

        [JsonProperty("default_zoom")]
        public float DefaultZoom;
        
        [JsonProperty("pages")]
        public Bindable<SortedList<BindablePage>> Pages { get; set; }
    }
}