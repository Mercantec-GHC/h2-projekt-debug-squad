using Application.Interfaces;
using Application.Rooms.Handlers;
using Application.Guests.Handlers;
using Application.RoomTypes.Handlers;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Application.Bookings.Handlers;
using Application.DayMultipliers.Handlers;
using Application.Pricing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddScoped<CreateRoomHandler>();
            builder.Services.AddScoped<GetAllRoomsHandler>();
            builder.Services.AddScoped<GetRoomByIdHandler>();
            builder.Services.AddScoped<DeleteRoomHandler>();
            builder.Services.AddScoped<EditRoomHandler>();
            builder.Services.AddScoped<GetRoomsFilteredHandler>();
            builder.Services.AddScoped<GetAvailableRoomsHandler>();
            builder.Services.AddScoped<IRoomRepository, EfRoomRepository>();
            builder.Services.AddScoped<AddGuestHandler>();
            builder.Services.AddScoped<GetAllGuestsHandler>();
            builder.Services.AddScoped<GetGuestByIdHandler>();
            builder.Services.AddScoped<DeleteGuestHandler>();
            builder.Services.AddScoped<EditGuestHandler>();
            builder.Services.AddScoped<RegisterGuestHandler>();
            builder.Services.AddScoped<IGuestRepository, EfGuestRepository>();
            builder.Services.AddScoped<GetBookingsByGuestIdHandler>();
            builder.Services.AddScoped<CreateBookingHandler>();
            builder.Services.AddScoped<DeleteBookingHandler>();
            builder.Services.AddScoped<GetAllBookingsHandler>();
            builder.Services.AddScoped<GetBookingByIdHandler>();
            builder.Services.AddScoped<EditBookingHandler>();
            builder.Services.AddScoped<IBookingRepository, EfBookingRepository>();
            builder.Services.AddScoped<EditDayMultiplierHandler>();
            builder.Services.AddScoped<GetAllDayMultipliersHandler>();
            builder.Services.AddScoped<IDayMultiplierRepository, EfDayMultiplierRepository>();
            builder.Services.AddScoped<IRoomPricingService, RoomPricingService>();
            builder.Services.AddScoped<GetAllRoomTypesHandler>();
            builder.Services.AddScoped<EditRoomTypeHandler>();
            builder.Services.AddScoped<GetAvailableRoomTypesHandler>();
            builder.Services.AddScoped<IRoomTypeRepository, EfRoomTypeRepository>();
            builder.Services.AddScoped<GetRoomTypeByIdHandler>();
            builder.Services.AddScoped<GetBookingSummariesHandler>();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddScoped<AssignRoomHandler>();
            builder.Services.AddScoped<ReassignRoomHandler>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                    };
                });
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
