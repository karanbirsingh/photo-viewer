using Newtonsoft.Json;
using System.Collections.Generic;

namespace PhotoViewer
{
    public class ElementNode
    {
        public string Name { get; set; }
        public string ControlType { get; set; } // Localized, or fallback to control type
        public IEnumerable<ElementNode> Children { get; set; }

        [JsonIgnore]
        public string DisplayContent => $"{ControlType} {Name}";
    }
}
