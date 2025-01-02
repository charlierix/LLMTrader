using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public record Landmark
    {
        public string Name { get; init; }

        /// <summary>
        /// A sentence or two describing this landmark
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Notable specifics about this landmark
        /// </summary>
        public string[] InterstingFacts { get; init; }

        public Location Location { get; init; }

        /// <summary>
        /// This could be interpreted differently based on the shape (e.g., radius for spherical, length for linear)
        /// </summary>
        public float Size { get; init; }

        /// <summary>
        /// Some words that help categorize this landmark
        /// </summary>
        public string[] Tags { get; init; }
    }
}
