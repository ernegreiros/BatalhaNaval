﻿using BattleshipApi.BattleField.DML;
using BattleshipApi.BattleField.DML.Interfaces;
using BattleshipApi.Match.DML.Interfaces;
using BattleshipApi.MatchAttacks.DML.Interfaces;
using BattleshipApi.MatchSpecialPower.DML.Interfaces;
using BattleshipApi.Player.DML.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleshipApi.BattleField.BLL
{
    public class BoBattleField : IBoBattleField
    {
        #region Readonly
        private readonly IDispatcherBattleField IDispatcherBattleField;
        private readonly IBoPlayer IBoPlayer;
        private readonly IBoMatch IBoMatch;
        private readonly IBoMatchSpecialPower IBoMatchSpecialPower;
        private readonly IBoMatchAttacks IBoMatchAttacks;

        public BoBattleField(IDispatcherBattleField iDispatcherBattleField, IBoPlayer iBoPlayer, IBoMatch iBoMatch, IBoMatchSpecialPower iBoMatchSpecialPower, IBoMatchAttacks iBoMatchAttacks)
        {
            IDispatcherBattleField = iDispatcherBattleField;
            IBoPlayer = iBoPlayer;
            IBoMatch = iBoMatch;
            IBoMatchSpecialPower = iBoMatchSpecialPower;
            IBoMatchAttacks = iBoMatchAttacks;
        }

        #endregion

        /// <summary>
        /// Register positions
        /// </summary>
        /// <param name="pBattleFieldsPositions">Positions</param>
        public void RegisterPositions(List<DML.BattleField> pBattleFieldsPositions)
        {
            if (pBattleFieldsPositions == null)
                throw new ArgumentNullException(paramName: nameof(pBattleFieldsPositions), "Battlefield positions cannot be null");
            else if (pBattleFieldsPositions.Any())
            {
                BattleFieldList list = pBattleFieldsPositions as BattleFieldList;

                if (!IBoPlayer.PlayerExists(list.First().Player))
                    throw new Exception("Player do not exists");

                Match.DML.Match currentMatch = IBoMatch.CurrentMatch(list.First().Player);
                if (currentMatch == null)
                    throw new Exception("The player does not have any match");
                else if (currentMatch.ID != list.First().MatchID)
                    throw new Exception("The current match of the player is another");

                list.CheckData();

                try
                {
                    IDispatcherBattleField.BeginTransaction();
                    foreach (DML.BattleField battleField in list)
                    {
                        battleField.Attacked = 0;
                        IDispatcherBattleField.RegisterPosition(battleField);
                    }
                    IDispatcherBattleField.Commit();
                }
                catch (Exception ex)
                {
                    IDispatcherBattleField.Rollback();
                    throw new Exception($"Error on register positions. Original error: {ex.Message}");
                }
            }
            else
                throw new Exception("Battlefield positions count is 0");
        }


        public int AttackPositions(List<DML.BattleField> pBattleFieldsPositions, int? pSpecialPowerId, out bool enemyDefeated)
        {
            if (pBattleFieldsPositions == null)
                throw new ArgumentNullException(paramName: nameof(pBattleFieldsPositions), "Battlefield positions cannot be null");
            else if (pBattleFieldsPositions.Any())
            {
                BattleFieldList list = pBattleFieldsPositions as BattleFieldList;
                List<MatchAttacks.DML.MatchAttacks> matchAttacks = new List<MatchAttacks.DML.MatchAttacks>();

                int targetHited = 0;

                if (!IBoPlayer.PlayerExists(list.First().Player))
                    throw new Exception("Player do not exists");

                Match.DML.Match currentMatch = IBoMatch.CurrentMatch(list.First().Player);
                if (currentMatch == null)
                    throw new Exception("The player does not have any match");
                else if (currentMatch.ID != list.First().MatchID)
                    throw new Exception("The current match of the player is another");

                list.CheckData();

                try
                {
                    IDispatcherBattleField.BeginTransaction();
                    foreach (DML.BattleField battleField in list)
                    {
                        if (targetHited == 1)
                        {
                            IDispatcherBattleField.AttackPosition(battleField);
                            matchAttacks.Add(new MatchAttacks.DML.MatchAttacks()
                            {
                                MatchId = battleField.MatchID,
                                PosX = battleField.PosX,
                                PosY = battleField.PosY,
                                /*O alvo não é quem está atacando*/
                                Target = currentMatch.Player1 == battleField.Player ? currentMatch.Player2 : currentMatch.Player1
                            });
                        }
                        else
                        {
                            targetHited = IDispatcherBattleField.AttackPosition(battleField);
                            matchAttacks.Add(new MatchAttacks.DML.MatchAttacks()
                            {
                                MatchId = battleField.MatchID,
                                PosX = battleField.PosX,
                                PosY = battleField.PosY,
                                /*O alvo não é quem está atacando*/
                                Target = currentMatch.Player1 == battleField.Player ? currentMatch.Player2 : currentMatch.Player1
                            });
                        }
                    }

                    IBoMatchAttacks.RegisterMatchAttacks(matchAttacks);
                    if (pSpecialPowerId != null)
                        IBoMatchSpecialPower.RegisterUseOfSpecialPower(list.First().MatchID, list.First().Player, Convert.ToInt32(pSpecialPowerId));

                    enemyDefeated = PlayerDefeated(currentMatch.ID, currentMatch.Player1 == list.First().Player ? currentMatch.Player2 : currentMatch.Player1);
                    if (enemyDefeated)
                    {
                        IBoMatch.CloseMatch(currentMatch.ID);                        
                    }
                    else
                    {
                        IBoMatch.ChangeCurrentPlayer(currentMatch.ID, currentMatch.Player1 == list.First().Player ? currentMatch.Player2 : currentMatch.Player1);
                    }

                    IDispatcherBattleField.Commit();

                    return targetHited;
                }
                catch (Exception ex)
                {
                    IDispatcherBattleField.Rollback();
                    throw new Exception($"Error on register positions. Original error: {ex.Message}");
                }
            }
            else
                throw new Exception("Battlefield positions count is 0");
        }

        public bool PlayerDefeated(int pMatchId, int pTarget)
        {
            if (pMatchId <= 0)
                throw new ArgumentOutOfRangeException("Match id must be grater than zero");
            if (pTarget <= 0)
                throw new ArgumentOutOfRangeException("Target id must be grater than zero");

            return IDispatcherBattleField.PlayerDefeated(pMatchId, pTarget);
        }
    }
}
