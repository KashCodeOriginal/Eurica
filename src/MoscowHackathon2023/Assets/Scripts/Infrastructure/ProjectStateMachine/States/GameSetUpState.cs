using Infrastructure.ProjectStateMachine.Base;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameSetUpState : IState<Bootstrap>, IEnterable
    {
        public Bootstrap Initializer { get; }

        public GameSetUpState(Bootstrap initializer)
        {
            Initializer = initializer;
        }

        public void OnEnter()
        {
            Initializer.StateMachine.SwitchState<GameplayState>();
        }
    }
}