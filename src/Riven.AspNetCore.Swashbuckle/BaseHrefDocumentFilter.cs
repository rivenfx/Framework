using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven
{
    public class BaseHrefDocumentFilter : IDocumentFilter
    {
        readonly string _baseHref;

        public BaseHrefDocumentFilter(string baseHref)
        {
            _baseHref = baseHref?.Trim()?.TrimEnd('/')?.Trim();
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (!string.IsNullOrWhiteSpace(this._baseHref)
                && this._baseHref != "/")
            {
                swaggerDoc.Servers.Add(new OpenApiServer()
                {
                    Url = this._baseHref
                });
            }
        }
    }
}
