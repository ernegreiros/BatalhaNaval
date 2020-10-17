using Battleship.Models.Ships.In;
using Battleship.Models.Ships.Out;
using BattleshipApi.JWT.BLL;
using BattleshipApi.Ships.DML;
using BattleshipApi.Ships.DML.Enums;
using BattleshipApi.Ships.DML.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = BoJWT.NormalUserPolicyName)]
    [ApiController]
    public class ShipsController : ControllerBase
    {
        private readonly IBoShips IBoShips;

        public ShipsController(IBoShips pIBoShips)
        {
            IBoShips = pIBoShips;
        }

        [HttpGet]
        [Authorize(Policy = BoJWT.NormalUserPolicyName)]
        [Route("{shipId}")]
        public OutGetShipVM Get(int shipId)
        {
            OutGetShipVM outGetShipVM = new OutGetShipVM();

            try
            {
                outGetShipVM.Ship =  IBoShips.Get(shipId);
                outGetShipVM.HttpStatus = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                outGetShipVM.HttpStatus = StatusCodes.Status400BadRequest;
                outGetShipVM.Message = $"Error while getting Ship! {ex.Message}";
                outGetShipVM.Ship = null;
            }

            return outGetShipVM;
        }

        [Authorize(Policy = BoJWT.SuperUserPolicyName)]
        [HttpPost]
        public OutCreateShipVM Post(InCreateShipVM shipObject)
        {
            OutCreateShipVM outCreateShipVM = new OutCreateShipVM();

            if (ModelState.IsValid)
            {

                try
                {
                    IBoShips.Create(new Ships()
                    {
                        Name = shipObject.Name,
                        Type = (ShipsTypes)shipObject.Type,
                        ThemeId = Convert.ToInt32(shipObject.ThemeId),
                        ImageId = Convert.ToInt32(shipObject.ImageId)
                    });

                    outCreateShipVM.HttpStatus = StatusCodes.Status201Created;
                    outCreateShipVM.Message = $"Ship {shipObject.Name} successfully registered!";
                }
                catch (Exception ex)
                {
                    outCreateShipVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outCreateShipVM.Message = $"Error when registering Ship! {ex.Message}";
                }
            }
            else
            {
                outCreateShipVM.Message = "Entry Object not valid!";
                outCreateShipVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outCreateShipVM;
        }

        [Authorize(Policy = BoJWT.SuperUserPolicyName)]
        [HttpPut]
        public OutUpdateShipVM Put(InUpdateShipVM shipObject)
        {
            OutUpdateShipVM outUpdateShipVM = new OutUpdateShipVM();

            if (ModelState.IsValid)
            {

                try
                {
                    var ship = IBoShips.Get(shipObject.ID);

                    ship.Name = shipObject.Name ?? ship.Name;
                    ship.Type = shipObject.Type ?? ship.Type;
                    ship.ImageId = shipObject.ImageId ?? ship.ImageId;
                    ship.ThemeId = shipObject.ThemeId ?? ship.ThemeId;

                    IBoShips.Update(ship);

                    outUpdateShipVM.HttpStatus = StatusCodes.Status200OK;
                    outUpdateShipVM.Message = $"Ship {shipObject.Name} successfully Updated!";
                }
                catch (Exception ex)
                {
                    outUpdateShipVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outUpdateShipVM.Message = $"Error when updating Ship! {ex.Message}";
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
        [HttpDelete("{shipId}")]
        public OutDeleteShipVM Delete(int shipId)
        {
            OutDeleteShipVM outDeleteShipVM = new OutDeleteShipVM();

            try
            {
                IBoShips.Delete(shipId);

                outDeleteShipVM.HttpStatus = StatusCodes.Status201Created;
                outDeleteShipVM.Message = $"Ship successfully Deleted!";
            }
            catch (Exception ex)
            {
                outDeleteShipVM.HttpStatus = StatusCodes.Status400BadRequest;
                outDeleteShipVM.Message = $"Error when Deleting Ship! {ex.Message}";
            }


            return outDeleteShipVM;

        }
    }
}
