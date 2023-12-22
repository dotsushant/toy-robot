namespace Robot
{
    public class Program
    {
        public static void Main()
        {
            new RobotSimulation().AddRobot().BuildTerrain(5, 5).Simulate();
        }
    }
}