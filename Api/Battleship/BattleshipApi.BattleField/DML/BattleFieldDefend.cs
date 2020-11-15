using DataBaseHelper.Atributos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.BattleField.DML
{
    [Tabela(pNomeTabela: "BattleFieldDefended")]
    public class BattleFieldDefend
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleFieldDefend()
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
        [Coluna(pNomeColuna: "PlayerID", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int Player { get; set; }

        /// <summary>
        /// Position object (x,y)
        /// </summary>
        public BattleFieldPosition PositionObject { get; set; }

        /// <summary>
        /// Position X
        /// </summary>
        [Coluna(pNomeColuna: "PosX", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int PosX => PositionObject.X;

        /// <summary>
        /// Position Y
        /// </summary>
        [Coluna(pNomeColuna: "PosY", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int PosY => PositionObject.Y;

        /// <summary>
        /// Match ID
        /// </summary>
        [Coluna(pNomeColuna: "MatchID", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int MatchID { get; set; }

        /// <summary>
        /// Attacked
        /// </summary>
        [Coluna(pNomeColuna: "MatchContrl", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int MatchContrl { get; set; } = 0;

        public void CheckData()
        {
            if (MatchID < 0)
                throw new ArgumentOutOfRangeException("Match id cannot be lower than zero");

            if (Player < 0)
                throw new ArgumentOutOfRangeException("Player ID cannot be lower than zero");
        }
    }
}
