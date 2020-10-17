using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models.Themes.In;
using Battleship.Models.Themes.Out;
using BattleshipApi.JWT.BLL;
using BattleshipApi.Theme.DML;
using BattleshipApi.Theme.DML.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = BoJWT.NormalUserPolicyName)]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        private readonly IBoTheme IBoThemes;

        public ThemesController(IBoTheme iBoThemes)
        {
            IBoThemes = iBoThemes;
        }

        [Authorize(Policy = BoJWT.NormalUserPolicyName)]
        [HttpGet]
        public OutGetThemesVM Get()
        {
            OutGetThemesVM outGetShipVM = new OutGetThemesVM();

            try
            {
                outGetShipVM.Themes = IBoThemes.Get();
                outGetShipVM.HttpStatus = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                outGetShipVM.HttpStatus = StatusCodes.Status400BadRequest;
                outGetShipVM.Message = $"Error while getting Themes! {ex.Message}";
                outGetShipVM.Themes = null;
            }

            return outGetShipVM;
        }

        [Authorize(Policy = BoJWT.NormalUserPolicyName)]
        [HttpGet]
        [Route("{pId}")]
        public OutGetThemeVM Get(int pId)
        {
            OutGetThemeVM outGetShipVM = new OutGetThemeVM();

            try
            {
                outGetShipVM.Theme = IBoThemes.Get(pId);
                outGetShipVM.HttpStatus = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                outGetShipVM.HttpStatus = StatusCodes.Status400BadRequest;
                outGetShipVM.Message = $"Error while getting Theme! {ex.Message}";
                outGetShipVM.Theme = null;
            }

            return outGetShipVM;
        }

        [Authorize(Policy = BoJWT.SuperUserPolicyName)]
        [HttpPost]
        public OutPostThemeVM Post(InPostThemesVM pModel)
        {
            OutPostThemeVM outPostThemes = new OutPostThemeVM();

            if (ModelState.IsValid)
            {

                try
                {
                    IBoThemes.Create(new Theme()
                    {
                        Name = pModel.Name,
                        Description = pModel.Description,
                        ImagePath = pModel.ImagePath
                    });

                    outPostThemes.HttpStatus = StatusCodes.Status201Created;
                    outPostThemes.Message = $"Theme {pModel.Name} successfully registered!";
                }
                catch (Exception ex)
                {
                    outPostThemes.HttpStatus = StatusCodes.Status400BadRequest;
                    outPostThemes.Message = $"Error when registering theme! {ex.Message}";
                }
            }
            else
            {
                outPostThemes.Message = "Entry Object not valid!";
                outPostThemes.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outPostThemes;
        }

        [Authorize(Policy = BoJWT.SuperUserPolicyName)]
        [HttpPut]
        public OutUpdateThemeVM Put(InUpdateThemeVM pModel)
        {
            OutUpdateThemeVM outUpdateShipVM = new OutUpdateThemeVM();

            if (ModelState.IsValid)
            {

                try
                {
                    IBoThemes.Update(new Theme()
                    {
                        Id = Convert.ToInt32(pModel.Id),
                        ImagePath = pModel.ImagePath,
                        Description = pModel.Description,
                        Name = pModel.Name
                    });

                    outUpdateShipVM.HttpStatus = StatusCodes.Status201Created;
                    outUpdateShipVM.Message = $"Theme {pModel.Name} successfully Updated!";
                }
                catch (Exception ex)
                {
                    outUpdateShipVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outUpdateShipVM.Message = $"Error when updating Theme! {ex.Message}";
                }
            }
            else
            {
                outUpdateShipVM.Message = "Entry Object not valid!";
                outUpdateShipVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outUpdateShipVM;
        }

        [Authorize(Policy = BoJWT.SuperUserPolicyName)]
        [HttpDelete("{pId}")]
        public OutDeleteThemeVM Delete(int pId)
        {
            OutDeleteThemeVM outDeleteTheme = new OutDeleteThemeVM();

            try
            {
                IBoThemes.Delete(pId);

                outDeleteTheme.HttpStatus = StatusCodes.Status200OK;
                outDeleteTheme.Message = $"Theme successfully Deleted!";
            }
            catch (Exception ex)
            {
                outDeleteTheme.HttpStatus = StatusCodes.Status400BadRequest;
                outDeleteTheme.Message = $"Error when Deleting Theme! {ex.Message}";
            }


            return outDeleteTheme;

        }

    }
}
