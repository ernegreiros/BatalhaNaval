using DataBaseHelper.Atributos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.Theme.DML
{
    [Tabela("Themes")]
    public class Theme
    {
        [Coluna(pNomeColuna:"NAME", DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo:30)]
        public string Name { get; set; }
        [Coluna(pNomeColuna: "DESCRIPTION", DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 100)]
        public string Description { get; set; }
        [Coluna(pNomeColuna: "IMAGEPATH", DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 8000)]
        public string ImagePath { get; set; }
        public int Id { get; set; }

        internal void CheckData()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException(paramName: nameof(Name), message: "Name is required");
            else if (Name.Length > 30)
                throw new ArgumentOutOfRangeException(paramName: nameof(Name), message: "Name max length is 30 caracters");

            if (string.IsNullOrEmpty(Description))
                throw new ArgumentNullException(paramName: nameof(Description), message: "Description is required");
            else if (Description.Length > 100)
                throw new ArgumentOutOfRangeException(paramName: nameof(Description), message: "Description max length is 100 caracters");

            if (string.IsNullOrEmpty(ImagePath))
                throw new ArgumentNullException(paramName: nameof(ImagePath), message: "ImagePath is required");

        }
    }
}
