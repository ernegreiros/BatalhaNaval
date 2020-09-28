using BattleshipApi.Match.DML.Enumerados;

namespace BattleshipApi.Match.DML
{
    /// <summary>
    /// Match object
    /// </summary>
    public class Match
    {

        /// <summary>
        /// Id da partida
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Jogador 1
        /// </summary>
        public int Player1 { get; set; }

        /// <summary>
        /// Jogador 2
        /// </summary>
        public int Player2 { get; set; }
        /// <summary>
        /// Status da partida
        /// </summary>
        public MatchStatus StatusDaPartida { get; set; }
    }
}
