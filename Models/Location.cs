using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public record Location
    {
        // Names and directions to other items
        public LocationRelation[] RelativeTo { get; init; }

        // ------- exact positions are optional and only used when the map has been finalized -------

        // Used if shape is Point
        public Vector3? ExactPoint { get; init; }

        // Used if shap is linear
        public Vector3? ExactFrom { get; init; }
        public Vector3? ExactTo { get; init; }

        /// <summary>
        /// The shape of item (e.g., spherical, linear).
        /// </summary>
        public LocationShape? Shape => ExactPoint != null ? LocationShape.Point : ExactFrom != null && ExactTo != null ? LocationShape.Linear : null;
    }

    public record LocationRelation
    {
        /// <summary>
        /// Could be cardinal directions (north, east...) or other types of directions like above, inside, next to...
        /// </summary>
        public string Direction { get; init; }

        /// <summary>
        /// How far away the item is
        /// </summary>
        public RelativeDistance Distance { get; init; }

        /// <summary>
        /// Name of the object or objects that this is relative to.  Use more than one name if direction is something
        /// like "between"
        /// </summary>
        public string[] Names { get; init; }
    }

    public enum LocationShape
    {
        Point,
        Linear,
    }

    public enum RelativeDirection
    {
        Inside,
        Surrounding,

        NextTo,
        Between,

        Left,
        Right,
        InFront,
        Behind,
        Above,
        Below,

        North,
        South,
        East,
        West,

        //NorthEast,        // instead of more exact enums, make two instances of location.  This would also accomodate above/below
        //NorthWest,
        //SouthWest,
        //SouthEast,
    }

    public enum RelativeDistance
    {
        Same,       // roughly the same location
        Near,       // close but not directly adjacent
        Medium,     // somewhat far but still within a reasonable distance
        Far,        // relatively distant
        VeryFar,    // extremely distant
    }
}
