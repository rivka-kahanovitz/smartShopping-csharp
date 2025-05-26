using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mock;
using Repository.Entities;
using Repository.Interfaces;
using System.Text;

namespace SmartShoppingApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //        var builder = WebApplication.CreateBuilder(args);

            //        // חיבור למסד נתונים
            //        builder.Services.AddDbContext<DataBase>(options =>
            //            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //        // שירותי בקרה ו־Swagger
            //        builder.Services.AddControllers();
            //        builder.Services.AddEndpointsApiExplorer();
            //        builder.Services.AddSwaggerGen(c =>
            //        {
            //            c.SwaggerDoc("v1", new() { Title = "SmartShoppingApplication", Version = "v1" });

            //            // הגדרת סכמת אבטחה מסוג Bearer
            //            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            //            {
            //                Name = "Authorization",
            //                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            //                Scheme = "Bearer",
            //                BearerFormat = "JWT",
            //                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            //                Description = "הכניסי כאן את ה־JWT Token. לדוגמה: Bearer {token}"
            //            });

            //            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            //{
            //    {
            //        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            //        {
            //            Reference = new Microsoft.OpenApi.Models.OpenApiReference
            //            {
            //                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
            //                Id = "Bearer"
            //            }
            //        },
            //        Array.Empty<string>()
            //    }
            //});
            //        });


            //        //  הגדרת אימות בטוקן
            //        var secretKey = builder.Configuration["Jwt:Key"] ?? "ThisIsAReallyStrongSecretKey123456789!";
            //        builder.Services.AddAuthentication(options =>
            //        {
            //            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //        })
            //        .AddJwtBearer(options =>
            //        {
            //            options.TokenValidationParameters = new TokenValidationParameters
            //            {
            //                ValidateIssuer = false,
            //                ValidateAudience = false,
            //                ValidateLifetime = true,
            //                ValidateIssuerSigningKey = true,
            //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            //            };
            //        });

            //        builder.Services.AddAuthorization();

            //        var app = builder.Build();

            //        // הרצת Swagger בסביבת פיתוח
            //        if (app.Environment.IsDevelopment())
            //        {
            //            app.UseSwagger();
            //            app.UseSwaggerUI();
            //        }

            //        app.UseHttpsRedirection();

            //        // קריאות לסדר נכון: קודם אימות, אחר כך הרשאות
            //        app.UseAuthentication();
            //        app.UseAuthorization();

            //        app.MapControllers();
            //        app.Run();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //הזרקת תלות

            builder.Services.AddDbContext<IContext, DataBase>();
            builder.Services.AddServiceExtension();



            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                option.TokenValidationParameters = new TokenValidationParameters()
                {

                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

                }
                );

            // enable cors
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            var app = builder.Build();
            //app.UseDeveloperExceptionPage(); // --------------


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //הרשאות
            app.UseAuthentication();
            //אימות
            app.UseAuthorization();

            // enable cors
            app.UseCors(MyAllowSpecificOrigins);
            // enable cors

            app.MapControllers();

            app.Run();




        }
    }
}
