using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models.SpecialPower.In;
using Battleship.Models.SpecialPower.Out;
using BattleshipApi.SpecialPower.DML;
using BattleshipApi.SpecialPower.DML.Enums;
using BattleshipApi.SpecialPower.DML.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialPowerController : ControllerBase
    {
        #region Readonly
        private readonly IBoSpecialPower IBoSpecialPower;
        #endregion


        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pIBoSpecialPower"></param>
        public SpecialPowerController(IBoSpecialPower pIBoSpecialPower)
        {
            IBoSpecialPower = pIBoSpecialPower;
        }

        [HttpGet]
        [Route("{specialPowerId}")]
        public SpecialPower Get(int specialPowerId)
        {
            return IBoSpecialPower.Get(specialPowerId);
        }

        [HttpGet]
        public List<SpecialPower> Get()
        {
            return IBoSpecialPower.GetAll();
        }

        [HttpPut]
        public OutUpdateSpecialPowerVM Put(InUpdateSpecialPowerVM model)
        {
            OutUpdateSpecialPowerVM outUpdateSpecialPowerVM = new OutUpdateSpecialPowerVM();

            if (ModelState.IsValid)
            {

                try
                {
                    var specialPower = IBoSpecialPower.Get(model.ID);

                    specialPower.Cost = model.Cost is null ? specialPower.Cost : (double)model.Cost;
                    specialPower.Name = String.IsNullOrEmpty(model.Name) ? specialPower.Name : model.Name;
                    specialPower.Quantifier = model.Quantifier is null ? specialPower.Quantifier : Convert.ToInt32(model.Quantifier);
                    specialPower.Type = model.Type is null ? specialPower.Type : (SpecialPowerTypes)model.Type;
                    specialPower.Compensation = model.Compensation is null ? specialPower.Compensation : (double)model.Compensation;

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
                outCreateSpecialPowerVM.Message = "Objeto de entrada não está valido!";
                outCreateSpecialPowerVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outCreateSpecialPowerVM;
        }
    }
}
