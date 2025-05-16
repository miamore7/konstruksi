using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LaptopInventory.Services
{
    public class LanguageManager
    {
        private Dictionary<string, string> _currentLang = new();

        public void LoadLanguage(string langCode, string path = "Models/Languages.json")
        {
            try
            {
                var json = File.ReadAllText(path);
                var allLangs = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);

                if (allLangs == null || !allLangs.ContainsKey(langCode))
                    throw new InvalidOperationException($"Language code '{langCode}' not found!");

                _currentLang = allLangs[langCode];
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load language: {ex.Message}");
            }
        }

        public string Get(string key)
        {
            if (_currentLang.TryGetValue(key, out var value))
                return value;

            return $"[MISSING:{key}]";
        }

        public string Format(string key, params object[] args)
        {
            var template = Get(key);
            try
            {
                return args.Length > 0 ? string.Format(template, args) : template;
            }
            catch
            {
                return template;
            }
        }
    }
}