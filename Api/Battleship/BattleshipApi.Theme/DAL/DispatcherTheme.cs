using BattleshipApi.Base.DAL;
using BattleshipApi.Theme.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BattleshipApi.Theme.DAL
{
    public class DispatcherTheme : DispatcherBase, IDispatcherTheme
    {
        public DispatcherTheme(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        public void Create(DML.Theme theme)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(theme).ToString());
        }

        public void Delete(int pId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID INT");
            stringBuilder.AppendLine($"SET @ID = {pId}");
            stringBuilder.AppendLine("DELETE FROM Themes WHERE ID = @ID");

            IUnitOfWork.Executar(stringBuilder.ToString());
        }

        public DML.Theme Get(int pId)
        {
            return Get().FirstOrDefault(theme => theme.Id == pId);
        }

        public List<DML.Theme> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  ID,");
            stringBuilder.AppendLine("  NAME,");
            stringBuilder.AppendLine("  DESCRIPTION,");
            stringBuilder.AppendLine("  IMAGEPATH");
            stringBuilder.AppendLine("FROM Themes WITH(NOLOCK)");

            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<DML.Theme> themes = new List<DML.Theme>();

                DML.Theme theme;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    theme = new DML.Theme();

                    if (row["ID"] != DBNull.Value)
                        theme.Id = Convert.ToInt32(row["ID"]);
                    if (row["NAME"] != DBNull.Value)
                        theme.Name = row["NAME"].ToString();
                    if (row["DESCRIPTION"] != DBNull.Value)
                        theme.Description = row["DESCRIPTION"].ToString();
                    if (row["IMAGEPATH"] != DBNull.Value)
                        theme.ImagePath = row["IMAGEPATH"].ToString();

                    themes.Add(theme);
                }
                return themes;
            }
            return new List<DML.Theme>();
        }

        public void Update(DML.Theme theme)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID INT,   ");
            stringBuilder.AppendLine("  @NAME VARCHAR(30),");
            stringBuilder.AppendLine("  @DESCRIPTION VARCHAR(100),");
            stringBuilder.AppendLine("  @IMAGEPATH VARCHAR(200)");

            stringBuilder.AppendLine($"SET @ID = {theme.Id}");
            stringBuilder.AppendLine($"SET @NAME = '{theme.Name}'");
            stringBuilder.AppendLine($"SET @DESCRIPTION = '{theme.Description}'");
            stringBuilder.AppendLine($"SET @IMAGEPATH = '{theme.ImagePath}'");

            stringBuilder.AppendLine("UPDATE Themes");
            stringBuilder.AppendLine("SET");
            stringBuilder.AppendLine("  NAME = @NAME,");
            stringBuilder.AppendLine("  DESCRIPTION = @DESCRIPTION,");
            stringBuilder.AppendLine("  IMAGEPATH = @IMAGEPATH");
            stringBuilder.AppendLine("WHERE ID = @ID");

            IUnitOfWork.Executar(stringBuilder.ToString());
        }
    }
}
