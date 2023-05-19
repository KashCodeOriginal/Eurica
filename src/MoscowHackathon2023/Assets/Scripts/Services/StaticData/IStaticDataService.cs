using Data.StaticData.LevelData;
using Data.StaticData.VoicePhrases;

namespace Services.StaticData
{
    public interface IStaticDataService
    {
        public void LoadStaticData();
        public LevelData ForLevel(string key);
        public VoiceMessage GetVoiceMessageByID(string id);
    }
}