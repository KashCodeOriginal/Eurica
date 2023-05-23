namespace Services.GameProgress
{
    public class GameProgressService : IGameProgressService
    {
        private LiftStage _currentLiftStage = LiftStage.First;
        
        public LiftStage CurrentLiftStage => _currentLiftStage;

        public void SetUpLiftStage(LiftStage liftStage)
        {
            _currentLiftStage = liftStage;
        }
    }
}