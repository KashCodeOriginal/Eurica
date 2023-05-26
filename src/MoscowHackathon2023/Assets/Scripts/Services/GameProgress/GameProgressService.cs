using Unit.UniversalGun;

namespace Services.GameProgress
{
    public class GameProgressService : IGameProgressService
    {
        private LiftStage _currentLiftStage = LiftStage.First;
        private HubStage _currentHubStage = HubStage.First;
        private GunTypes _currentWeaponOnPlayer = GunTypes.None;
        
        public LiftStage CurrentLiftStage => _currentLiftStage;
        public HubStage CurrentHubStage => _currentHubStage;

        public GunTypes CurrentWeaponOnPlayer => _currentWeaponOnPlayer;

        public void SetUpLiftStage(LiftStage liftStage)
        {
            _currentLiftStage = liftStage;
        }

        public void SetUpHubStage(HubStage hubStage)
        {
            _currentHubStage = hubStage;
        }

        public void SetUpCurrentWeapon(GunTypes gunType)
        {
            _currentWeaponOnPlayer = gunType;
        }
    }
}