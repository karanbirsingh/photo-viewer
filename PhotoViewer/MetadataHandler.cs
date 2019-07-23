using MetadataExtractor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer
{
    class MetadataHandler
    {
        const string AccessibleKeyword = "a11ytree";
        public static ElementNode LoadMetadata(Stream fileStream)
        {
            IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(fileStream);
            var a11yTag = directories.Where(dir => dir.Name == "PNG-iTXt").SelectMany(dir => dir.Tags)
                .Where(tag => tag.Description.StartsWith(AccessibleKeyword, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            if (a11yTag == null) return null;

            try
            {
                return JsonConvert.DeserializeObject<ElementNode>(a11yTag.Description.Substring(AccessibleKeyword.Length + 1));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
