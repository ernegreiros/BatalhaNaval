using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleshipApi.Models.Player.In;
using BattleshipApi.Models.Player.Out;
using BattleshipApi.Player.DML;
using BattleshipApi.Player.DML.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IBoPlayer IBoPlayer;

        public PlayerController(IBoPlayer pIBoPlayer)
        {
            IBoPlayer = pIBoPlayer;
        }

        [HttpGet("{playerId}")]
        public Player Get(int playerId)
        {
            return IBoPlayer.GetPlayerInfo(playerId);
        }

        [HttpPost]
        public OutCreatePlayerVM Post(InCreatePlayerVM playerObject)
        {
            OutCreatePlayerVM outCreatePlayerVM = new OutCreatePlayerVM();

            if (ModelState.IsValid)
            {

                try
                {
                    IBoPlayer.InsertPlayer(new Player() 
                    {
                        Code  = playerObject.Code,
                        Name = playerObject.Name,
                        Login = playerObject.Login,
                        Password = playerObject.Password,
                        Money = playerObject.Money

                    });

                    outCreatePlayerVM.HttpStatus = StatusCodes.Status201Created;
                    outCreatePlayerVM.Message = $"Player {playerObject.Name} successfully registered!";
                }
                catch (Exception ex)
                {
                    outCreatePlayerVM.HttpStatus = StatusCodes.Status400BadRequest;
                    outCreatePlayerVM.Message = $"Error when registering player! {ex.Message}";
                }
            }
            else
            {
                outCreatePlayerVM.Message = "Objeto de entrada não está valido!";
                outCreatePlayerVM.HttpStatus = StatusCodes.Status400BadRequest;
            }

            return outCreatePlayerVM;
        }

        // PUT api/<PlayerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
