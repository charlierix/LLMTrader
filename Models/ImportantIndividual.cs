using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// An entry for someone of significance
    /// </summary>
    public record ImportantIndividual
    {
        public string Name { get; init; }

        /// <summary>
        /// A sentence or two describing the individual
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Role or profession
        /// </summary>
        public string Role { get; init; }

        /// <summary>
        /// Level of influence or reputation
        /// </summary>
        public Level Influence { get; init; }

        /// <summary>
        /// Whether they are still alive/working or more generically when they are/were/willbe active
        /// </summary>
        public WhenActive WhenActive { get; init; }

        /// <summary>
        /// Notable accomplishments or contributions
        /// </summary>
        public string[] Accomplishments { get; init; }

        /// <summary>
        /// Some words that help categorize this individual
        /// </summary>
        public string[] Tags { get; init; }
    }

    public enum WhenActive
    {
        FarFuture,
        NearFuture,
        CurrentlyActive,
        RecentlyStopped,
        LongAgo,
    }
}
