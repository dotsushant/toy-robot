using System;
using System.Collections.Generic;
using System.Linq;

namespace Robot
{
    public class Robot : IRobot
    {
        private IMotor _motor;
        private readonly List<IRobotSkill> _skills = new List<IRobotSkill>();

        public Orientation Orientation
        {
            get; private set;
        }

        public void SetMotor(IMotor motor)
        {
            _motor = motor;
        }

        public void AddSkill<T>() where T : IRobotSkill
        {
            if (!IsAlreadySkilledAt<T>())
            {
                _skills.Add(Activator.CreateInstance<T>());
            }
        }

        public void ApplySkill<T>() where T : IRobotSkill
        {
            FindSkill<T>()?.Apply(HandleOrientationUpdate, new CoreParameter(_motor, Orientation));
        }

        public void ApplySkill<T>(IParameter parameter) where T : IRobotSkill
        {
            if (parameter != null)
            {
                FindSkill<T>()?.Apply(HandleOrientationUpdate, new CoreParameter(_motor, Orientation), parameter);
            }
        }

        private void HandleOrientationUpdate(Orientation orientation)
        {
            Orientation = orientation;
        }

        private bool IsAlreadySkilledAt<T>() where T : IRobotSkill
        {
            return null != FindSkill<T>();
        }

        private IRobotSkill FindSkill<T>() where T : IRobotSkill
        {
            return _skills.FirstOrDefault(s => s.GetType() == typeof(T));
        }
    }
}