using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickBuy.Dominio.Contratos;
using QuickBuy.Repositorio.Contexto;
using QuickBuy.Repositorio.Repositorios;

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
            // Construtor para adicionar configuração
            var builder = new ConfigurationBuilder();
            // (Optinal: Não é opcional = false). reloadOnChance(Recarregar após utilizar alguma página: true).
            builder.AddJsonFile("config.json", optional:false, reloadOnChange: true);
            // Constroi um arquivo de configuração com as chaves e os valores do arquivo config
            // relacionado a cima.
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Configuração que aponta a leitura do arquivo config, que contem a ConnectionString.
            var connectionString = Configuration.GetConnectionString("QuickBuyDB");
            services.AddDbContext<QuickBuyContext>(option =>
                                                        option.UseLazyLoadingProxies() // Permite carregamento automatico de relacionamento de tabelas.
                                                        .UseMySql(connectionString,
                                                                            m => m.MigrationsAssembly("QuickBuy.Repositorio")));

            // Mapeamento de interface e classe concreta.
            // Injeção de dependência
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

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
