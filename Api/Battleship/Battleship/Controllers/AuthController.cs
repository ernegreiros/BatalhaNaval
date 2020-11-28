using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Battleship.Models.Auth;
using Battleship.Models.Auth.In;
using Battleship.Models.Auth.Out;
using BattleshipApi.JWT.BLL;
using BattleshipApi.JWT.DML;
using BattleshipApi.JWT.DML.Interfaces;
using BattleshipApi.Models;
using BattleshipApi.Player.DML.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Readonly
        private readonly IBoJWT IBoJWT;
        private readonly IBoPlayer IBoPlayer;

        #endregion

        #region Constructor

        public AuthController(IBoJWT iBoJWT, IBoPlayer iBoPlayer)
        {
            IBoJWT = iBoJWT;
            IBoPlayer = iBoPlayer;
        }
        #endregion

        [AllowAnonymous]
        [HttpPost()]
        public OutAuthorizeVM Authorize(InAuthorizeVM pModel)
        {
            OutAuthorizeVM outAuthorizeVM = new OutAuthorizeVM();

            if (ModelState.IsValid)

            {
                try
                {
                    outAuthorizeVM.Token = IBoJWT.WriteToken(new AuthenticationData()
                    {
                        Login = pModel.Login,
                        Password = pModel.Password
                    }, IBoPlayer.PasswordMatch(pModel.Login, pModel.Password));

                    outAuthorizeVM.HttpStatus = StatusCodes.Status200OK;
                    outAuthorizeVM.Message = $"Authenticated!";
                }
                catch (Exception ex)
                {
                    outAuthorizeVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outAuthorizeVM.Message = $"Error when gerenating token! {ex.Message}";
                }
            }
            else
            {
                outAuthorizeVM.Message = "Objeto de entrada não está valido!";
                outAuthorizeVM.HttpStatus = StatusCodes.Status400BadRequest;
            }


            return outAuthorizeVM;
        }


        [Authorize(Policy = BoJWT.SuperUserPolicyName)]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Status: OK com bearer - SUPERUSER";
        }

        [Authorize("Bearer")]
        [HttpGet("get-2")]
        public ActionResult<string> Get2()
        {
            return "Status: OK com bearer";
        }

    }
}
