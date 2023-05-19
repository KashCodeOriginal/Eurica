using System.Collections.Generic;
using System.Linq;
using Data.StaticData.LevelData;
using Data.StaticData.VoicePhrases;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string LEVEL_STATIC_DATA_PATH = "LevelData";
        private const string VOICE_MESSAGES_DATA_PATH = "VoiceMessagesData";

        private Dictionary<string, LevelData> _levels;
        private Dictionary<string, VoiceMessage> _voiceMessages;

        public void LoadStaticData()
        {
            _levels = Resources
                .LoadAll<LevelData>(LEVEL_STATIC_DATA_PATH)
                .ToDictionary(x => x.LevelName, x => x);
            
            _voiceMessages = Resources
                .LoadAll<VoiceMessage>(VOICE_MESSAGES_DATA_PATH)
                .ToDictionary(x => x.ID, x => x);
        }

        public LevelData ForLevel(string key)
        {
            return _levels.TryGetValue(key, out LevelData staticData) ? staticData : null;
        }

        public VoiceMessage GetVoiceMessageByID(string id)
        {
            return _voiceMessages.TryGetValue(id, out VoiceMessage voiceMessage) ? voiceMessage : null;
        }
    }
}