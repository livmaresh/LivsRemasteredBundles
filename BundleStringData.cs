using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivsRemasteredBundles
{
    public sealed class BundleStringData
    {

        public BundleStringData(string name, int selection, int required, string reward, string location) {
            this.name = name;
            this.selection = selection;
            this.required = required;
            this.reward = reward;
            this.location = location;
        }

        public string name;
        public int selection;
        public int required;
        public string reward;
        public string location;
    }
}
