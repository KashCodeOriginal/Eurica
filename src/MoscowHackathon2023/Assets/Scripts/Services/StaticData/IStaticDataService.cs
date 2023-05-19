using Data.StaticData.LevelData;

namespace Services.StaticData
{
    public interface IStaticDataService
    {
        public void LoadStaticData();

        public LevelData ForLevel(string key);
    }
}