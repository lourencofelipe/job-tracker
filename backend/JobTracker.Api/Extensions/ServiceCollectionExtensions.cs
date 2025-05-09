namespace JobTracker.Api.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IJobApplicationService, JobApplicationService>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
       services.AddDbContext<EfContext>(options =>
            options.UseInMemoryDatabase("InMemoryDatabase"));

        services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "JobTracker API",
                Version = "v1"
            });

            options.UseInlineDefinitionsForEnums();
            options.MapType<ApplicationStatus>(() => new OpenApiSchema
            {
                Type = "string",
                Enum = (IList<IOpenApiAny>)Enum
                    .GetNames<ApplicationStatus>()
                    .Select(n => new OpenApiString(n))
                    .ToList<object>() // regiter of enums as string
            });
        });

        return services;
    }
}
