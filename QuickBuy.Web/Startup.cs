using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//Classe responsável por:
//receber string de conexão do dbc.
//Ingeção de dependência.
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
                app.UseDeveloperExceptionPage(); //Adição de middleware.
            }
            else
            {
                app.UseExceptionHandler("/Error"); //Adição de tratamento de erros.
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); //Adição automática de protocólo https ou https.
            }

            app.UseHttpsRedirection(); //Método de redirecionamento para uso de https.
            app.UseStaticFiles(); //Método de uso de arquivos estáticos (Recursos:CSS, JS..).
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

                    //Após inicializar via CMD e rodar a aplicação localmente passo o
                    //endereço do servidor local que roda minha aplicação Angular.
                    //Através desse modo, minha aplicação é compilada bem mais rápida, pois a requisição é local.
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                }
            });
        }
    }
}
