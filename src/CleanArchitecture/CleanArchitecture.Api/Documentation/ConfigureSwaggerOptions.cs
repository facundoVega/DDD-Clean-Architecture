using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitecture.Api.Documentation;

public sealed class ConfigureSwagerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwagerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach(var documentation in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(documentation.GroupName,CreateDocumentation(documentation));
        }
    }

    private static OpenApiInfo CreateDocumentation(ApiVersionDescription apiVersionDescription)
    {
        var OpenApiInfo = new OpenApiInfo 
        {
            Title = $"CleanArchitecture.Api v{ apiVersionDescription.ApiVersion }",
            Version = apiVersionDescription.ApiVersion.ToString()
        };

        if(apiVersionDescription.IsDeprecated)
        {
            OpenApiInfo.Description += "This API version has been deprecated";
        }

        return OpenApiInfo;
    }
}
