using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PatientsApplication.BusinessLogic.Interfaces;
using PatientsApplication.BusinessLogic.Mapping;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.BusinessLogic.Services;
using PatientsApplication.DataAccess.Context;
using PatientsApplication.DataAccess.Entities;
using PatientsApplication.DataAccess.Interfaces;
using PatientsApplication.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddTransient<IEntityService<PatientDto>, PatientsService<PatientDto>>();
//builder.Services.AddTransient<IEntityRepository<Patient>, PatientsRepository>();
builder.Services.AddTransient<IPatiensRepository, PatientsRepository>();
builder.Services.AddTransient<ILookupRepository, LookUpRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Patients API"
    });
    var basePath = AppContext.BaseDirectory;
    var xmlPath = Path.Combine(basePath, "PatienApi.xml");
    options.IncludeXmlComments(xmlPath);
});


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
