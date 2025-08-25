using msAlertaMongoDB.Entity;
using msAlertaMongoDB.Mapper;
using msAlertaMongoDB.Repository;
using msAlertaMongoDB.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
builder.Services.AddScoped<IAlertaService, AlertaService>();

builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<AlertaProfile>();
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
