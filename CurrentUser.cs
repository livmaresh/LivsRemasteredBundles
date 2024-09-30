using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivsRemasteredBundles
{
    public sealed class CurrentUser
    {
        public CurrentUser()
        {
            this.farmerName = "";
            this.farmName = "";
            this.bundleType = "Remastered";
        }

        public string farmerName { get; set; }
        public string farmName { get; set; }
        public string bundleType { get; set; }
    }
}
