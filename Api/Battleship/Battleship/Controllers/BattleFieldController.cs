using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models.BattleField.In;
using Battleship.Models.BattleField.Out;
using BattleshipApi.BattleField.DML.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleFieldController : ControllerBase
    {
        #region readonly
        private readonly IBoBattleField IBoBattleField;

        public BattleFieldController(IBoBattleField iBoBattleField)
        {
            IBoBattleField = iBoBattleField;
        }
        #endregion


        [HttpPost]
        public OutRegisterPositionsVM RegisterPositions(List<InRegisterPositionsVM> pModel)
        {
            OutRegisterPositionsVM outRegisterPositionsVM = new OutRegisterPositionsVM();

            if (ModelState.IsValid)
            {

                try
                {
                    if (pModel.Any())
                    {
                        IBoBattleField.RegisterPositions(pModel.Select(c => new BattleshipApi.BattleField.DML.BattleField()
                        {
                            MatchID = c.MatchID,
                            Player = c.Player,
                            PositionObject = new BattleshipApi.BattleField.DML.BattleFieldPosition()
                            {
                                X = c.PosX,
                                Y = c.PosY
                            }
                        }).ToList());
                    }
                    else
                    {
                        outRegisterPositionsVM.HttpStatus = StatusCodes.Status400BadRequest;
                        outRegisterPositionsVM.Message = "Positions count is zero";
                    }

                    outRegisterPositionsVM.HttpStatus = StatusCodes.Status201Created;
                    outRegisterPositionsVM.Message = $"Positions successfully registered!";
                }
                catch (Exception ex)
                {
                    outRegisterPositionsVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outRegisterPositionsVM.Message = $"Error when registering positions! {ex.Message}";
                }
            }
            else
            {
                outRegisterPositionsVM.Message = "Objeto de entrada não está valido!";
                outRegisterPositionsVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outRegisterPositionsVM;
        }
    }
}
