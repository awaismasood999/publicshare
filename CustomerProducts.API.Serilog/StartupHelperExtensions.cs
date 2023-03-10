using AspNetCore.Serilog.RequestLoggingMiddleware;
using CustomerProducts.Core.DbContexts;
using CustomerProducts.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CustomerProducts.API;

internal static class StartupHelperExtensions
{
    // Add services to the container
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddControllers();

        builder.Services.AddScoped<IUserRepository,
           UserRepository>();

        builder.Services.AddScoped<ICustomerProductsRepository,
            CustomerProductsRepository>();

        builder.Services.AddDbContext<UserContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DemoDatabase"));
        });

        builder.Services.AddScoped<ICustomerProductsContext, CustomerProductsContext>();

        //builder.Services.AddScoped<ICustomerProductsContext, CustomerProductsContext>().AddDbContext<CustomerProductsContext>(options =>
        //{
        //    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoDatabase"));
        //});

        builder.Services.AddDbContext<CustomerProductsContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DemoDatabase"));
        });

        builder.Services.AddAutoMapper(
            AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddApiVersioning(s =>
        {
            s.AssumeDefaultVersionWhenUnspecified = true;
            s.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            s.ReportApiVersions = true;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
         {
             o.AddSecurityDefinition("CustomerProductApiAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
             {
                 Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                 Scheme = "Bearer",
                 Description = "Use a valid token to get access"
             });

             o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "CustomerProductApiAuth" }
                        }, new List<string>() }
                });
         }
        );

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                };
            });
        return builder.Build();
    }

    // Configure the request/response pipelien
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        //if (app.Environment.IsDevelopment())
        //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        //}

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers(); 
         
        return app; 
    }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        //using (var scope = app.Services.CreateScope())
        //{
        //    try
        //    {
        //        var context = scope.ServiceProvider.GetService<CustomerProductsContext>();
        //        if (context != null)
        //        {
        //            await context.Database.EnsureDeletedAsync();
        //            await context.Database.MigrateAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
        //        logger.LogError(ex, "An error occurred while migrating the database.");
        //    }
        //} 
    }
}