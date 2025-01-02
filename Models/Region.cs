using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public record Region
    {
        public string Name { get; init; }

        /// <summary>
        /// A sentence or two describing this area
        /// </summary>
        public string Description { get; init; }

        public DangerLevel DangerLevel { get; init; }

        /// <summary>
        /// Historical events or notes related to this area
        /// </summary>
        public string[] History { get; init; }

        public Location Location { get; init; }
        public float Size { get; init; }

        /// <summary>
        /// Some words that help categorize this region
        /// </summary>
        public string[] Tags { get; init; }
    }

    public enum DangerLevel
    {
        Safe,
        Moderate,
        Dangerous,
    }

}
