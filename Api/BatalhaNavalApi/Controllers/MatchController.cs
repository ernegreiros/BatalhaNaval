#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial do controlador de partida
 */

/*
 * Nome: Edvandro Negreiros
 * Data: 23/09/2020
 * Descrição: Renomear métodos
 */

#endregion
using System;
using BatalhaNavalApi.Models.Partida.Entrada;
using BatalhaNavalApi.Models.Partida.Saida;
using BatalhaNavalApi.Partida.DML.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BatalhaNavalApi.Controllers
{
    /// <summary>
    /// Controlador de partida
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pIBoPartida">Classe de negócio de partida</param>
        public MatchController(IBoMatch pIBoPartida)
        {
            IBoPartida = pIBoPartida;
        }
        #endregion

        #region Propriedades readonly
        /// <summary>
        /// Interface de negócio de partida
        /// </summary>
        private readonly IBoMatch IBoPartida;
        #endregion

        #region Métodos
        /// <summary>
        /// Método que inicia a partida
        /// </summary>
        /// <param name="pModel">Objeto de entrada</param>
        /// <returns>Objeto com o ID da partida</returns>
        /// <remarks>Método que inicia a partida</remarks>
        public OutInitMatchVM IniciarPartida(InitMatchVM pModel)
        {
            OutInitMatchVM outIniciarPartidaVM = new OutInitMatchVM();

            if (ModelState.IsValid)
            {

                try
                {
                    outIniciarPartidaVM.ID = IBoPartida.IniciarPartida(new Partida.DML.Match()
                    {
                        Jogador1 = pModel.Jogador1,
                        Jogador2 = pModel.Jogador2
                    });
                    outIniciarPartidaVM.StatusHttp = StatusCodes.Status201Created;
                    outIniciarPartidaVM.Mensagem = $"Partida {outIniciarPartidaVM.ID} iniciada com sucesso!";
                }
                catch (Exception ex)
                {
                    outIniciarPartidaVM.StatusHttp = StatusCodes.Status400BadRequest;
                    outIniciarPartidaVM.Mensagem = $"Erro ao iniciar a partida! {ex.Message}";
                }
            }
            else
            {
                outIniciarPartidaVM.Mensagem = "Objeto de entrada não está valido!";
                outIniciarPartidaVM.StatusHttp = StatusCodes.Status400BadRequest;
            }

            return outIniciarPartidaVM;
        }
        #endregion

    }
}