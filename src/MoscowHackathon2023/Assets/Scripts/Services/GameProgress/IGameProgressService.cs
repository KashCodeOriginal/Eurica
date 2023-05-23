namespace Services.GameProgress
{
    public interface IGameProgressService
    {
        public LiftStage CurrentLiftStage { get; }
        public HubStage CurrentHubStage { get; }
        public void SetUpLiftStage(LiftStage liftStage);
        public void SetUpHubStage(HubStage hubStage);
    }
}