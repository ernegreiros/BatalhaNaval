using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatalhaNavalApi.Models.Partida.Saida
{
    /// <summary>
    /// Objeto de retorno do método de iniciar partida
    /// </summary>
    public class OutIniciarPartidaVM : OutBase
    {
        /// <summary>
        /// ID da partida
        /// </summary>
        public int ID { get; set; }
    }
}
