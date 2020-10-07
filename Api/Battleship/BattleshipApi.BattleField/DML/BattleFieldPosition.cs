using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.BattleField.DML
{
    /// <summary>
    /// Position (X,Y)
    /// </summary>
    public class BattleFieldPosition
    {
        /// <summary>
        /// X
        /// </summary>
        public int X { get; set; }
        
        /// <summary>
        /// Y
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Check datas
        /// </summary>
        public void CheckData()
        {
            if (X < 0)
                throw new ArgumentOutOfRangeException("X cannot be lower than zero");
            if (Y < 0)
                throw new ArgumentOutOfRangeException("Y cannot be lower than zero");
        }
    }
}
