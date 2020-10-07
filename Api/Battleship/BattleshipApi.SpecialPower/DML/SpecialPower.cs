using BattleshipApi.SpecialPower.DML.Enums;
using DataBaseHelper.Atributos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.SpecialPower.DML
{
    /// <summary>
    /// Special power
    /// </summary>
    [Tabela(pNomeTabela: "SpecialPowers")]
    public class SpecialPower
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of special power
        /// </summary>
        [Coluna(pNomeColuna: "Name", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 30)]
        public string Name { get; set; }

        /// <summary>
        /// Quantifier by type
        /// </summary>
        [Coluna(pNomeColuna: "Quantifier", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Integer)]
        public int Quantifier { get; set; }

        /// <summary>
        /// how much special power costs
        /// </summary>
        [Coluna(pNomeColuna: "Cost", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Float)]
        public double Cost { get; set; }

        /// <summary>
        /// Special power type
        /// </summary>
        [Coluna(pNomeColuna: "Type", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Enum)]
        public SpecialPowerTypes Type { get; set; }

        /// <summary>
        /// Compensation for use and victory in the game
        /// </summary>
        [Coluna(pNomeColuna: "Compensation", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Float)]
        public double Compensation { get; set; }

        /// <summary>
        /// Check the data
        /// </summary>
        public void CheckData()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Name of special power cannot be null");
            else if (Name.Length > 30)
                throw new ArgumentOutOfRangeException(paramName: nameof(Name), message: "Special power name max lenght is 30 positions");

            if (Cost < double.Epsilon)
                throw new ArgumentOutOfRangeException("Special power Cost cannot be lower than zero");

            if (Type == SpecialPowerTypes.NotDefined)
                throw new ArgumentOutOfRangeException("Special power Type cannot be not defined");

            if (Compensation < double.Epsilon)
                throw new ArgumentOutOfRangeException("Compensation cannot be lower than zero");
        }
    }
}
