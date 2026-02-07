using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//adding the MVC services to the application
builder.Services.AddControllersWithViews();
//using the EF core services to create object of Dbcontext by passing connection string from appsettings.json as the parameters
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//building the application
var app = builder.Build();

//pipeline of the application whose order must be intact
app.UseStaticFiles();
app.UseRouting();
//default route of where the controller is Employee and action is Index(id can be optional)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
