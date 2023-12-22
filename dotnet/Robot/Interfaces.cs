using System;

namespace Robot
{
    /// <summary>
    /// Describes the skills exhibited by a robot
    /// </summary>
    public interface IRobotSkill
    {
        /// <summary>
        /// When asked the robot can apply a given skill
        /// </summary>
        /// <param name="orientationPredicate">The result of apply action is the new orientation of the robot</param>
        /// <param name="parameters">Additional parameters specific to the skill</param>
        void Apply(Action<Orientation> orientationPredicate, params IParameter[] parameters);
    }

    /// <summary>
    /// Toy robot
    /// </summary>
    public interface IRobot
    {
        /// <summary>
        /// Indicates the orientation of the robot within its terrain
        /// </summary>
        Orientation Orientation { get; }

        /// <summary>
        /// Attaches/Detaches motor to the robot
        /// </summary>
        /// <remarks>Pass null to remove the motor</remarks>
        /// <param name="motor">Motor to be attached</param>
        void SetMotor(IMotor motor);

        /// <summary>
        /// Equips the robot with a new skill
        /// </summary>
        /// <typeparam name="T">Skill type to be added</typeparam>
        void AddSkill<T>() where T : IRobotSkill;

        /// <summary>
        /// Asks the robot to apply a given skill as specified by the skill type
        /// </summary>
        /// <typeparam name="T">Skill type to be applied</typeparam>
        void ApplySkill<T>() where T : IRobotSkill;

        /// <summary>
        /// Asks the robot to apply a given skill with additional parameters as specified by the skill type
        /// </summary>
        /// <typeparam name="T">Skill type to be applied</typeparam>
        /// <param name="parameter"></param>
        void ApplySkill<T>(IParameter parameter) where T : IRobotSkill;
    }

    /// <summary>
    /// Describes the terrain of the robot
    /// </summary>
    public interface ITerrain
    {
        /// <summary>
        /// Indicates if the location is within the bounds of the terrain
        /// </summary>
        /// <param name="location">Location to be validated</param>
        /// <returns>True if the location is within the bounds of the terrain</returns>
        bool Contains(Location location);
    }

    /// <summary>
    /// Decribes the motor used in the robot
    /// The motor provides the robot an ability to step and turn
    /// </summary>
    public interface IMotor
    {
        /// <summary>
        /// Indicates the direction of the motor
        /// </summary>
        Direction Direction { get; }

        /// <summary>
        /// Instructs the motor to turn left
        /// </summary>
        void TurnLeft();

        /// <summary>
        /// Instructs the motor to turn right
        /// </summary>
        void TurnRight();

        /// <summary>
        /// Instructs the motor to turn to specific direction
        /// </summary>
        void Turn(Direction direction);
    }

    /// <summary>
    /// Parameter to be used when the robot skill needs to be applied
    /// </summary>
    public interface IParameter { }
}