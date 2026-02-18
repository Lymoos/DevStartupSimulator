namespace DevStartupSim.Tasks
{
    public class BugTask : TaskBase
    {
        public BugTask(string title, SkillType skill, int difficulty)
            : base(title, TaskType.Bug, skill, difficulty) { }

        public override int GetMoneyReward()
        {
            return 10 * Difficulty;
        }
    }
}
