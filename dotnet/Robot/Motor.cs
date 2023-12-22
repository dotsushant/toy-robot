using Stateless;

namespace Robot
{
    public class Motor : IMotor
    {
        private readonly StateMachine<Direction, Intent> _stateMachine;

        public Motor(Direction direction = Direction.NORTH)
        {
            _stateMachine = new StateMachine<Direction, Intent>(direction);
            _stateMachine.Configure(Direction.NORTH).Permit(Intent.TURN_LEFT, Direction.WEST);
            _stateMachine.Configure(Direction.WEST).Permit(Intent.TURN_LEFT, Direction.SOUTH);
            _stateMachine.Configure(Direction.SOUTH).Permit(Intent.TURN_LEFT, Direction.EAST);
            _stateMachine.Configure(Direction.EAST).Permit(Intent.TURN_LEFT, Direction.NORTH);
            _stateMachine.Configure(Direction.NORTH).Permit(Intent.TURN_RIGHT, Direction.EAST);
            _stateMachine.Configure(Direction.EAST).Permit(Intent.TURN_RIGHT, Direction.SOUTH);
            _stateMachine.Configure(Direction.SOUTH).Permit(Intent.TURN_RIGHT, Direction.WEST);
            _stateMachine.Configure(Direction.WEST).Permit(Intent.TURN_RIGHT, Direction.NORTH);
        }

        public Direction Direction => _stateMachine.State;

        public void TurnLeft()
        {
            _stateMachine.Fire(Intent.TURN_LEFT);
        }

        public void TurnRight()
        {
            _stateMachine.Fire(Intent.TURN_RIGHT);
        }

        public void Turn(Direction direction)
        {
            while (Direction != direction) TurnLeft();
        }

        public override string ToString()
        {
            return $"{Direction}";
        }
    }
}