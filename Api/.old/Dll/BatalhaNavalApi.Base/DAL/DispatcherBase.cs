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
        /// Constructor
        /// </summary>
        /// <param name="pIUnitOfWork">Connection DB object</param>
        public DispatcherBase(IUnitOfWork pIUnitOfWork)
        {
            IUnitOfWork = pIUnitOfWork;
        }
    }
}
