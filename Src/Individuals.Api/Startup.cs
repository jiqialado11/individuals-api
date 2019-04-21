using FluentValidation.AspNetCore;
using Individuals.Api.Middlewares;
using Individuals.Commands.Images.AddImage;
using Individuals.Commands.Individual.CreateIndividual;
using Individuals.DataAccess;
using Individuals.DataAccess.Contracts;
using Individuals.Decorators;
using Individuals.Persistance;
using Individuals.Persistance.Repositories;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Queries.Individuals.QueryIndividuals;
using Individuals.Shared.Configurations;
using Individuals.Shared.Contracts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Individuals.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

         public void ConfigureServices(IServiceCollection services)
        {
               services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<QueryIndividualsQuery>();
                    options.RegisterValidatorsFromAssemblyContaining<AddImageCommand>();
                    options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    options.ImplicitlyValidateChildProperties = true;
                    options.LocalizationEnabled = false;
                }); ;
            services.AddDbContext<IndividualsDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork<IndividualsDBContext>>();
            services.AddMediatR(typeof(QueryIndividualsQuery),typeof(CreateIndividualCommand));
            DecoratorRegistrar.RegisterDecoratorsFromAssembly(services, typeof(QueryIndividualsQuery).Assembly);
            DecoratorRegistrar.RegisterDecoratorsFromAssembly(services, typeof(CreateIndividualCommand).Assembly);

            services.AddTransient<IIndividualsRelatedQueries, IndividualsRelatedQueries>();
            services.AddTransient<IIndividualsRepository, IndividualsRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IPhoneNumbersRepository, PhoneNumbersRepository>();

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddMemoryCache();

            AutoMapper.Mapper.Initialize(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCors",
                    builder =>
                    {
                        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials().Build();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Individuals.Api",
                    Version = "v1",
                    Description = "Application programming interface regarding operations related individual entities"

                });
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("EnableCors");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.DisplayRequestDuration();
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Individuals.Api V1");
            });
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMvc();
        }
    }
}
