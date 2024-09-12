using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Project.Core.Settings
{
    public sealed class AppSettings
    {
        private const string _jsonFileName = "settings.json";

        private readonly static string _jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", _jsonFileName);

        public static AppSettings Settings { get; private set; }

        public IDictionary<string, ThirdPartyConnectionModel> ThirdPartyConnectionModels { get; set; }

        private AppSettings(){}

        static AppSettings()
        {
            ReloadSettings();
        }

        private static void ReloadSettings()
        {
            var serializer = new JsonSerializer();

            using var sr = new StreamReader(_jsonFilePath);
            using var reader = new JsonTextReader(sr);
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.StartObject)
                {
                    Settings = serializer.Deserialize<AppSettings>(reader);
                }
            }
        }
    }
}
