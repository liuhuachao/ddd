using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace WebApi.Filters
{
    /// <summary>  
    /// 隐藏接口，不生成到swagger文档展示  
    /// </summary>  
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]

    public partial class HiddenApiAttribute : Attribute { }
    public class HiddenApiFilter : IDocumentFilter
    {
        //public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        //{
        //    foreach (ApiDescription apiDescription in context.ApiDescriptions)
        //    {
        //        if (apiDescription.ControllerAttributes().OfType<HiddenApiAttribute>().Count() == 0
        //            && apiDescription.ActionAttributes().OfType<HiddenApiAttribute>().Count() == 0)
        //            continue;

        //        var key = "/" + apiDescription.RelativePath.TrimEnd('/');
        //        if (!key.Contains("/test/") && swaggerDoc.Paths.ContainsKey(key))
        //            swaggerDoc.Paths.Remove(key);
        //    }
        //}

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (ApiDescription apiDescription in context.ApiDescriptions)
            {
                if (new OperationFilterContext(apiDescription, null).ControllerActionDescriptor.GetControllerAndActionAttributes(true).OfType<HiddenApiAttribute>().Count() == 0)
                {
                    continue;
                }
                var key = "/" + apiDescription.RelativePath.TrimEnd('/');
                if (!key.Contains("/test/") && swaggerDoc.Paths.ContainsKey(key))
                {
                    swaggerDoc.Paths.Remove(key);
                }

            }
        }

    }
}