// <copyright file="IngredientsController.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using MenuPlanner.Server.Contracts.Blob;
using MenuPlanner.Server.Contracts.Logic;
using MenuPlanner.Server.Data;
using MenuPlanner.Server.Logic;
using MenuPlanner.Server.Logic.Blob;
using MenuPlanner.Server.Logic.EntityUpdater;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SqlHandler;
using SqlHandler.Contracts;



namespace MenuPlanner.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ISqlConnectionHandler sqlConnection = new SqlConnectionHandler() {Configuration = Configuration};
            services = sqlConnection.HandleSQLServers<MenuPlannerContext>(services, sqlConnection.GetCredentialsFromConfiguration("Data"));
            services = sqlConnection.HandleSQLServers<ApplicationDbContext>(services, sqlConnection.GetCredentialsFromConfiguration("Auth"));

            //Added to handle the EF Reference Loop in Ingredient Model taken from https://stackoverflow.com/a/58155532
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<ApplicationUser>(options =>  options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //var identityServer = services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            //    var certificate = new X509Certificate2("certs/aspnetapp-root-cert.pfx", "password");
            //    identityServer.AddSigningCredential(certificate);
            
            IoCSetUp(services);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                    builder
                        .AllowAnyMethod()
                        .AllowCredentials()
#if DEBUG
                        .SetIsOriginAllowed((host) => true)
#else
                        .WithOrigins("https://*.ddev.ch").SetIsOriginAllowedToAllowWildcardSubdomains()
#endif
                        .AllowAnyHeader();
            });
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme =  CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(options =>
                {
                    options.Authority = Configuration["IdentityServer:Clients:MenuPlanner.Client:Authority"];
                    options.ClientId = Configuration["IdentityServer:Clients:MenuPlanner.Client:Id"];
                    options.ClientSecret = Configuration["IdentityServer:Clients:MenuPlanner.Client:Secret"];                    
                    options.ResponseType = "code";
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.UseTokenLifetime = false;
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                })
                .AddJwtBearer("bearer",options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = Configuration["IdentityServer:Clients:MenuPlanner.Client:Authority"];
                    options.Audience = Configuration["IdentityServer:Clients:MenuPlanner.Client:Id"];
                });

            services.AddControllersWithViews();
            services.AddRazorPages();
           


            // Register the Swagger generator, defining 1 or more Swagger documents
            //services.AddSwaggerGen();
            // Swagger Authorization take from https://stackoverflow.com/a/61899245
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "JWT Authorization header using the Bearer scheme." +
                    "Enter 'Bearer' [space] and then your token in the text input below." +
                    "Example: 'Bearer 12345abcdef'"
                });
            });

        }

        private static void IoCSetUp(IServiceCollection services)
        {
            services.AddScoped<IMenuEntityUpdater, MenuEntityUpdater>();
            services.AddScoped<IIngredientEntityUpdater, IngredientEntityUpdater>();
            services.AddScoped<ISearchLogic, SearchLogic>();
            services.AddScoped<IPictureHandler, PictureHandler>();

            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
                
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
                
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            // reach swagger UI by "/swagger/index.html"
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Menu Planner API V1");
            });
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();
            // app.UseIdentityServer();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
