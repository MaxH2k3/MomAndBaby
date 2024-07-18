using Microsoft.EntityFrameworkCore;
using MomAndBaby;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.Configuration.SystemConfig;
using MomAndBaby.Middleware;
using MomAndBaby.Repository.Uow;
using MomAndBaby.Service;
using MomAndBaby.Service.MessageCommunication;
using MomAndBaby.Service.OrderService;
using MomAndBaby.Service.Service;
using MomAndBaby.Service.Service.Email;
using MomAndBaby.Service.Service.PayPalService;
using MomAndBaby.Subscribe;
using MomAndBaby.Utilities.Constants;

var builder = WebApplication.CreateBuilder(args);

// Set up auto mapper.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Set up database context.
builder.Services.AddDbContext<MomAndBabyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstant.DefaultDatabase)!));

// Set up fluentEmail.
builder.Services.AddFluentEmail(builder.Configuration);

// Set up cookie authentication.
builder.Services.AddCustomCookie(builder.Configuration);
builder.Services.AddGoogle(builder.Configuration);

// Add services to the container.
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IPayPalService, PayPalService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<SubscribeNotification>();

builder.Services.AddSession();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Set up policies authorization.
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Customer", policy => policy.RequireClaim(UserClaimType.Role, ((int)RoleType.Customer).ToString()));
    opt.AddPolicy("Staff", policy => policy.RequireClaim(UserClaimType.Role, ((int)RoleType.Staff).ToString()));
    opt.AddPolicy("Admin", policy => policy.RequireClaim(UserClaimType.Role, ((int)RoleType.Admin).ToString()));
    opt.AddPolicy("SA", policy => policy.RequireClaim(UserClaimType.Role, ((int)RoleType.Staff).ToString(), ((int)RoleType.Admin).ToString()));
    opt.AddPolicy("NonAdmin", policy =>
    {
        policy.RequireAssertion(context =>
        {
            // Allow access if the user is not authenticated (anonymous)
            if (context.User.Identity!.IsAuthenticated)
            {
                // Allow access if the user is authenticated and does not have the Admin role
                return !context.User.HasClaim(UserClaimType.Role, ((int)RoleType.Admin).ToString());
            }

            return true;
        });
    });
    opt.AddPolicy("User", policy =>
    {
        policy.RequireAssertion(context =>
        {
            if(context.User.Identity!.IsAuthenticated)
            {
                return true;
            }

            return false;
        });
    });
});

// Set up http context accessor.
builder.Services.AddHttpContextAccessor();

// Set up cors
builder.Services.AddCors();
builder.Services.AddSignalR(option =>
{
    option.EnableDetailedErrors = true;
});
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Dashboard/Body", "SA");
    options.Conventions.AuthorizeFolder("/Main/Body", "NonAdmin");
    options.Conventions.AuthorizePage("/Main/Body/CartDetail", "User");
});

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

app.UseStatusCodePagesWithRedirects(SystemConstant.DefaultPageError);

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.MapHub<ChatHub>(SystemConstant.ChatHubConnection);

app.MapHub<NotificationHub>(SystemConstant.NotificationHubConnection);

app.UseNotificationTableDependency();

app.Run();
