using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivsRemasteredBundles
{
    public sealed class BundleData
    {
        public BundleData()
        {
            //this.name = "";
            //this.location = "";
            //this.bundleString = "";
            this.names = new List<string>();
            this.locations = new List<string>();
            this.bundleStrings = new List<string>();
        }

        public List<string> names { get; set; }
        public List<string> locations { get; set; }
        public List<string> bundleStrings { get; set; }
    }
}
