using BigBangAss2.Interfaces;
using BigBangAss2.Models;
using BigBangAss2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HospitalContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Conn"));
});
builder.Services.AddScoped<IRepo<User, int>, UserRepo>();
builder.Services.AddScoped<IRepo<Doctor, int>, DoctorRepo>();
builder.Services.AddScoped<IRepo<Patient, int>, PatientRepo>();
builder.Services.AddScoped<IManageService,ManageService>();
builder.Services.AddScoped<IGenerateToken,GenerateTokenService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("ReactCors", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
