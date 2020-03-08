using Newtonsoft.Json;
using osu.Framework.Bindables;
using osu.Framework.Lists;

namespace SeeSharp.Models
{
    public class Page
    {
        public Page()
        {
            Speed = new BindableFloat {MinValue = 0.1f, MaxValue = 4f};
            Zoom = new BindableFloat {MinValue = 0.6f, MaxValue = 8f};
        }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("speed")]
        public BindableFloat Speed { get; set; }

        [JsonProperty("zoom")]
        public BindableFloat Zoom { get; set; }

        [JsonProperty("bars")]
        public SortedList<float> Bars { get; set; }
    }
}