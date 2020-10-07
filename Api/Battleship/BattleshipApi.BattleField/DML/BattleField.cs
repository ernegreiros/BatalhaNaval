using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.BattleField.DML
{
    /// <summary>
    /// Battlefield
    /// </summary>
    public class BattleField
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleField()
        {
            PositionObject = new BattleFieldPosition();
        }
        #endregion

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Player
        /// </summary>
        public int Player { get; set; }
        
        /// <summary>
        /// Position object (x,y)
        /// </summary>
        public BattleFieldPosition PositionObject { get; set; }
        
        /// <summary>
        /// Position X
        /// </summary>
        public int PosX => PositionObject.X;
        
        /// <summary>
        /// Position Y
        /// </summary>
        public int PosY => PositionObject.Y;
        
        /// <summary>
        /// Match ID
        /// </summary>
        public int MatchID { get; set; }

        public void CheckData()
        {
            if (MatchID < 0)
                throw new ArgumentOutOfRangeException("Match id cannot be lower than zero");

            if (Player < 0)
                throw new ArgumentOutOfRangeException("Player ID cannot be lower than zero");
        }
    }
}
