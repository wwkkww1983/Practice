using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Caist.ICL.Api
{
    public class SwaggerParameterFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.TryGetMethodInfo(out MethodInfo mi))
            {
                if (!mi.GetCustomAttributes<NonAuthAttribute>(true).Any())
                {
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Type = "string",
                        Name = "Authorization",
                        In = "header",
                        Default = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1Njc4NDY2ODYsInVzZXIiOnsiVXNlcklEIjoiMSIsIlVzZXJOYW1lIjoi6LaF57qn566h55CG5ZGYIn19.U4mnFIJpioXChcKIBD29MLx1UL-NfPiK6hM1XVrw2U8",
                        Required = true,
                        Description = ""
                    });
                }
            }
        }
    }
}