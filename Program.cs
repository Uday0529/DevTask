
using DevTask2.Business;
using DevTask2.Business.ServiceInterface;
using DevTask2.DataAdapters;
using DevTask2.DataAdapters.DBContext;
using DevTask2.DataAdapters.IDataAdapter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;






var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
builder.Services.AddScoped<ITaskService, TaskService>();

//adapters
builder.Services.AddScoped<ITaskDataAdapter, TaskDataAdapter>();

//mapper
builder.Services.AddAutoMapper(typeof(DevTask2.Mapping_Repository.Mapper.Mapper));

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseMySql(connectionString,
ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
