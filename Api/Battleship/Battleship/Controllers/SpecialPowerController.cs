using Battleship.Models.SpecialPower.In;
using Battleship.Models.SpecialPower.Out;
using BattleshipApi.JWT.BLL;
using BattleshipApi.SpecialPower.DML;
using BattleshipApi.SpecialPower.DML.Enums;
using BattleshipApi.SpecialPower.DML.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = BoJWT.NormalUserPolicyName)]
    [ApiController]
    public class SpecialPowerController : ControllerBase
    {
        #region Readonly
        private readonly IBoSpecialPower IBoSpecialPower;
        #endregion

        public SpecialPowerController(IBoSpecialPower pIBoSpecialPower)
        {
            IBoSpecialPower = pIBoSpecialPower;
        }

        [HttpGet]
        [Authorize(Policy = BoJWT.NormalUserPolicyName)]
        [Route("{specialPowerId}")]
        public OutGetSpecialPowerVM Get(int specialPowerId)
        {
            OutGetSpecialPowerVM outGetSpecialPowerVM = new OutGetSpecialPowerVM();

            try
            {
                outGetSpecialPowerVM.SpecialPower = IBoSpecialPower.Get(specialPowerId); ;
                outGetSpecialPowerVM.HttpStatus = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                outGetSpecialPowerVM.HttpStatus = StatusCodes.Status400BadRequest;
                outGetSpecialPowerVM.Message = $"Error while getting Special Power! {ex.Message}";
                outGetSpecialPowerVM.SpecialPower = null;
            }

            return outGetSpecialPowerVM;
        }

        [HttpGet]
        [Authorize(Policy = BoJWT.NormalUserPolicyName)]
        public List<SpecialPower> Get()
        {
            return IBoSpecialPower.GetAll();
        }


        [HttpPut]
        [Authorize(Policy = BoJWT.SuperUserPolicyName)]
        public OutUpdateSpecialPowerVM Put(InUpdateSpecialPowerVM model)
        {
            OutUpdateSpecialPowerVM outUpdateSpecialPowerVM = new OutUpdateSpecialPowerVM();

            if (ModelState.IsValid)
            {

                try
                {
                    var specialPower = IBoSpecialPower.Get(model.ID);

                    specialPower.Cost = model.Cost ?? specialPower.Cost;
                    specialPower.Name = model.Name ?? specialPower.Name;
                    specialPower.Quantifier = model.Quantifier ?? specialPower.Quantifier;
                    specialPower.Type = model.Type ?? specialPower.Type;
                    specialPower.Compensation = model.Compensation ?? specialPower.Compensation;

                    IBoSpecialPower.Update(specialPower);

                    outUpdateSpecialPowerVM.HttpStatus = StatusCodes.Status201Created;
                    outUpdateSpecialPowerVM.Message = $"Special Power {model.Name} successfully Updated!";
                }
                catch (Exception ex)
                {
                    outUpdateSpecialPowerVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outUpdateSpecialPowerVM.Message = $"Error when Updating special power! {ex.Message}";
                }
            }
            else
            {
                outUpdateSpecialPowerVM.Message = "Entry Object not valid!";
                outUpdateSpecialPowerVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outUpdateSpecialPowerVM;
        }

        [HttpPost]
        [Authorize(Policy = BoJWT.SuperUserPolicyName)]
        public OutCreateSpecialPowerVM Post(InCreateSpecialPowerVM pModel)
        {
            OutCreateSpecialPowerVM outCreateSpecialPowerVM = new OutCreateSpecialPowerVM();

            if (ModelState.IsValid)
            {

                try
                {
                    IBoSpecialPower.Create(new BattleshipApi.SpecialPower.DML.SpecialPower()
                    {
                        Cost = pModel.Cost,
                        Name = pModel.Name,
                        Quantifier = Convert.ToInt32(pModel.Quantifier),
                        Type = (SpecialPowerTypes)pModel.Type,
                        Compensation = pModel.Compensation
                    });

                    outCreateSpecialPowerVM.HttpStatus = StatusCodes.Status201Created;
                    outCreateSpecialPowerVM.Message = $"Special Power {pModel.Name} successfully registered!";
                }
                catch (Exception ex)
                {
                    outCreateSpecialPowerVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outCreateSpecialPowerVM.Message = $"Error when registering special power! {ex.Message}";
                }
            }
            else
            {
                outCreateSpecialPowerVM.Message = "Entry Object not valid!";
                outCreateSpecialPowerVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outCreateSpecialPowerVM;
        }


        [HttpDelete("{specialPowerId}")]
        [Authorize(Policy = BoJWT.SuperUserPolicyName)]
        public OutDeleteSpecialPowerVM Delete(int specialPowerId)
        {
            OutDeleteSpecialPowerVM outDeleteSpecialPower = new OutDeleteSpecialPowerVM();

            try
            {
                IBoSpecialPower.Delete(specialPowerId);

                outDeleteSpecialPower.HttpStatus = StatusCodes.Status201Created;
                outDeleteSpecialPower.Message = $"Special Power successfully Deleted!";
            }
            catch (Exception ex)
            {
                outDeleteSpecialPower.HttpStatus = StatusCodes.Status400BadRequest;
                outDeleteSpecialPower.Message = $"Error when Deleting special power! {ex.Message}";
            }


            return outDeleteSpecialPower;

        }

    }
}
