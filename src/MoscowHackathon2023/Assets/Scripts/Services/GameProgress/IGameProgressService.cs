using Unit.UniversalGun;

namespace Services.GameProgress
{
    public interface IGameProgressService
    {
        public LiftStage CurrentLiftStage { get; }
        public HubStage CurrentHubStage { get; }
        public GunTypes CurrentWeaponOnPlayer { get; }
        public void SetUpLiftStage(LiftStage liftStage);
        public void SetUpHubStage(HubStage hubStage);
        public void SetUpCurrentWeapon(GunTypes gunType);
    }
}