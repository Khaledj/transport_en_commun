using System;
using System.Collections.Generic;
using System.Text;

namespace Projet2
{
    public class DetailLigne
    {
        public string id { get; set; }
        public string name { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public List<string> lines { get; set; }
    }
}
