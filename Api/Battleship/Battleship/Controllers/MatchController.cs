using System;
using BattleshipApi.Models.Match.In;
using BattleshipApi.Models.Match.Out;
using BattleshipApi.Match.DML.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BattleshipApi.JWT.BLL;
using Battleship.Models.Match.Out;
using System.Linq;
using Battleship;

namespace BattleshipApi.Controllers
{
    /// <summary>
    /// Match Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pIBoPartida">Business class of match</param>
        public MatchController(IBoMatch pIBoPartida)
        {
            IBoMatch = pIBoPartida;
        }
                
        /// <summary>
        /// Interface de negócio de partida
        /// </summary>
        private readonly IBoMatch IBoMatch;
       
        /// <summary>
        /// Method that creates a match
        /// </summary>
        /// <param name="pModel">In Object</param>
        /// <returns>Return Object</returns>
        /// <remarks>Method that creates a match</remarks>
        [Authorize(BoJWT.NormalUserPolicyName)]
        [HttpPost]
        public OutInitMatchVM CreateMatch(InInitMatchVM pModel)
        {
            OutInitMatchVM outIniciarPartidaVM = new OutInitMatchVM();

            if (ModelState.IsValid)
            {

                try
                {
                    outIniciarPartidaVM.ID = IBoMatch.CreateMatch(new Match.DML.Match()
                    {
                        Player1 = pModel.Player1,
                        Player2 = pModel.Player2
                    });
                    outIniciarPartidaVM.HttpStatus = StatusCodes.Status201Created;
                    outIniciarPartidaVM.Message = $"Partida {outIniciarPartidaVM.ID} iniciada com sucesso!";
                }
                catch (Exception ex)
                {
                    outIniciarPartidaVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outIniciarPartidaVM.Message = $"Erro ao iniciar a partida! {ex.Message}";
                }
            }
            else
            {
                outIniciarPartidaVM.Message = "Objeto de entrada não está valido!";
                outIniciarPartidaVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outIniciarPartidaVM;
        }

        [Authorize(BoJWT.NormalUserPolicyName)]
        [HttpGet]
        public OutCurrentMatchVM CurrentMatch()
        {
            OutCurrentMatchVM outCurrentMatchVM = new OutCurrentMatchVM();

            try
            {
                string userName = User.Claims.GetJWTUserName();

                outCurrentMatchVM.Match = IBoMatch.CurrentMatch(userName);
                outCurrentMatchVM.HttpStatus = StatusCodes.Status200OK;
                outCurrentMatchVM.Message = "OK";
            }
            catch (Exception ex)
            {
                outCurrentMatchVM.HttpStatus = StatusCodes.Status400BadRequest;
                outCurrentMatchVM.Message = $"Error while getting current match! {ex.Message}";
                outCurrentMatchVM.Match = null;
            }

            return outCurrentMatchVM;
        }
    }
}