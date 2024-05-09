using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//Add scope
//builder.Services.AddScoped<IEmailServices, EmailSerivces>();


//Checking enviroment
bool IsDevelop = builder.Environment.IsDevelopment();

//Get appsettings file
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


//Config DB
builder.Services.AddDbContext<Prn221GroupProjectContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));

/*builder.Services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<Prn221GroupProjectContext>();*/
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<Prn221GroupProjectContext>();
/*builder.Services.AddDefaultIdentity<IdentityUser>()
            .AddEntityFrameworkStores<Prn221GroupProjectContext>()
            .AddDefaultTokenProviders();*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
