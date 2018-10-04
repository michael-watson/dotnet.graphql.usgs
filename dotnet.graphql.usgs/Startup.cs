using dotnet.graphql.usgs.Types;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet.graphql.usgs
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UsgsSchema>();
            services.AddSingleton<UsgsQuery>();
            services.AddSingleton<SiteType>();
            services.AddSingleton<SiteMetaType>();
            services.AddSingleton<FlowInfoType>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
            })
            .AddDataLoader();

            services.AddMvc();
            services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SiteDatabase")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseGraphQL<UsgsSchema>("/graphql");
            app.UseGraphiQLServer(new GraphQL.Server.Ui.GraphiQL.GraphiQLOptions
            {
                GraphiQLPath = "/graphiql",
                GraphQLEndPoint = "/graphql"
            });

            app.UseMvc();
        }
    }
}
