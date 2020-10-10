﻿using BattleshipApi.Base.DAL;
using BattleshipApi.SpecialPower.DML.Intefaces;
using DataBaseHelper.Interfaces;
using System;

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

    }
}
