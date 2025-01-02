using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public record MarketSession
    {
        public string Name { get; init; }

        /// <summary>
        /// A few sentences describing the world, type of market, etc
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// A list of notable events that have happened in this world or market
        /// </summary>
        public string[] HistoricalEvents { get; init; }

        /// <summary>
        /// Half a dozen or so words that help categorize this world
        /// </summary>
        public string[] Tags { get; init; }

        public ImportantIndividual[] Individuals { get; init; }

        public Region[] Regions { get; init; }

        public Landmark[] Landmarks { get; init; }

        public EconomySite[] EconomySites { get; init; }
    }
}
