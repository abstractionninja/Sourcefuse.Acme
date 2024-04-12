using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sourcefuse.Acme.WebApi.Configuration
{
    /// <summary>
    /// Operation Filter to add api key parameter to swagger
    /// </summary>
    public class ApiKeyOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Default Apply Method
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= [];

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Api-Key",
                In = ParameterLocation.Header,
                Description = "The api key is added to the header",
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Description = "The api key is add to the header"
                },
               
                Required = true // set to false if this is optional
            });

            operation.Responses.Add("401", new OpenApiResponse
            {
                Description = "Missing or Invalid Api Key",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/problem+json"] = new OpenApiMediaType(),
                }
            });
        }
    }
}
