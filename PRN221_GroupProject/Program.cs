using Microsoft.EntityFrameworkCore;
using PRN_GroupProject.Services;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository;
using PRN221_GroupProject.Repository.Products;
using PRN221_GroupProject.Repository.ProductCategories;
using Microsoft.AspNetCore.Identity;
using PRN221_GroupProject.Repository.Products;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.File;
using PRN221_GroupProject.Repository.Users;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation()
    .AddMvcOptions(options =>
    {
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
            _ => "The field is required.");
    }); ;
builder.Services.AddControllersWithViews();

//Add scope
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<ISenderEmail, SenderEmail>();
//builder.Services.AddScoped<IEmailServices, EmailSerivces>();
builder.Services.AddScoped<IProductCategorieRepository, ProductCategorieRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFileUploadRepository, FileUploadRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();






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

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<Prn221GroupProjectContext>()
        .AddDefaultTokenProviders(); ;

// Add authorization services with a specific policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy =>
        policy.RequireRole("admin"));
});


//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF1cXmhPYVJ2WmFZfVpgdVRMYl5bRXBPMyBoS35RckVmWH5fcXZXR2daVU1y");

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
