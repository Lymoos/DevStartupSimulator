using UnityEngine;

namespace DevStartupSim.Core.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private int money = 0;
        [SerializeField] private int stress = 0;          // 0..100
        [SerializeField] private int productivity = 1;    // 1..10

        [Header("Tuning")]
        [SerializeField] private int stressMax = 100;

        public int Money => money;
        public int Stress => stress;
        public int Productivity => productivity;

        public void AddMoney(int amount)
        {
            money += Mathf.Max(0, amount);
            Debug.Log($"Money +{amount} => {money}");
        }

        public void AddStress(int amount)
        {
            stress += amount;
            stress = Mathf.Clamp(stress, 0, stressMax);
            Debug.Log($"Stress {stress}/{stressMax}");
        }

        public void ImproveProductivity(int amount)
        {
            productivity += amount;
            productivity = Mathf.Clamp(productivity, 1, 10);
            Debug.Log($"Productivity => {productivity}");
        }
    }
}
