using UnityEngine;
using DevStartupSim.Core.Company;

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
        [SerializeField] private CompanyUpgrades upgrades;

        public int Money => money;
        public int Stress => stress;
        public int Productivity => productivity;
        private void Awake()
        {
            if (upgrades == null)
                upgrades = FindObjectOfType<CompanyUpgrades>();
        }
        public void AddMoney(int amount)
        {
            float mult = upgrades != null ? upgrades.MoneyMultiplier : 1f;
            int final = Mathf.RoundToInt(amount * mult);
            money += Mathf.Max(0, final);
            Debug.Log($"Money +{final} => {money}");
        }


        public void AddStress(int amount)
        {
            float mult = upgrades != null ? upgrades.StressMultiplier : 1f;
            int final = Mathf.RoundToInt(amount * mult);

            stress += final;
            stress = Mathf.Clamp(stress, 0, stressMax);
            Debug.Log($"Stress {stress}/{stressMax}");
        }

        public void ImproveProductivity(int amount)
        {
            productivity += amount;
            productivity = Mathf.Clamp(productivity, 1, 10);
            Debug.Log($"Productivity => {productivity}");
        }

        public bool SpendMoney(int amount)
        {
            if (money < amount) return false;
            money -= amount;
            return true;
        }
    }
}
