using ArchiAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//commentaires : 
// ce fichier est éxecuter au début de notre serveur
namespace ArchiAPI
{
    public class Startup
    {
        // configuration of the secret key 

        private const string SECRET_KEY = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // méthdoe qui permet de configurer nos services et donc par extension configurer toutes nos 
        // injéction de dépendances
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // configure the JWT Authentification Service 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    // the Signing Key is defined in the TokenController class

                    // signing key is like a secure password...
                    IssuerSigningKey = SIGNING_KEY,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "https://localhost:44385",
                    ValidAudience = "https://localhost:44385",
                    ValidateLifetime = true
                };
            });
            services.AddDbContext<ArchiDbContext>(options =>
            {
                // lui fournir la chaine de connexion de la bdd et donc demander un accès à un la bdd
                options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Archidb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            });
            // versionning

            services.AddApiVersioning(vers =>
            {
                vers.DefaultApiVersion = new ApiVersion(1, 1);
                vers.AssumeDefaultVersionWhenUnspecified = true;
                vers.ReportApiVersions = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // fonction qui va venir créer les propres fonctionnalités lors de l'appel de l'API
        // en spécifiant quel fonctionnalités que l'on veut utiliser
        // utilsiation des API (application auto emergé, utilsiation de middlewware qui viendont booster)
        // le processus de traitement de requête et de réponse
        // utilisation de nginx (serveur passerelle qui permettra de transférer des demande afin de nous retourner
        // la réponse dans notre serveur local
        // ce serveur fera la passerelle entre les différente API et notre serveur locale
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // toute ces app. sont nos middlewares
            app.UseHttpsRedirection();

            // confuguration for loggin using serilog
            app.UseSerilogRequestLogging();

            app.UseRouting();
             
            // use the authentification 
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
