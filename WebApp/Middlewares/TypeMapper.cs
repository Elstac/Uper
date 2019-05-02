using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Repositories;
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

        public async Task Invoke(HttpContext context,
                                 IViewerTypeMapper typeMapper,
                                 IAccountManager accountManager,
                                 ITripDetailsRepository repository)
        {
            var url = UriHelper.GetEncodedPathAndQuery(context.Request).ToLower();
            
            if(context.Request.Path.ToString().ToLower() == "/tripdetails/index")
            {
                int id;
                if(!int.TryParse(context.Request.Query["id"],out id))
                {
                    return;
                }
                var user = await accountManager.GetUserAsync(context.User);
                var data = repository.GetTripWithPassengersById(id);
                var type = typeMapper.GetViewerType(user,data);
                
                context.Request.QueryString = new QueryString($"?id={id}&viewerType={type}");
            }

            await next.Invoke(context);
        }
    }
}
