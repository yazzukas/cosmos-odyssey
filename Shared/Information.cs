using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosOdyssey.Shared
{
    public class Information
    {
        public static List<string> PlanetNames = new() { "Earth", "Mars", "Venus", "Jupiter", "Saturn", "Uranus", "Neptune", "Mercury" };

        //public static readonly List<List<string>> PossibleRoutes = new List<string>;
        public static Dictionary<string, List<string>> PossibleRoutes = new()
        {
            { "Earth", new List<string> { "Jupiter", "Uranus" } },
            { "Venus", new List<string> { "Earth", "Mercury" } },
            { "Mercury", new List<string> { "Venus" } },
            { "Jupiter", new List<string> { "Mars", "Venus" } },
            { "Saturn", new List<string> { "Earth", "Neptune" } },
            { "Uranus", new List<string> { "Saturn", "Neptune" } },
            { "Neptune", new List<string> { "Uranus", "Mercury" } }
        };
    }
}
