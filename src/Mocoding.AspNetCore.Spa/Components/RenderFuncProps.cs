using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mocoding.AspNetCore.Spa.Components
{
    public class RenderFuncProps
    {
        public string requestUrl { get; set; }
        public string baseUrl { get; set; }
        public string[] assets { get; set; }
        public InlineScript[] inlineScripts { get; set; }
    }
    public class InlineScript
    {
        public string position { get; set; }
        public string script { get; set; }
    }
}
