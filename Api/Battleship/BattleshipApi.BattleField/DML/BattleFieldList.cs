using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleshipApi.BattleField.DML
{
    /// <summary>
    /// Battle field list
    /// </summary>
    public class BattleFieldList 
    {
        public List<BattleField> BattleFields { get; set; } = new List<BattleField>();
        /// <summary>
        /// Check datas
        /// </summary>
        public void CheckData()
        {
            if (!BattleFields.Any())
                throw new Exception("Battle field positions count is zero");
            //else if (BattleFields.Count(c => c.PositionObject.X == c.PositionObject.X && c.PositionObject.Y == c.PositionObject.Y) > 1)
            //    throw new Exception("Positions duplicate");
            else
                foreach (BattleField battleField in BattleFields)
                {
                    battleField.CheckData();
                    battleField.PositionObject.CheckData();
                }
        }
    }
}
