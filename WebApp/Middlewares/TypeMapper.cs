using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Middlewares
{
    public class TypeMapper
    {
        private RequestDelegate next;

        public TypeMapper(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IViewerTypeMapper typeMapper)
        {
            var url = UriHelper.GetEncodedPathAndQuery(context.Request).ToLower();
            
            if(context.Request.Path.ToString().ToLower() == "tripdetails/index")
            {

            }

            await next.Invoke(context);
        }
    }
}
