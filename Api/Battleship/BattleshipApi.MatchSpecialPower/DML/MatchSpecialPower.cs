using DataBaseHelper.Atributos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.MatchSpecialPower.DML
{
    [Tabela("MatchSpecialPowers")]
    public class MatchSpecialPower
    {
        public int ID { get; set; }
        
        [Coluna("MatchId", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int MatchId { get; set; }

        [Coluna("PlayerId", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int PlayerId { get; set; }

        [Coluna("SpecialPowerId", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int SpecialPowerId { get; set; }

        [Coluna("Used", DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int Used { get; set; } = 0;

        public void CheckData()
        {
            if (MatchId < 0)
                throw new ArgumentOutOfRangeException("Match id must be grater than zero");

            if (PlayerId < 0)
                throw new ArgumentOutOfRangeException("Player id must be grater than zero");

            if (SpecialPowerId < 0)
                throw new ArgumentOutOfRangeException("Special power id must be grater than zero");
        }
    }
}
