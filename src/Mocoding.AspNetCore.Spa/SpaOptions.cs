using System;
using System.Collections.Generic;
using System.Text;

namespace Mocoding.AspNetCore.Spa
{
    public class SpaOptions
    {
        public SpaOptions()
        {
            IncludeScripts = true;
            BabelPolyfill = null;
            SsrScriptPath = "./wwwroot_node/server.js";
        }

        public bool IncludeScripts { get; set; }

        public string BabelPolyfill { get; set; }

        public string SsrScriptPath { get; set; }
    }
}
