using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models.BattleField.In;
using Battleship.Models.BattleField.Out;
using BattleshipApi.BattleField.DML.Interfaces;
using BattleshipApi.JWT.BLL;
using BattleshipApi.Match.DML;
using BattleshipApi.Match.DML.Interfaces;
using BattleshipApi.MatchAttacks.DML.Interfaces;
using BattleshipApi.Player.DML.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IBoPlayer IBoPlayer;
        private readonly IBoMatch IBoMatch;
        private readonly IBoMatchAttacks IBoMatchAttacks;

        public BattleFieldController(IBoBattleField iBoBattleField, IBoPlayer iBoPlayer, IBoMatch iBoMatch, IBoMatchAttacks iBoMatchAttacks)
        {
            IBoBattleField = iBoBattleField;
            IBoPlayer = iBoPlayer;
            IBoMatch = iBoMatch;
            IBoMatchAttacks = iBoMatchAttacks;
        }

        #endregion

        [Authorize(Policy = BoJWT.NormalUserPolicyName)]
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

        [HttpPost]
        [Authorize(Policy = BoJWT.NormalUserPolicyName)]
        public OutAttackPlayerVM AttackPositions(List<InAttackPlayerVM> pAttackPositions, int? pSpecialPower)
        {
            OutAttackPlayerVM outAttackPlayerVM = new OutAttackPlayerVM();

            if (pAttackPositions.Any())
            {
                BattleshipApi.Player.DML.Player player = IBoPlayer.FindPlayerByUserName(User.Claims.GetJWTUserName());

                if (player == null || player.ID <= 0)
                {
                    outAttackPlayerVM.Message = "Player not found";
                    outAttackPlayerVM.HttpStatus = StatusCodes.Status400BadRequest;
                }
                else
                {
                    Match currentMatch = IBoMatch.CurrentMatch(player.ID);

                    if (currentMatch == null || currentMatch.ID <= 0)
                    {
                        outAttackPlayerVM.Message = "Match not found";
                        outAttackPlayerVM.HttpStatus = StatusCodes.Status400BadRequest;
                    }
                    else
                    {
                        outAttackPlayerVM.HitTarget = IBoBattleField.AttackPositions(pAttackPositions.Select(c => new BattleshipApi.BattleField.DML.BattleField()
                        {
                            Attacked = 0,
                            MatchID = currentMatch.ID,
                            Player = player.ID,
                            PositionObject = new BattleshipApi.BattleField.DML.BattleFieldPosition()
                            {
                                X = c.PosX,
                                Y = c.PosY
                            }
                        }).ToList(), pSpecialPower, out bool enemyDefeated) == 1;
                        outAttackPlayerVM.EnemyDefeated = enemyDefeated;
                        outAttackPlayerVM.PositionsAttacked = IBoMatchAttacks.PositionsAttacked(currentMatch.ID, currentMatch.CurrentPlayer == currentMatch.Player1 ? currentMatch.Player2 : currentMatch.Player1);
                    }
                }
            }
            else
            {
                outAttackPlayerVM.Message = "No attack positions";
                outAttackPlayerVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outAttackPlayerVM;
        }

        [HttpPost]
        [Authorize(Policy = BoJWT.NormalUserPolicyName)]
        public OutDefendPositionsVM DefendPositions(List<InDefendPositionVM> pDeffendPositions, int pSpecialPower)
        {
            OutDefendPositionsVM outDefendPlayer = new OutDefendPositionsVM();

            if (pDeffendPositions.Any())
            {
                BattleshipApi.Player.DML.Player player = IBoPlayer.FindPlayerByUserName(User.Claims.GetJWTUserName());

                if (player == null || player.ID <= 0)
                {
                    outDefendPlayer.Message = "Player not found";
                    outDefendPlayer.HttpStatus = StatusCodes.Status400BadRequest;
                }
                else
                {
                    Match currentMatch = IBoMatch.CurrentMatch(player.ID);

                    if (currentMatch == null || currentMatch.ID <= 0)
                    {
                        outDefendPlayer.Message = "Match not found";
                        outDefendPlayer.HttpStatus = StatusCodes.Status400BadRequest;
                    }
                    else
                    {
                        IBoBattleField.DeffendPositions(pDeffendPositions.Select(c => new BattleshipApi.BattleField.DML.BattleField()
                        {
                            MatchID = currentMatch.ID,
                            Player = player.ID,
                            PositionObject = new BattleshipApi.BattleField.DML.BattleFieldPosition()
                            {
                                X = c.PosX,
                                Y = c.PosY
                            }
                        }).ToList(), pSpecialPower);
                    }
                }
            }
            else
            {
                outDefendPlayer.Message = "No deffend positions";
                outDefendPlayer.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outDefendPlayer;
        } 

        [HttpPost]
        [Authorize(Policy = BoJWT.NormalUserPolicyName)]
        public OutShowPositionsVM ShowPositions(List<InShowPositionsVM> pShowPositions, int pSpecialPower)
        {
            OutShowPositionsVM outDefendPlayer = new OutShowPositionsVM();

            if (pShowPositions.Any())
            {
                BattleshipApi.Player.DML.Player player = IBoPlayer.FindPlayerByUserName(User.Claims.GetJWTUserName());

                if (player == null || player.ID <= 0)
                {
                    outDefendPlayer.Message = "Player not found";
                    outDefendPlayer.HttpStatus = StatusCodes.Status400BadRequest;
                }
                else
                {
                    Match currentMatch = IBoMatch.CurrentMatch(player.ID);

                    if (currentMatch == null || currentMatch.ID <= 0)
                    {
                        outDefendPlayer.Message = "Match not found";
                        outDefendPlayer.HttpStatus = StatusCodes.Status400BadRequest;
                    }
                    else
                    {
                        outDefendPlayer.Positions = IBoBattleField.ShowPositions(pShowPositions.Select(c => new BattleshipApi.BattleField.DML.BattleField()
                        {
                            MatchID = currentMatch.ID,
                            Player = player.ID,
                            PositionObject = new BattleshipApi.BattleField.DML.BattleFieldPosition()
                            {
                                X = c.PosX,
                                Y = c.PosY
                            }
                        }).ToList(), pSpecialPower);
                    }
                }
            }
            else
            {
                outDefendPlayer.Message = "No positions to show";
                outDefendPlayer.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outDefendPlayer;
        }
    }
}
