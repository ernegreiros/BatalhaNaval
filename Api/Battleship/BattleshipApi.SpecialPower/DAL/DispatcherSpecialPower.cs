using BattleshipApi.Base.DAL;
using BattleshipApi.SpecialPower.DML.Enums;
using BattleshipApi.SpecialPower.DML.Intefaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BattleshipApi.SpecialPower.DAL
{
    public class DispatcherSpecialPower : DispatcherBase, IDispatcherSpecialPower
    {
        public DispatcherSpecialPower(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        /// <summary>
        /// Create a special power
        /// </summary>
        /// <param name="pSpecialPower">Special power</param>
        public void Create(DML.SpecialPower pSpecialPower)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(pSpecialPower).ToString());
        }

        public DML.SpecialPower Get(int specialPowerId)
        {
            var row = IUnitOfWork.Consulta($"SELECT * FROM SpecialPowers WITH(NOLOCK) WHERE ID = {specialPowerId}").Tables[0].Rows[0];

            var specialPower = new DML.SpecialPower
            {
                ID = Convert.ToInt32(row["ID"]),
                Name = row["Name"].ToString(),
                Quantifier = Convert.ToInt32(row["Quantifier"]),
                Cost = Convert.ToDouble(row["Cost"]),
                Type = (DML.Enums.SpecialPowerTypes)row["Type"],
                Compensation = Convert.ToDouble(row["Compensation"])
            };

            return specialPower;
        }

        public List<DML.SpecialPower> GetAll()
        {
            var specialPowers = new List<DML.SpecialPower>();

            var rows = IUnitOfWork.Consulta($"SELECT * FROM SpecialPowers WITH(NOLOCK)").Tables[0].Rows;

            foreach (DataRow row in rows)
            {
                specialPowers.Add(
                    new DML.SpecialPower
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Name = row["Name"].ToString(),
                        Quantifier = Convert.ToInt32(row["Quantifier"]),
                        Cost = Convert.ToDouble(row["Cost"]),
                        Type = (DML.Enums.SpecialPowerTypes)row["Type"],
                        Compensation = Convert.ToDouble(row["Compensation"])
                    });
            }


            return specialPowers;
        }

        public void Update(DML.SpecialPower specialPower)
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"UPDATE SpecialPowers ");
            stringBuilder.AppendLine($"SET Name  =  '{specialPower.Name}', ");
            stringBuilder.AppendLine($" Cost   =  {specialPower.Cost}, ");
            stringBuilder.AppendLine($" Quantifier   =  {specialPower.Quantifier}, ");
            stringBuilder.AppendLine($" Type   =  {(int)specialPower.Type}, ");
            stringBuilder.AppendLine($" Compensation   =  {specialPower.Compensation} ");
            stringBuilder.AppendLine($"WHERE ID ={specialPower.ID}");

            IUnitOfWork.Executar(stringBuilder.ToString());
        }

        public void Delete(int specialPowerId)
        {
            IUnitOfWork.Executar($"DELETE FROM SpecialPowers WHERE ID = {specialPowerId}");
        }
    }
}
