using System.Collections.Generic;
using System.Linq;
using Data.StaticData.LevelData;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string LEVEL_STATIC_DATA_PATH = "LevelData";

        private Dictionary<string, LevelData> _levels;

        public void LoadStaticData()
        {
            _levels = Resources
                .LoadAll<LevelData>(LEVEL_STATIC_DATA_PATH)
                .ToDictionary(x => x.LevelName, x => x);
        }

        public LevelData ForLevel(string key)
        {
            return _levels.TryGetValue(key, out LevelData staticData) ? staticData : null;
        }
    }
}