using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElectronicsDatabase.Models
{
    internal class Part
    {
        public string id { get; set; }
        public string name { get; set; }
        public string amount { get; set; }
        public string description { get; set; }
    }
}
