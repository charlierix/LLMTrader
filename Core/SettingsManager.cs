using System.IO;
using System.Text.Json;

namespace Core
{
    public sealed class SettingsManager
    {
        #region Declaration Section

        private readonly object _lock = new object();

        private static readonly Lazy<SettingsManager> _instance = new Lazy<SettingsManager>(() => new SettingsManager());
        private const string SETTINGS_FILEPATH = "settings.json";

        private SettingsModel _settings;

        #endregion

        private SettingsManager()
        {
            Load();
        }

        public static SettingsModel Settings => _instance.Value._settings;

        public static void Save(SettingsModel settings)
        {
            lock (_instance.Value._lock)
            {
                _instance.Value._settings = settings;
                _instance.Value.Save();
            }
        }

        #region Private Methods

        // Example method to update a setting
        private static void UpdateLlmUrl(string newUrl)
        {
            lock (_instance.Value._lock)
            {
                var obj = _instance.Value._settings;

                if (obj?.llm != null)
                {
                    obj = obj with
                    {
                        llm = obj.llm with
                        {
                            url = newUrl,
                        }
                    };

                    _instance.Value.Save();
                }
            }
        }

        private void Load()
        {
            if (File.Exists(SETTINGS_FILEPATH))
            {
                string json = File.ReadAllText(SETTINGS_FILEPATH);
                _settings = JsonSerializer.Deserialize<SettingsModel>(json);
            }
            else
            {
                // Initialize with default settings if the file doesn't exist
                _settings = new SettingsModel
                {
                    llm = new SettingsModel_LLM
                    {
                        url = "http://192.168.0.1:11434",
                        model = "jean-luc/tiger-gemma-9b-v3:fp16",
                        max_threads = 2,
                    }
                };
            }
        }
        private void Save()
        {
            string json = JsonSerializer.Serialize(_instance.Value._settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SETTINGS_FILEPATH, json);
        }

        #endregion
    }
}
