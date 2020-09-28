using System.Collections.Generic;

namespace Battleship
{
    public class WebSocketConnections
    {
        public Dictionary<string, string> Connections { get; set; }
        public List<Bind> Binds { get; set; }
        public WebSocketConnections()
        {
            Connections = new Dictionary<string, string>();
            Binds = new List<Bind>();
        }
    }

    public class Bind
    {
        string Code1;
        string Code2;

        public Bind(string code1, string code2)
        {
            Code1 = code1;
            Code2 = code2;
        }
    }
}
