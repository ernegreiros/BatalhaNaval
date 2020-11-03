using DataBaseHelper.Atributos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.MatchAttacks.DML
{
    [Tabela("MatchAttacks")]
    public class MatchAttacks
    {
        [Coluna("MatchId", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int MatchId { get; set; }
        [Coluna("PosX", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int PosX { get; set; }
        [Coluna("PosY", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int PosY { get; set; }
        [Coluna("Target", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int Target { get; set; }

        public void CheckData()
        {
            if (MatchId < 0)
                throw new ArgumentOutOfRangeException("Match id must be grater than zero");
            if (PosX < 0)
                throw new ArgumentOutOfRangeException("X cannot be lower than zero");
            if (PosY < 0)
                throw new ArgumentOutOfRangeException("Y cannot be lower than zero");
            if (Target < 0)
                throw new ArgumentOutOfRangeException("Target cannot be lower than zero");
        }
    }
}
