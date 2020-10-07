using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models.SpecialPower.In;
using Battleship.Models.SpecialPower.Out;
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

        /// <summary>
        /// Method to register special power
        /// </summary>
        /// <param name="pModel">In Model</param>
        /// <returns>Created or not</returns>
        /// <remarks>Register special power</remarks>
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
