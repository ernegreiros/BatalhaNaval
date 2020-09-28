using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipApi.Models.Match.Out
{
    /// <summary>
    /// Objeto de retorno do método de iniciar partida
    /// </summary>
    public class OutInitMatchVM : OutBase
    {
        /// <summary>
        /// ID da partida
        /// </summary>
        public int ID { get; set; }
    }
}
