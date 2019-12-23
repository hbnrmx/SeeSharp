using System.Collections.Generic;
using Newtonsoft.Json;

namespace SeeSharp.Models
{
    public class Data
    {
        [JsonProperty("default_speed")]
        public float DefaultSpeed;

        [JsonProperty("default_zoom")]
        public float DefaultZoom;
        
        [JsonProperty("pages")]
        public List<Page> Pages { get; set; }
    }
}