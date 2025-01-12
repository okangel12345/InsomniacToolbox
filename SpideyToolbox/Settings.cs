using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpideyToolbox
{
    public class AppSettings
    {
        // Modding settings
        //------------------------------------------------------------------------------------------
        public bool _autoloadRecent { get; set; }
        public string _authorName { get; set; }

        // Save most recent TOCs
        //------------------------------------------------------------------------------------------
        public string _recentTOC1 { get; set;}
        public string _recentTOC2 { get; set; }
        public string _recentTOC3 { get; set; }
        public string _recentTOC4 { get; set; }
        public string _recentTOC5 { get; set; }
    }

}
