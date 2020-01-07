using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Mocoding.AspNetCore.Spa.Abstractions
{
    public interface IServerRenderer
    {
        public Task<string> RenderHtmlPage(HttpContext context);
    }
}
