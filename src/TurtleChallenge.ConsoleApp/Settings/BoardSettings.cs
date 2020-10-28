using System.Collections.Generic;

namespace TurtleChallenge.ConsoleApp.Settings
{
    public sealed class BoardSettings
    {
        public SizeSettings Size { get; set; }
        public PointSettings ExitPoint { get; set; }
        public List<PointSettings> Mines { get; set; }
    }
}