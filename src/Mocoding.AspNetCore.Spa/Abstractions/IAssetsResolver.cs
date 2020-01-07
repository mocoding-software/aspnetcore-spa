using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Mocoding.AspNetCore.Spa.Abstractions
{

    public interface IAssetsResolver
    {
        public string[] ResolveAssets(HttpContext context);
    }
}
