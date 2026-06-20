
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
// Locate this block in your Program.cs file:
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Add this line to force Swagger and API endpoints to use text strings instead of numbers
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });


//services
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();

//adapters
builder.Services.AddScoped<ITaskDataAdapter, TaskDataAdapter>();
builder.Services.AddScoped<IUserDataAdapter, UserDataAdapter>();

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
