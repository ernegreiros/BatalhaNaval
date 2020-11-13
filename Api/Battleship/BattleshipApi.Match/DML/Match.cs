using BattleshipApi.Match.DML.Enumerados;
using DataBaseHelper.Atributos;

namespace BattleshipApi.Match.DML
{
    /// <summary>
    /// Match object
    /// </summary>
    [Tabela(pNomeTabela:"Match")]
    public class Match
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Match()
        {
            Status = DML.Enumerados.MatchStatus.WaitingBattleField;
            CurrentPlayer = Player1;
            Winner = null;
            Controle = 1;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Id da partida
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Jogador 1
        /// </summary>
        [Coluna(pNomeColuna: "Player1", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int Player1 { get; set; }

        /// <summary>
        /// Jogador 2
        /// </summary>
        [Coluna(pNomeColuna: "Player2", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int Player2 { get; set; }

        /// <summary>
        /// Status da partida
        /// </summary>
        [Coluna(pNomeColuna: "Status", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Enum)]
        public DML.Enumerados.MatchStatus Status { get; set; }

        [Coluna(pNomeColuna: "CurrentPlayer", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int CurrentPlayer { get; set; }

        public int? Winner { get; set; }

        [Coluna(pNomeColuna: "MatchContrl", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int Controle { get; set; }
        #endregion
    }
}
