using Microsoft.EntityFrameworkCore;
using PatientsApplication.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/users", async (HttpContext _context, ApplicationDbContext db) => { db.Active.Add(new PatientsApplication.DataAccess.Entities.Active() { Id = Guid.Parse("11111AA1-1111-11AA-1111-1A111111AAA1"), Name = "ntcn"}); db.SaveChanges(); });
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
