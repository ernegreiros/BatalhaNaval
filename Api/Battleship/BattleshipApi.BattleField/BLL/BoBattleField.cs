using BattleshipApi.BattleField.DML;
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
                BattleFieldList list = new BattleFieldList();

                list.BattleFields.AddRange(pBattleFieldsPositions);

                if (!IBoPlayer.PlayerExists(list.BattleFields.First().Player))
                    throw new Exception("Player do not exists");

                Match.DML.Match currentMatch = IBoMatch.CurrentMatch(list.BattleFields.First().Player);
                if (currentMatch == null)
                    throw new Exception("The player does not have any match");
                else if (currentMatch.ID != list.BattleFields.First().MatchID)
                    throw new Exception("The current match of the player is another");

                list.CheckData();

                try
                {
                    IDispatcherBattleField.BeginTransaction();
                    foreach (DML.BattleField battleField in list.BattleFields)
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
                BattleFieldList list = new BattleFieldList();

                list.BattleFields.AddRange(pBattleFieldsPositions);

                List<MatchAttacks.DML.MatchAttacks> matchAttacks = new List<MatchAttacks.DML.MatchAttacks>();

                int targetHited = 0;

                if (!IBoPlayer.PlayerExists(list.BattleFields.First().Player))
                    throw new Exception("Player do not exists");

                Match.DML.Match currentMatch = IBoMatch.CurrentMatch(list.BattleFields.First().Player);
                if (currentMatch == null)
                    throw new Exception("The player does not have any match");
                else if (currentMatch.ID != list.BattleFields.First().MatchID)
                    throw new Exception("The current match of the player is another");

                list.CheckData();

                try
                {
                    IDispatcherBattleField.BeginTransaction();
                    foreach (DML.BattleField battleField in list.BattleFields)
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
                        IBoMatchSpecialPower.RegisterUseOfSpecialPower(list.BattleFields.First().MatchID, list.BattleFields.First().Player, Convert.ToInt32(pSpecialPowerId));

                    enemyDefeated = PlayerDefeated(currentMatch.ID, currentMatch.CurrentPlayer == currentMatch.Player1 ? currentMatch.Player2 : currentMatch.Player1);
                    if (enemyDefeated)
                    {
                        IBoMatch.CloseMatch(currentMatch.ID);
                    }
                    else if (targetHited == 0)
                    {
                        IBoMatch.ChangeCurrentPlayer(currentMatch.ID, currentMatch.Player1 == list.BattleFields.First().Player ? currentMatch.Player2 : currentMatch.Player1);
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

        public void DeffendPositions(List<DML.BattleField> lists, int pSpecialPower)
        {
            if (lists == null)
                throw new ArgumentNullException(paramName: nameof(lists), "Battlefield positions cannot be null");
            else if (lists.Any())
            {
                List<BattleFieldDefend> positions = lists.Select(c => new BattleFieldDefend()
                {
                    Id = c.Id,
                    MatchContrl = 0,
                    MatchID = c.MatchID,
                    Player = c.Player,
                    PositionObject = new BattleFieldPosition()
                    {
                        X = c.PositionObject.X,
                        Y = c.PositionObject.Y
                    }
                }).ToList();

                if (!IBoPlayer.PlayerExists(positions.First().Player))
                    throw new Exception("Player do not exists");

                Match.DML.Match currentMatch = IBoMatch.CurrentMatch(positions.First().Player);
                if (currentMatch == null)
                    throw new Exception("The player does not have any match");
                else if (currentMatch.ID != positions.First().MatchID)
                    throw new Exception("The current match of the player is another");

                /*Defende o próximo controle, pois o próximo jogador não pode atacar se for nesse controle*/
                int controle = currentMatch.Controle + 1;
                positions.ForEach(p => { p.MatchContrl = controle; IDispatcherBattleField.DeffendPosition(p); });

                IBoMatchSpecialPower.RegisterUseOfSpecialPower(currentMatch.ID, currentMatch.CurrentPlayer, pSpecialPower);
            }
        }

        public List<DML.BattleField> ShowPositions(List<DML.BattleField> lists, int pSpecialPower)
        {
            if (lists == null)
            {
                throw new ArgumentNullException(paramName: nameof(lists), "Battlefield positions cannot be null");
            }
            else if (lists.Any())
            {
                List<DML.BattleField> retorno = new List<DML.BattleField>();
                List<BattleFieldDefend> positions = lists.Select(c => new BattleFieldDefend()
                {
                    Id = c.Id,
                    MatchContrl = 0,
                    MatchID = c.MatchID,
                    Player = c.Player,
                    PositionObject = new BattleFieldPosition()
                    {
                        X = c.PositionObject.X,
                        Y = c.PositionObject.Y
                    }
                }).ToList();

                if (!IBoPlayer.PlayerExists(positions.First().Player))
                    throw new Exception("Player do not exists");

                Match.DML.Match currentMatch = IBoMatch.CurrentMatch(positions.First().Player);
                if (currentMatch == null)
                    throw new Exception("The player does not have any match");
                else if (currentMatch.ID != positions.First().MatchID)
                    throw new Exception("The current match of the player is another");

                positions.ForEach(p =>
                {
                    p.Player = currentMatch.CurrentPlayer == currentMatch.Player1 ? currentMatch.Player2 : currentMatch.Player1;
                    retorno.Add(IDispatcherBattleField.ShowPosition(p));
                });

                IBoMatchSpecialPower.RegisterUseOfSpecialPower(currentMatch.ID, currentMatch.CurrentPlayer, pSpecialPower);
                return retorno;
            }
            return null;
        }

        public List<DML.BattleField> Get(int playerID)
        {
            return IDispatcherBattleField.Get(playerID);
        }
    }
}
