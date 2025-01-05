using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public record SettingsModel
    {
        public SettingsModel_LLM llm { get; init; }
    }

    public record SettingsModel_LLM
    {
        public string url { get; init; }
        public string model { get; init; }
        public int max_threads { get; init; }
    }
}
