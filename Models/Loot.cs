using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    // TODO: figure out how to define quantity.  A single int may get washed out and ignored

    public record Loot
    {
        public string Name { get; init; }

        /// <summary>
        /// A sentence or two describing the item
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// A list of notable events that have occurred with this item.
        /// Each event should add to the story or lore of the item.
        /// The list can be empty if the item is brand new and has nothing significant about its origin.
        /// </summary>
        public string[] UsageHistory { get; init; }

        /// <summary>
        /// Size of the item relative to other items of its type
        /// 1 would be normal size, .5 is half of normal size, 2 is double the average size
        /// </summary>
        public double RelativeScalePercent { get; init; }

        /// <summary>
        /// The quality of the item when it was new
        /// </summary>
        public ItemQuality InitialQuality { get; init; }
        /// <summary>
        /// Percentage condition of the item (0 is fully broken, 100 is new)
        /// </summary>
        public int ConditionPercent { get; init; }

        public string MainColor { get; init; }
        public string AccentColor { get; init; }

        /// <summary>
        /// Some words that help categorize this item
        /// </summary>
        public string[] Tags { get; init; }
    }
}
