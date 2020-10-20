using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//Classe respons�vel por:
//receber string de conex�o do dbc.
//Inge��o de depend�ncia.
//Pipe-line do aspnetcore.
namespace QuickBuy.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //Adi��o de middleware.
            }
            else
            {
                app.UseExceptionHandler("/Error"); //Adi��o de tratamento de erros.
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); //Adi��o autom�tica de protoc�lo https ou https.
            }

            app.UseHttpsRedirection(); //M�todo de redirecionamento para uso de https.
            app.UseStaticFiles(); //M�todo de uso de arquivos est�ticos (Recursos:CSS, JS..).
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //Starta o Angular junto com dotnetcore, para facilitar.
                    //acaba, algumas vezes, dando erros.
                    //spa.UseAngularCliServer(npmScript: "start");

                    //Ap�s inicializar via CMD e rodar a aplica��o localmente passo o
                    //endere�o do servidor local que roda minha aplica��o Angular.
                    //Atrav�s desse modo, minha aplica��o � compilada bem mais r�pida, pois a requisi��o � local.
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                }
            });
        }
    }
}
