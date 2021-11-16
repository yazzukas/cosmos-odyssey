using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosOdyssey.Shared
{
    public class Information
    {
        public static IEnumerable<string> PlanetNames = new List<string> { "Earth", "Mars", "Venus", "Jupiter", "Saturn", "Uranus", "Neptune", "Mercury" };

        public static Dictionary<string, List<string>> PossibleRoutes = new()
        {
            { "Earth", new List<string> { "Jupiter", "Uranus" } },
            { "Venus", new List<string> { "Earth", "Mercury" } },
            { "Mars", new List<string> { "Venus" } },
            { "Mercury", new List<string> { "Venus" } },
            { "Jupiter", new List<string> { "Mars", "Venus" } },
            { "Saturn", new List<string> { "Earth", "Neptune" } },
            { "Uranus", new List<string> { "Saturn", "Neptune" } },
            { "Neptune", new List<string> { "Uranus", "Mercury" } }
        };
    }
}
