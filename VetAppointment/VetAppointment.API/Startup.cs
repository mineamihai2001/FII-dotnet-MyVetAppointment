using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure;
using VetAppointment.Infrastructure.Generics;
using VetAppointment.Infrastructure.Generics.GenericRepositories;
using VetAppointment.API.Helpers;
using AutoMapper;
using VetAppointment.Application;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddApplicationService();
builder.Services.AddInfrastructureServices(builder.Configuration);

//!!!!Am comentat liniile de mai jos deoarece am refactorizat mecanismul de Dependecy Injection
//builder.Services.AddDbContext<DatabaseContext>(
//    options => options.UseSqlite(
//       builder.Configuration.GetConnectionString("VetAppointmentDB"),
//       b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));

//builder.Services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
//builder.Services.AddScoped<IRepository<Bill>, BillRepository>();
//builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
//builder.Services.AddScoped<IRepository<Medicine>, MedicineRepository>();
//builder.Services.AddScoped<IRepository<Medic>, MedicRepository>();
//builder.Services.AddScoped<IRepository<Nurse>, NurseRepository>();
//builder.Services.AddScoped<IRepository<Patient>, PatientRepository>();
//builder.Services.AddScoped<IRepository<Room>, RoomRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();


//public partial class Startup { }