using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentsStorage.Infra.Data.Models
{
    public class File
    {
        public long Id { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte[] FileContent { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string FileType { get; set; }
        public string Description { get; set; }
    }
}
