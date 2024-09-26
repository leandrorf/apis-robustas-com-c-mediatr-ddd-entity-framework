using VemDeZap.Api;
using VemDeZap.Domain.Interfaces.Repositories;
using VemDeZap.Infra.Repositories.Base;
using VemDeZap.Infra.Repositories.Transactions;
using VemDeZap.Infra.Repositories;
using VemDeZap.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers( );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( );

//builder.Services.AddMediatR( cfg => cfg.RegisterServicesFromAssemblies( typeof( Program ).Assembly ) );

builder.Services.AddInjectionApplication( );

var connectionString = builder.Configuration.GetConnectionString( "Default" );

builder.Services.AddDbContext<DataContext>( options =>
{
    options.UseMySql( connectionString, ServerVersion.AutoDetect( connectionString ) );
} );

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>( );
builder.Services.AddTransient<IUsuarioRepository, UsuarioRespository>( );
builder.Services.AddTransient<IGrupoRepository, GrupoRespository>( );

builder.Services.ConfigureAuthentication( );

var app = builder.Build( );

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment( ) )
{
    app.UseSwagger( );
    app.UseSwaggerUI( );
}

app.UseHttpsRedirection( );

app.UseAuthorization( );

app.MapControllers( );

app.Run( );
