
using Pet4U.Application.UseCases.CreateVolunteer;
using Microsoft.Extensions.DependencyInjection;
using Pet4U.Infrastructure.Repositories;
using Pet4U.Infrastructure.Interceptors;
using Minio;
using Minio.AspNetCore;
using Microsoft.Extensions.Configuration;
using Pet4U.Infrastructure.Options;
using Minio.Helper;
using Pet4U.Infrastructure.Providers;
using Pet4U.Application.UseCases.Providers;

namespace Pet4U.Infrastructure;

public static class Inject
{
  public static IServiceCollection AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<ApplicationDbContext>();
    services.AddScoped<IVolunteersRepository, VolunteersRepository>();
    
    //services.AddSingleton<SoftDeleteInterceptor>();
    services.AddMinio(configuration);

    return services;
  }


  //services.Configure<Options.MinioOptions>(configuration.GetSection(Options.MinioOptions.MINIO));

  private static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddMinio(options =>
    {

      Options.MinioOptions minioOptions = configuration.GetSection(Options.MinioOptions.MINIO).Get<Options.MinioOptions>() ?? throw new ApplicationException("Missing minio configuration");
      
      options.WithEndpoint(minioOptions.Endpoint);
      options.WithCredentials(minioOptions.AccessKey,minioOptions.SecretKey);
      options.WithSSL(minioOptions.IsSsl);
    });

    services.AddScoped<IFileProvider, MinioProvider>();

    return services;
  }
}