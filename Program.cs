using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WardrobeOrganizerApp.AuthenticationSettings;
using WardrobeOrganizerApp.Context;
using WardrobeOrganizerApp.Repositories.Implementation;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Implementation;
using WardrobeOrganizerApp.Services.Interface;

public class program
{
    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("StoreString");
        builder.Services.AddDbContext<StoreContext>(options =>
        options.UseMySQL("server=localhost;user=root;database=wardrobeorganizerappdb;password=password;"));
        builder.Services.AddScoped<ICartInterface, CartRepo>();
        builder.Services.AddScoped<IChartBotInterface, ChartBotRepo>();
        builder.Services.AddScoped<ICurrentUser, CurrentUserRepo>();
        builder.Services.AddScoped<ICustomerInterface, CustomerRepo>();
        builder.Services.AddScoped<IOrderInterface, OrderRepo>();
        builder.Services.AddScoped<IPaymentInterface, PaymentRepo>();
        builder.Services.AddScoped<IProductInterface, ProductRepo>();
        builder.Services.AddScoped<IRoleInterface, RoleRepo>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRepo>();
        builder.Services.AddScoped<IUserInterface, UserRepo>();


        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<IChartBotService, ChartBotservice>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IJWTSettingsService, JWTSettingsService>();

        builder.Services.AddScoped<HttpClient>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));


        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Add Authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JWTSettings>>().Value;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtSettings.Isseur,
                ValidAudience = jwtSettings.Audience,
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("admin", policy =>
            policy.RequireRole("admin"));

            options.AddPolicy("customer", policy =>
            policy.RequireRole("customer"));
            
        });



        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
 {
     c.SwaggerDoc("v1", new OpenApiInfo
     {
         Title = "Your API",
         Version = "v1"
     });

     // Configure Swagger to use the Authorization header
     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {
         Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
         Name = "Authorization",
         In = ParameterLocation.Header,
         Type = SecuritySchemeType.Http,
         Scheme = "bearer",
         BearerFormat = "JWT"
     });

     c.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
     });
 });


        builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
        policy.WithOrigins("https://your-allowed-origin.com") // replace with actual URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()); // Allow credentials if needed
});



        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
    {
        // Set the Swagger UI to be the default page (root URL)
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;  // This makes Swagger UI the default page
    });
        }


        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization(); // Authorization should be after routing
        app.UseAuthentication();
        app.UseCors("AllowSpecificOrigin");
        app.MapControllers();
        //app.UseAuthentication();
        // app.UseAuthorization();

        app.Run();
    }
}
