using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Configuration.Hub;
using MomAndBaby.Configuration.SystemConfig;
using MomAndBaby.Service.MessageCommunication;
using MomAndBaby.Utilities.Constants;

var builder = WebApplication.CreateBuilder(args);

// Set up auto mapper.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Set up database context.
//builder.Services.AddDbContext<MomAndBabyContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstant.DefaultDatabase)));

// Set up fluentEmail.
builder.Services.AddFluentEmail(builder.Configuration);

//Setup Authentication


// Set up cookie authentication.
builder.Services.Configure<CookieSetting>(builder.Configuration.GetSection("CookieSetting"));
builder.Services.AddCustomCookie(builder.Configuration);
builder.Services.AddGoogle(builder.Configuration);

// Add services to the container.
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Set up policies authorization.
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("User", policy => policy.RequireClaim(UserClaimType.Role, RoleType.User.ToString()));
    opt.AddPolicy("Staff", policy => policy.RequireClaim(UserClaimType.Role, RoleType.Staff.ToString()));
    opt.AddPolicy("Admin", policy => policy.RequireClaim(UserClaimType.Role, RoleType.Admin.ToString()));
});

// Set up http context accessor.
builder.Services.AddHttpContextAccessor();

// Set up cors
builder.Services.AddCors();
builder.Services.AddSignalR(option =>
{
    option.EnableDetailedErrors = true;
});
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure cors
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(SystemConstant.DefaultPageError);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<ChatHub>(SystemConstant.HubConnection);

app.Run();
