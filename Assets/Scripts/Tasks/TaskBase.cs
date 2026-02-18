using UnityEngine;

namespace DevStartupSim.Tasks
{
    public abstract class TaskBase
    {
        public string Title { get; }
        public TaskType Type { get; }
        public SkillType RequiredSkill { get; }
        public int Difficulty { get; }

        protected TaskBase(string title, TaskType type, SkillType skill, int difficulty)
        {
            Title = title;
            Type = type;
            RequiredSkill = skill;
            Difficulty = Mathf.Clamp(difficulty, 1, 5);
        }

        public abstract int GetMoneyReward();
    }
}
