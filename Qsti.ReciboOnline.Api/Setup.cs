using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.DependencyInjection;
using Qsti.ReciboOnline.Infra.Repositories;
using Qsti.ReciboOnline.Api.Security;
using Qsti.ReciboOnline.Api;
using Qsti.ReciboOnline.Domain.Commands.Usuario.AdicionarUsuario;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Interfaces.Services;
using Qsti.ReciboOnline.Infra.Repositories.Transactions;
using Qsti.ReciboOnline.Infra.Services.ServicePushNotification;
using Qsti.ReciboOnline.Infra.Repositories.Base;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;

namespace Qsti.ReciboOnline.Api
{
    public static class Setup
    {
        private const string ISSUER = "c1f51f42";
        private const string AUDIENCE = "c6bbbb645024";


        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            //Configuração do Token
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations
            {
                Audience = AUDIENCE,
                Issuer = ISSUER,
                Seconds = int.Parse(TimeSpan.FromDays(1).TotalSeconds.ToString())

            };
            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.SigningCredentials.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            //Para todas as requisições serem necessaria o token, para um endpoint não exisgir o token
            //deve colocar o [AllowAnonymous]
            //Caso remova essa linha, para todas as requisições que precisar de token, deve colocar
            //o atributo [Authorize("Bearer")]
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddCors();

        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Pipelines.MeasureTime<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Pipelines.ValidateCommand<,>));

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly, typeof(AdicionarUsuarioRequest).GetTypeInfo().Assembly);


        }

        public static void ConfigureSignalR(this IServiceCollection services)
        {
            services.AddSignalR();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            //services.AddDbContext<ControlContext>();
            services.AddScoped<Context, Context>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IRepositoryUsuario, RespositoryUsuario>();
            services.AddTransient<IRepositoryFuncionario, RespositoryFuncionario>();
            services.AddTransient<IRepositoryPushNotification, RepositoryPushNotification>();
            services.AddTransient<IRepositoryRecibo, RepositoryRecibo>();
            services.AddTransient<IRepositorySolicitacao, RepositorySolicitacao>();
           // services.AddTransient<IRepositoryPushNotification, RepositoryPushNotification>();

        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IServiceEmail, ReciboOnline.Infra.Services.ServiceEmail.ServiceEmail>();
            
            services.AddTransient<IServicePushNotification, ServicePushNotification>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName); //Se tiver objetos request com o mesmo nome em namespace diferentes dava erro, isso resolve
                c.SwaggerDoc("v1", new Info { Title = "Qsti.ReciboOnline", Version = "v1" });
            });
        }

        public static void ConfigureMVC(this IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("ApiCorsPolicy",
            //    builder =>
            //    {
            //        builder.WithOrigins("http://control.qsti.com.br","https://control.qsti.com.br", "http://control.qsti.com.br","https://control.qsti.com.br","*")
            //                            .WithMethods("GET","POST","PUT","DELETE")
            //                            .AllowAnyOrigin()
            //                            .AllowCredentials()
            //                            .AllowCredentials()
            //                            .AllowAnyHeader();
            //    });
            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.Configure<MvcOptions>(options => {
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("ApiCorsPolicy"));
            //});
        }
    }
}