using Microsoft.EntityFrameworkCore;
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
        builder.Services.AddScoped<IClothingItemInterface, ClothingItemsRepo>();
        builder.Services.AddScoped<IChartBotInterface, ChartBotRepo>();
        builder.Services.AddScoped<ICurrentUser, CurrentUserRepo>();
        builder.Services.AddScoped<ICustomerInterface, CustomerRepo>();
        builder.Services.AddScoped<IOrderInterface, OrderRepo>();
        builder.Services.AddScoped<IOutfitsInterface, OutfitsRepo>();
        builder.Services.AddScoped<IPaymentInterface, PaymentRepo>();
        builder.Services.AddScoped<IProductInterface, ProductRepo>();
        builder.Services.AddScoped<IRoleInterface, RoleRepo>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRepo>();
        builder.Services.AddScoped<IUserInterface, UserRepo>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<IClothingItemService, ClothingItemsService>();
        builder.Services.AddScoped<IChartBotService, ChartBotservice>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IOutfitsService, OutfitsService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IJWTSettingsService, JWTSettingsService>();

        builder.Services.AddScoped<HttpClient>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthentication(); 
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
        app.MapControllers();
       //app.UseAuthentication();
      // app.UseAuthorization();

        app.Run();
    }
}
