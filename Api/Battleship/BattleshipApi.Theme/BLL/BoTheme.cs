using BattleshipApi.Theme.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.Theme.BLL
{
    public class BoTheme : IBoTheme
    {
        private readonly IDispatcherTheme IDispatcherTheme;

        public BoTheme(IDispatcherTheme iDispatcherTheme)
        {
            IDispatcherTheme = iDispatcherTheme;
        }

        public void Create(DML.Theme theme)
        {
            if (theme == null)
                throw new ArgumentNullException(paramName: nameof(theme), message: "Theme is required");

            theme.CheckData();
            IDispatcherTheme.Create(theme);
        }

        public void Delete(int pId)
        {
            if (pId <= 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(pId), message: "ID cannot be lower ou equal zero");
            IDispatcherTheme.Delete(pId);
        }

        public DML.Theme Get(int pId)
        {
            if (pId <= 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(pId), message: "ID cannot be lower ou equal zero");
            return IDispatcherTheme.Get(pId);
        }

        public List<DML.Theme> Get()
        {
            return IDispatcherTheme.Get();
        }

        public void Update(DML.Theme theme)
        {
            if (theme.Id <= 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(theme.Id), message: "ID cannot be lower ou equal zero");

            if (theme == null)
                throw new ArgumentNullException(paramName: nameof(theme), message: "Theme is required");

            DML.Theme themeOld = Get(theme.Id);

            theme.Name = theme.Name ?? themeOld.Name;
            theme.Description = theme.Description ?? themeOld.Description;
            theme.ImagePath = theme.ImagePath ?? themeOld.ImagePath;

            theme.CheckData();
            IDispatcherTheme.Update(theme);
        }
    }
}
