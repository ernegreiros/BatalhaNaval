using BattleshipApi.Base.DAL;
using BattleshipApi.Ships.DML.Enums;
using BattleshipApi.Ships.DML.Intefaces;
using DataBaseHelper.Interfaces;
using System;
using System.Text;

namespace BattleshipApi.Ships.DAL
{
    public class DispatcherShips : DispatcherBase, IDispatcherShips
    {
        public DispatcherShips(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        public void Create(DML.Ships shipObject)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(shipObject).ToString());
        }

        public void Delete(int shipId)
        {
            IUnitOfWork.Executar($"DELETE FROM Ships WHERE ID = {shipId}");
        }

        public DML.Ships Get(int shipId)
        {
            var row = IUnitOfWork.Consulta($"SELECT * FROM Ships WITH(NOLOCK) WHERE ID = {shipId}").Tables[0].Rows[0];

            var ship = new DML.Ships
            {
                ID = Convert.ToInt32(row["ID"]),
                Name = row["Name"].ToString(),
                Type = (ShipsTypes)row["Type"],
                ImageId = Convert.ToInt32(row["ImageId"]),
                ThemeId = Convert.ToInt32(row["ThemeId"])
            };

            return ship;
        }

        public void Update(DML.Ships shipObject)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"UPDATE Ships ");
            stringBuilder.AppendLine($"SET Name  =  '{shipObject.Name}', ");
            stringBuilder.AppendLine($" Type   =  {(int)shipObject.Type}, ");
            stringBuilder.AppendLine($" ImageId   =  {shipObject.ImageId}, ");
            stringBuilder.AppendLine($" ThemeId   =  {shipObject.ThemeId} ");
            stringBuilder.AppendLine($"WHERE ID ={shipObject.ID}");

            IUnitOfWork.Executar(stringBuilder.ToString());
        }
    }
}
