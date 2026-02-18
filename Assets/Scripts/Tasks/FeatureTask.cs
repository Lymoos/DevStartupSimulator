namespace DevStartupSim.Tasks
{
    public class FeatureTask : TaskBase
    {
        public FeatureTask(string title, SkillType skill, int difficulty) : base(title, TaskType.Feature, skill, difficulty) { }

        public override int GetMoneyReward()
        {
            return 20 * Difficulty;
        }
    }
}
