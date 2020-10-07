using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.Base.DAL.Interfaces
{
    public interface IDispatcherBase
    {
        /// <summary>
        /// Begin transaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback
        /// </summary>
        void Rollback();
    }
}
