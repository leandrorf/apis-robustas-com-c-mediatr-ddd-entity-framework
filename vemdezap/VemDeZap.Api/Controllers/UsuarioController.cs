﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using VemDeZap.Api.Controllers.Base;
using VemDeZap.Api.Security;
using VemDeZap.Domain.Commands.Usuario.AdicionarUsuario;
using VemDeZap.Domain.Commands.Usuario.AutenticarUsuario;
using VemDeZap.Infra.Repositories.Transactions;

namespace VemDeZap.Api.Controllers
{
    [ApiController]
    [Route( "api/[controller]" )]
    public class UsuarioController : BaseCustomController
    {
        private readonly IMediator _mediator;

        public UsuarioController( IMediator mediator, IUnitOfWork unitOfWork )
            : base( unitOfWork )
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route( "Adicionar" )]
        public async Task<IActionResult> Adicionar( [FromBody] AdicionarUsuarioRequest request )
        {
            try
            {
                var response = await _mediator.Send( request, CancellationToken.None );
                return await ResponseAsync( response );
            }
            catch ( Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route( "Autenticar" )]
        public async Task<IActionResult> Autenticar(
           [FromBody] AutenticarUsuarioResquest request,
           [FromServices] SigningConfigurations signingConfigurations,
           [FromServices] TokenConfigurations tokenConfigurations )
        {
            try
            {
                var autenticarUsuarioResponse = await _mediator.Send( request, CancellationToken.None );

                if ( autenticarUsuarioResponse.Autenticado == true )
                {
                    var response = GerarToken( autenticarUsuarioResponse, signingConfigurations, tokenConfigurations );

                    return Ok( response );
                }

                return Ok( autenticarUsuarioResponse );
            }
            catch ( Exception ex )
            {
                return NotFound( ex.Message );
            }
        }

        [NonAction]
        private object GerarToken( AutenticarUsuarioResponse response, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations )
        {
            if ( response.Autenticado == true )
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity( response.Id.ToString( ), "Id" ),
                    new[ ] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        //new Claim(JwtRegisteredClaimNames.UniqueName, response.Usuario)
                        new Claim("Usuario", JsonConvert.SerializeObject(response))
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds( tokenConfigurations.Seconds );

                var handler = new JwtSecurityTokenHandler( );
                var securityToken = handler.CreateToken( new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                } );
                var token = handler.WriteToken( securityToken );

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString( "yyyy-MM-dd HH:mm:ss" ),
                    expiration = dataExpiracao.ToString( "yyyy-MM-dd HH:mm:ss" ),
                    accessToken = token,
                    message = "OK",
                    PrimeiroNome = response.Nome
                };
            }
            else
            {
                return response;
            }
        }
    }
}