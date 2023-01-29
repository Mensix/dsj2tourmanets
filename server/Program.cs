using Dsj2TournamentsServer;
using Dsj2TournamentsServer.Repositories;
using Dsj2TournamentsServer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddSimpleConsole(x => { x.IncludeScopes = true; x.TimestampFormat = "hh:mm:ss "; x.UseUtcTimestamp = true; });
builder.Services.AddControllers();
builder.Services.AddDbContext<Dsj2TournamentsServerDbContext>(x => x.UseNpgsql(builder.Configuration["Dsj2TournamentsServer:ConnectionString"]).UseSnakeCaseNamingConvention());
builder.Services.AddScoped<IJumpRepository, JumpRepository>();
builder.Services.AddScoped<IJumpService, JumpService>();
builder.Services.AddScoped<IReplayService, ReplayService>();
builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthorization();
app.MapControllers();
app.Run();