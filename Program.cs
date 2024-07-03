using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repository.IRepositories;
using Shop.Repository.IRepositories.Repositories;
using Shop.Repository.IRepositories.Repositoriescc;
using Shop.Service.IService;
using Shop.Service.IService.Services;
using System.Data;
using System.Net.Http.Headers;


/****************************************
*
*
*
***************************************/
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<ShopDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();

        // Add HttpClient with named client "MicroserviceClient"
        builder.Services.AddHttpClient("MicroserviceClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7245/"); // Replace with your actual API base URL
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        // add cors
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                name: "AllowOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register repositories from DataAccess project
        builder.Services.AddScoped<ICountryRepository, CountryRepository>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IUserRepository, UsersInfoRepository>();


        builder.Services.AddScoped<IUserRepository, UsersInfoRepository>();

        // Register services from Services project
        builder.Services.AddScoped<ICountryService, CountryService>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IUsersInfoService, UsersInfoService>();

        // Register your service client
        builder.Services.AddScoped<IDepartmentServiceClient, DepartmentServiceClient>();
        builder.Services.AddScoped<IShopService, ShopService>();


        // Register IDbConnection with the connection string
        builder.Services.AddTransient<IDbConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(options => options.AllowAnyOrigin());

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}