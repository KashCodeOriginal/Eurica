namespace Services.GameProgress
{
    public class GameProgressService : IGameProgressService
    {
        private LiftStage _currentLiftStage = LiftStage.First;
        private HubStage _currentHubStage = HubStage.First;
        
        public LiftStage CurrentLiftStage => _currentLiftStage;
        public HubStage CurrentHubStage => _currentHubStage;

        public void SetUpLiftStage(LiftStage liftStage)
        {
            _currentLiftStage = liftStage;
        }

        public void SetUpHubStage(HubStage hubStage)
        {
            _currentHubStage = hubStage;
        }
    }
}