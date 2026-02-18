namespace DevStartupSim.Tasks
{
    public class HotfixTask : TaskBase
    {
        public HotfixTask(string title, SkillType skill, int difficulty)
            : base(title, TaskType.Hotfix, skill, difficulty) { }

        public override int GetMoneyReward()
        {
            return 30 * Difficulty;
        }
    }
}
