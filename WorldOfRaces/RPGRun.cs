using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Ned.Engine;
using WorldOfRaces.Interfaces;
using WorldOfRaces.UI;

namespace WorldOfRaces
{
    public class RPGRun
    {
        public static void Main()
        {
            IRenderer renderer = new ConsoleRenderer();
            IInputReader reader = new ConsoleInputReader();

            GameEngine engine = new GameEngine(reader, renderer);

            engine.Run();
        }
    }
}
