using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public record EconomySite
    {
        public string Name { get; init; }

        /// <summary>
        /// A brief description of this economic entity, detailing its primary function (ex: creation, purchasing, refurbishment)
        /// </summary>
        public string Description { get; init; }

        public Location Location { get; init; }

        /// <summary>
        /// Notable facts or historical events associated with this site
        /// </summary>
        public string[] History { get; init; }

        /// <summary>
        /// The wealth and resources managed by this economic entity
        /// </summary>
        public Level Affluence { get; init; }

        /// <summary>
        /// The level of activity or operation of this economic entity
        /// </summary>
        public Level Productivity { get; init; }

        /// <summary>
        /// Specific activities performed by this economic entity
        /// </summary>
        public EconomyActivity[] Activities { get; init; }

        /// <summary>
        /// A set of keywords that categorize this site
        /// </summary>
        public string[] Tags { get; init; }
    }

    public record EconomyActivity
    {
        public string Name { get; init; }

        /// <summary>
        /// A sentence or two describing this specific activity
        /// </summary>
        public string Description { get; init; }

        public bool ImportsMaterial { get; init; }
        public bool ExportsMaterial { get; init; }

        public Level Quality { get; init; }
        public Level Quantity { get; init; }

        /// <summary>
        /// Some keywords that help categorize this activity
        /// </summary>
        public string[] Tags { get; init; }
    }
}
