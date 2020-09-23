#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial da classe de conexão com banco de dados base
 */

#endregion
using DataBaseHelper.Interfaces;

namespace BatalhaNavalApi.Base.DAL
{
    /// <summary>
    /// Dispatcher Base
    /// </summary>
    public class DispatcherBase
    {
        /// <summary>
        /// Objeto de conexão com o banco;
        /// </summary>
        protected readonly IUnitOfWork IUnitOfWork;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pIUnitOfWork">Objeto de conexão com o banco</param>
        public DispatcherBase(IUnitOfWork pIUnitOfWork)
        {
            IUnitOfWork = pIUnitOfWork;
        }
    }
}
