using UnityEngine;

namespace DevStartupSim.Core.Company
{
    public class CompanyUpgrades : MonoBehaviour
    {
        [Header("Multipliers")]
        [SerializeField] private float moneyMultiplier = 1f;   // увеличивает награду за задачи
        [SerializeField] private float stressMultiplier = 1f;  // уменьшает/увеличивает стресс

        public float MoneyMultiplier => moneyMultiplier;
        public float StressMultiplier => stressMultiplier;

        public void UpgradeTools()
        {
            moneyMultiplier += 0.25f; // +25% к деньгам
            Debug.Log($"Upgraded Tools. MoneyMultiplier={moneyMultiplier:0.00}");
        }

        public void HirePeople()
        {
            stressMultiplier -= 0.15f; // -15% к стрессу
            stressMultiplier = Mathf.Clamp(stressMultiplier, 0.3f, 2f);
            Debug.Log($"Hired People. StressMultiplier={stressMultiplier:0.00}");
        }
    }
}