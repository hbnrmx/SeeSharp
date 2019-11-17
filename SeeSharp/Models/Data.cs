using System.Collections.Generic;
using Newtonsoft.Json;

namespace SeeSharp.Models
{
    public class Data
    {
        [JsonProperty("default_speed")]
        public float DefaultSpeed;

        [JsonProperty("default_magnification")]
        public float DefaultMagnification;
        
        [JsonProperty("pages")]
        public List<Page> Pages { get; set; }
    }
}