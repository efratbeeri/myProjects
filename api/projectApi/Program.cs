
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using System;
using Serilog;
using Microsoft.AspNetCore.Cors.Infrastructure;
using projectApi.BL;
using projectApi.DAL;
using projectApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// הגדרת Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    //.WriteTo.c
    .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting up the API...");

    //builder.Services.AddDbContext<OrdersDBContex>(option => option.UseSqlServer("Data Source = DESKTOP-SOMQ3KC; Initial Catalog = Project; Integrated Security = True; Encrypt = True; Trust Server Certificate=True"));
    builder.Services.AddDbContext<OrdersDBContex>(option => option.UseSqlServer("Data Source = SRV2\\PUPILS; Initial Catalog = final_pro_215602301; Integrated Security = True; Encrypt = True; Trust Server Certificate=True"));

    builder.Services.AddControllers();

    // Add services to the container.
    builder.Services.AddScoped<IDonorDal, DonorDal>();
    builder.Services.AddScoped<IDonorService, DonorService>();
    builder.Services.AddScoped<IPresentDal, PresentDal>();
    builder.Services.AddScoped<IPresentService, PresentService>();
    builder.Services.AddScoped<IUseDal, UserDal>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ICartDal, CartDal>();
    builder.Services.AddScoped<ICartService, CartService>();
    builder.Services.AddScoped<IPurchasesDal, PurchasesDal>();
    builder.Services.AddScoped<IPurchasesService, PurchasesService>();
    builder.Services.AddScoped<IAuthService,AuthService>();
    builder.Services.AddScoped<IAuthDal, AuthDal>();
    builder.Services.AddScoped<IWinnersService, WinnersService>();
    builder.Services.AddScoped<IWinnersDal, WinnersDal>();
    builder.Services.AddScoped<FileHandler>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddControllers().AddJsonOptions(x =>
       x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
                            builder =>
                            {
                                builder.WithOrigins("http://localhost:4200"
                                    , "development web site")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                               
                            });
    });


    //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //.AddJwtBearer(options =>
    //{
    //    options.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuer = true,
    //        ValidateAudience = true,
    //        ValidateLifetime = true,
    //        ValidateIssuerSigningKey = true,
    //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
    //        ValidAudience = builder.Configuration["Jwt:Audience"],
    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    //    };
    //});
    var app = builder.Build();
    app.UseCors("CorsPolicy");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();    
    app.UseAuthentication(); // Middleware לאימות
    app.UseAuthorization();
    app.MapControllers();
    app.UseCors("CorsPolicy");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
