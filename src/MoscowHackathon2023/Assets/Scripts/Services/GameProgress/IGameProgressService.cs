namespace Services.GameProgress
{
    public interface IGameProgressService
    {
        public LiftStage CurrentLiftStage { get; }
        public void SetUpLiftStage(LiftStage liftStage);
    }
}