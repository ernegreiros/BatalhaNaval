using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models.Player.In;
using Battleship.Models.Player.Out;
using BattleshipApi.JWT.BLL;
using BattleshipApi.Models.Player.In;
using BattleshipApi.Models.Player.Out;
using BattleshipApi.Player.DML;
using BattleshipApi.Player.DML.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = BoJWT.NormalUserPolicyName)]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        /// <summary>
        /// Player business object
        /// </summary>
        private readonly IBoPlayer IBoPlayer;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pIBoPlayer"></param>
        public PlayerController(IBoPlayer pIBoPlayer)
        {
            IBoPlayer = pIBoPlayer;
        }
        #endregion


        /// <summary>
        /// Get player
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [Authorize(Policy = BoJWT.NormalUserPolicyName)]
        [HttpGet()]
        public OutGetPlayerVM Get()
        {
            OutGetPlayerVM outGetPlayerVM = new OutGetPlayerVM();

            try
            {
                string userName = User.Claims.GetJWTUserName();

                outGetPlayerVM.Player = IBoPlayer.FindPlayerByUserName(userName);
                outGetPlayerVM.HttpStatus = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                outGetPlayerVM.HttpStatus = StatusCodes.Status400BadRequest;
                outGetPlayerVM.Message = $"Error while getting Player! {ex.Message}";
                outGetPlayerVM.Player = null;
            }

            return outGetPlayerVM;
        }

        [AllowAnonymous]
        [HttpPost]
        public OutCreatePlayerVM Post(InCreatePlayerVM playerObject)
        {
            OutCreatePlayerVM outCreatePlayerVM = new OutCreatePlayerVM();

            if (ModelState.IsValid)
            {

                try
                {
                    IBoPlayer.InsertPlayer(new Player() 
                    {
                        Code  = playerObject.Code,
                        Name = playerObject.Name,
                        Login = playerObject.Login,
                        Password = playerObject.Password,
                        Money = playerObject.Money

                    });

                    outCreatePlayerVM.HttpStatus = StatusCodes.Status201Created;
                    outCreatePlayerVM.Message = $"Player {playerObject.Name} successfully registered!";
                }
                catch (Exception ex)
                {
                    outCreatePlayerVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outCreatePlayerVM.Message = $"Error when registering player! {ex.Message}";
                }
            }
            else
            {
                outCreatePlayerVM.Message = "Objeto de entrada não está valido!";
                outCreatePlayerVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outCreatePlayerVM;
        }

        [HttpPut()]
        public OutPutPlayerVM Put(InPutPlayerVM model)
        {
            OutPutPlayerVM outPutPlayerVM = new OutPutPlayerVM();

            if (ModelState.IsValid)
            {

                try
                {

                    IBoPlayer.Update(new Player()
                    {
                        Login = User.Claims.GetJWTUserName(),
                        Name = model.Name,
                        Password = model.Password
                    });

                    outPutPlayerVM.HttpStatus = StatusCodes.Status201Created;
                    outPutPlayerVM.Message = $"Player {model.Name} successfully Updated!";
                }
                catch (Exception ex)
                {
                    outPutPlayerVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outPutPlayerVM.Message = $"Error when Updating player! {ex.Message}";
                }
            }
            else
            {
                outPutPlayerVM.Message = "Entry Object not valid!";
                outPutPlayerVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outPutPlayerVM;
        }

    }
}
