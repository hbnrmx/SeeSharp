using System.IO;
using Newtonsoft.Json;
using osu.Framework.Lists;

namespace SeeSharp.Models
{
    public class Page
    {
        [JsonProperty("file_info")]
        public FileInfo FileInfo { get; set; }

        [JsonProperty("speed")]
        public float Speed { get; set; }

        [JsonProperty("magnification")]
        public float Magnification { get; set; }

        [JsonProperty("bars")]
        public SortedList<float> Bars { get; set; }
    }
}