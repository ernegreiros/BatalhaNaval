using System.Collections.Generic;

namespace Battleship
{
    public class WebSocketConnections
    {
        public Dictionary<string, string> Connections { get; set; }
        public List<PlayerBinded> PlayersBinded { get; set; }
        public WebSocketConnections()
        {
            Connections = new Dictionary<string, string>();
            PlayersBinded = new List<PlayerBinded>();
        }
    }

    public class PlayerBinded
    {
        public string Code1 { get; }
        public string Code2 { get; }

        public PlayerBinded(string code1, string code2)
        {
            Code1 = code1;
            Code2 = code2;
        }
    }
}
