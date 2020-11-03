using BattleshipApi.BattleField.DML;
using BattleshipApi.BattleField.DML.Interfaces;
using BattleshipApi.Match.DML.Interfaces;
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

        public BoBattleField(IDispatcherBattleField iDispatcherBattleField, IBoPlayer iBoPlayer, IBoMatch iBoMatch)
        {
            IDispatcherBattleField = iDispatcherBattleField;
            IBoPlayer = iBoPlayer;
            IBoMatch = iBoMatch;
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


        public int AttackPositions(List<DML.BattleField> pBattleFieldsPositions)
        {
            if (pBattleFieldsPositions == null)
                throw new ArgumentNullException(paramName: nameof(pBattleFieldsPositions), "Battlefield positions cannot be null");
            else if (pBattleFieldsPositions.Any())
            {
                BattleFieldList list = pBattleFieldsPositions as BattleFieldList;
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
                        }
                        else
                            targetHited = IDispatcherBattleField.AttackPosition(battleField);
                        
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
    }
}
