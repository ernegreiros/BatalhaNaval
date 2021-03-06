﻿using DataBaseHelper.Interfaces;

namespace BattleshipApi.Base.DAL
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

        /// <summary>
        /// Begin transaction
        /// </summary>
        public void BeginTransaction() { IUnitOfWork.BeginTransaction(); }

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit() { IUnitOfWork.Commit(); }

        /// <summary>
        /// Rollback
        /// </summary>
        public void Rollback() { IUnitOfWork.Rollback(); }
    }
}
