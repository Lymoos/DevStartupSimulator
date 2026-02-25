using UnityEngine;
using DevStartupSim.Core.Company;
using DevStartupSim.Core.Player;

namespace DevStartupSim.UI
{
    public class UpgradeButtons : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        [SerializeField] private CompanyUpgrades upgrades;

        [Header("Prices")]
        [SerializeField] private int toolsPrice = 50;
        [SerializeField] private int hirePrice = 80;

        private void Awake()
        {
            if (stats == null) stats = FindObjectOfType<PlayerStats>();
            if (upgrades == null) upgrades = FindObjectOfType<CompanyUpgrades>();
        }

        public void BuyTools()
        {
            if (stats == null || upgrades == null) return;
            if (stats.Money < toolsPrice) return;

            // списать деньги
            Spend(toolsPrice);
            upgrades.UpgradeTools();
        }

        public void BuyHire()
        {
            if (stats == null || upgrades == null) return;
            if (stats.Money < hirePrice) return;

            Spend(hirePrice);
            upgrades.HirePeople();
        }

        private void Spend(int amount)
        {
            // у тебя нет метода Spend — сделаем "AddMoney(-amount)" безопасно:
            // добавь в PlayerStats метод Spend, либо сделай так:
            if (!stats.SpendMoney(toolsPrice)) return;
        }
    }
}