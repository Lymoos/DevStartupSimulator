using UnityEngine;
using TMPro;
using DevStartupSim.Core.Player;
using DevStartupSim.Core.Company;

namespace DevStartupSim.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        [SerializeField] private PressureSystem pressure;

        [Header("TMP")]
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text stressText;
        [SerializeField] private TMP_Text messageText;

        private void Awake()
        {
            if (stats == null) stats = FindObjectOfType<PlayerStats>();
            if (pressure == null) pressure = FindObjectOfType<PressureSystem>();
        }

        private void Update()
        {
            if (stats == null) return;

            if (moneyText != null)
                moneyText.text = $"Money: {stats.Money}";

            if (stressText != null)
                stressText.text = $"Stress: {stats.Stress}/100";

            if (messageText != null)
                messageText.text = pressure != null ? pressure.LastMessage : "";
        }
    }
}