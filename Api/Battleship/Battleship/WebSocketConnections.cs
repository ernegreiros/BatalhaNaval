using System.Collections.Generic;

namespace Battleship
{
    public class WebSocketConnections
    {
        public List<Connection> Connections { get; set; }
        public List<PlayerBinded> PlayersBinded { get; set; }
        public WebSocketConnections()
        {
            Connections = new List<Connection>();
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

    public class Connection
    {
        public string Code { get; set; }
        public string ConnectionId { get; set; }
        public bool Ready { get; set; }

        public Connection(string code, string connectionId, bool ready)
        {
            Code = code;
            ConnectionId = connectionId;
            Ready = ready;
        }
    }
}
