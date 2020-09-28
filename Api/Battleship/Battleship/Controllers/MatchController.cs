using System;
using BattleshipApi.Models.Match.In;
using BattleshipApi.Models.Match.Out;
using BattleshipApi.Match.DML.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}