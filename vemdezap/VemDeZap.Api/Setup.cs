using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using VemDeZap.Api.Security;

namespace VemDeZap.Api
{
    public static class Setup
    {
        private const string ISSUER = "c1f51f42";
        private const string AUDIENCE = "c6bbbb645024";

        public static void ConfigureAuthentication( this IServiceCollection services )
        {
            //Configuração do Token
            var signingConfigurations = new SigningConfigurations( );
            services.AddSingleton( signingConfigurations );

            var tokenConfigurations = new TokenConfigurations
            {
                Audience = AUDIENCE,
                Issuer = ISSUER,
                Seconds = int.Parse( TimeSpan.FromDays( 1 ).TotalSeconds.ToString( ) )
            };
            services.AddSingleton( tokenConfigurations );

            services.AddAuthentication( authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            } ).AddJwtBearer( bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.SigningCredentials.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            } );

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization( auth =>
            {
                auth.AddPolicy( "Bearer", new AuthorizationPolicyBuilder( )
                    .AddAuthenticationSchemes( JwtBearerDefaults.AuthenticationScheme‌​ )
                    .RequireAuthenticatedUser( ).Build( ) );
            } );

            //Para todas as requisições serem necessaria o token, para um endpoint não exisgir o token
            //deve colocar o [AllowAnonymous]
            //Caso remova essa linha, para todas as requisições que precisar de token, deve colocar
            //o atributo [Authorize("Bearer")]
            services.AddMvc( config =>
            {
                var policy = new AuthorizationPolicyBuilder( )
                    .AddAuthenticationSchemes( JwtBearerDefaults.AuthenticationScheme‌​ )
                    .RequireAuthenticatedUser( ).Build( );

                config.Filters.Add( new AuthorizeFilter( policy ) );
            } );

            //Para todas as requisições serem necessaria o token, para um endpoint não exisgir o token
            //deve colocar o [AllowAnonymous]
            //Caso remova essa linha, para todas as requisições que precisar de token, deve colocar
            //o atributo [Authorize("Bearer")]
            services.AddMvc( config =>
            {
                var policy = new AuthorizationPolicyBuilder( )
                    .AddAuthenticationSchemes( JwtBearerDefaults.AuthenticationScheme‌​ )
                    .RequireAuthenticatedUser( ).Build( );

                config.Filters.Add( new AuthorizeFilter( policy ) );
            } );

            services.AddCors( );
        }

        
    }
}