using System;
using WorldOfRaces.Interfaces;

namespace WorldOfRaces.UI
{
    public class ConsoleInputReader : IInputReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
