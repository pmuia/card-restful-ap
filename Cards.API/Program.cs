using Core.Domain.Utils;
using Core.Management.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Cards.API.Models.Attribute;
using Cards.API.Models.Common;
using Cards.API.Models.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Core.Management;
using Core.Domain;
using Core.Domain.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Core.Management.Infrastructure.Seedwork;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();


// Add services to the container.

builder.Services.AddHttpClient();
builder.Services.AddAuthentication(configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddAutoMapper(cfg => { cfg.AllowNullDestinationValues = true; cfg.AllowNullCollections = false; }, AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddVersioning();
builder.Services.AddCustomControllers();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCustomOptions(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IApiVersionDescriptionProvider, DefaultApiVersionDescriptionProvider>();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        CardContext context = scope.ServiceProvider.GetRequiredService<CardContext>();
        if (context.Database.IsSqlServer()) { context.Database.Migrate(); }

        ISeed seed = services.GetRequiredService<ISeed>();
        seed.SeedDefaults().Wait();

        logger.LogInformation("Seed");
    }
    catch (Exception ex)
    {
        logger.LogError($"An error while setting up infrastructure - migration, sequences and seed {ex.Message}");
        throw;
    }
}

//Get API versions and their descriptions
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseDeveloperExceptionPage();
}

app.UseSwaggerDocumentation(provider);

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();

/// <summary>
/// 
/// </summary>
public static class ConfigurationExtensionMethods
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        //Add Token Validation Parameters
        TokenValidationParameters tokenParameters = new TokenValidationParameters
        {
            //what to validate
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            //set up validation data
            ValidIssuer = configuration["Security:Issuer"],
            ValidAudience = configuration["Security:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Security:Key"])),
            ClockSkew = new TimeSpan(0)//The validation parameters have a default clock skew of 5 minutes so i have to invalidate it to 0
        };

        //Add JWT Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = tokenParameters;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(AuthPolicy.GlobalRights), policy => policy.RequireRole(nameof(Roles.Root), nameof(Roles.Admin), nameof(Roles.Webapi), nameof(Roles.User)));
            options.AddPolicy(nameof(AuthPolicy.ElevatedRights), policy => policy.RequireRole(nameof(Roles.Root), nameof(Roles.Admin)));
        });

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        //REF https://dev.to/99darshan/restful-web-api-versioning-with-asp-net-core-1e8g
        //REF https://github.com/Microsoft/aspnet-api-versioning/wiki
        services.AddApiVersioning(options =>
        {
            // specify the default API Version as 1.0
            options.DefaultApiVersion = new ApiVersion(1, 0);

            // if the client hasn't specified the API version in the request, use the default API version number 
            options.AssumeDefaultVersionWhenUnspecified = true;

            // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
            options.ReportApiVersions = true;

            // DEFAULT Version reader is QueryStringApiVersionReader();
            // clients request the specific version using the X-version header
            // options.ApiVersionReader = new Microsoft.AspNetCore.Mvc.Versioning.HeaderApiVersionReader("X-version");   
            // Supporting multiple versioning scheme
            // options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader(new[] { "api-version", "x-version", "version" }),
            // new QueryStringApiVersionReader(new[] { "api-version", "v", "version" }));//MediaTypeApiVersionReader-UrlSegmentApiVersionReader

            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            options.ErrorResponses = new VersionErrorProvider();
        });

        services.AddVersionedApiExplorer(options =>
        {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        //TODO: revisit porting to native system.text.json https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ModelStateFilter));
            options.Filters.Add(typeof(ExceptionFilter));
        })
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(options =>
        {
            // add a custom operation filter which sets default values
            options.OperationFilter<SwaggerDefaultValues>();

            // integrate xml comments
            options.IncludeXmlComments(XmlCommentsFilePath);

            //define how the API is secured by defining one or more security schemes.
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Enter in the value field: <b>Bearer {your JWT token}</b>"
            });

            options.OrderActionsBy(description =>
            {
                ControllerActionDescriptor controllerActionDescriptor = (ControllerActionDescriptor)description.ActionDescriptor;
                SwaggerOrderAttribute attribute = (SwaggerOrderAttribute)controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute(typeof(SwaggerOrderAttribute));
                return string.IsNullOrEmpty(attribute?.Order?.Trim()) ? description.GroupName : attribute.Order.Trim();
            });

            //Operation security scheme based on Authorize attribute using OperationFilter()
            options.OperationFilter<SwaggerAuthOperationFilter>();
        });

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    public static string XmlCommentsFilePath
    {
        get
        {
            //typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
            return Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        // Enable middleware to serve generated Swagger as a JSON endpoint.            
        app.UseSwagger();

        //Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(options =>
        {
            //options.RoutePrefix = "";
            // build a swagger endpoint for each discovered API version
            foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
            options.DocExpansion(docExpansion: DocExpansion.None);
        });

        return app;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EventSetting>(configuration.GetSection("Events"));
        return services;
    }

}
