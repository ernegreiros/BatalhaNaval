using BattleshipApi.Ships.DML.Enums;
using DataBaseHelper.Atributos;
using System;

namespace BattleshipApi.Ships.DML
{

    [Tabela(pNomeTabela: "Ships")]
    public class Ships
    {
        public int ID { get; set; }

        [Coluna(pNomeColuna: "Name", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 30)]
        public string Name { get; set; }

        [Coluna(pNomeColuna: "Type", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Enum)]
        public ShipsTypes Type { get; set; }

        [Coluna(pNomeColuna: "ImagePath", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 8000)]
        public string ImagePath { get; set; }

        [Coluna(pNomeColuna: "ThemeId", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int ThemeId { get; set; }


        public void CheckData()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Name of ship cannot be null");
            else if (Name.Length > 30)
                throw new ArgumentOutOfRangeException(paramName: nameof(Name), message: "Ship name max lenght is 30 positions");

            if (string.IsNullOrEmpty(ImagePath))
                throw new ArgumentOutOfRangeException("Invalid ImagePath");

            if (ThemeId <= 0)
                throw new ArgumentOutOfRangeException("Invalid ThemeId");

            if (Type == ShipsTypes.NotDefined)
                throw new ArgumentOutOfRangeException("Ship Type cannot be not defined");
        }
    }
}
