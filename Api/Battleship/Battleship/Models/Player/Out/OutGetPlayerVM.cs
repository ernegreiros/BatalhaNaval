using BattleshipApi.Player.DML;

namespace BattleshipApi.Models.Player.Out
{
    public class OutGetPlayerVM : OutBase
    {
        public OutGetPlayerVM()
        {
            Player = new BattleshipApi.Player.DML.Player();
        }

        public BattleshipApi.Player.DML.Player Player { get; set; }
    }
}
