using BattleshipApi.Base.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.BattleField.DML.Interfaces
{
    public interface IDispatcherBattleField : IDispatcherBase
    {
        /// <summary>
        /// Register position
        /// </summary>
        /// <param name="pBattleFieldsPosition">Position</param>
        void RegisterPosition(BattleField pBattleFieldsPosition);
    }
}
