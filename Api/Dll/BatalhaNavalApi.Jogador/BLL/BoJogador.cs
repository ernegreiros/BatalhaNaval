using BatalhaNavalApi.Jogador.DML.Interfaces;
using System;

namespace BatalhaNavalApi.Jogador.BLL
{
    /// <summary>
    /// Classe de negócio de jogador
    /// </summary>
    public class BoJogador : IBoJogador
    {
        /// <summary>
        /// Verifica se o jogador existe
        /// </summary>
        /// <param name="pIdJogador">Id do jogador</param>
        /// <returns>Se o jogador existe ou não</returns>
        public bool JogadorExiste(int pIdJogador)
        {
            if (pIdJogador <= 0)
                throw new Exception("ID do jogador inválido");

            return true;
        }

    }
}
