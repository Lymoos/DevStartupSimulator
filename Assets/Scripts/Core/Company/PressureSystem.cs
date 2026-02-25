using UnityEngine;
using DevStartupSim.Core.Player;

namespace DevStartupSim.Core.Company
{
    public class PressureSystem : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private PlayerStats stats;
        [SerializeField] private Transform boardRoot; // SpawnArea или SlotsRoot

        [Header("Pressure rules")]
        [SerializeField] private float checkEverySeconds = 5f;
        [SerializeField] private int stressPerPressure = 5;
        [SerializeField] private int boardLimit = 3;

        [Header("Message")]
        [SerializeField] private string pressureText = "Менеджер: ДАВЛЕНИЕ! Закрывай задачи быстрее!";

        private float timer;

        public string LastMessage { get; private set; } = "";

        private void Awake()
        {
            if (stats == null) stats = FindObjectOfType<PlayerStats>();
        }

        private void Update()
        {
            if (stats == null || boardRoot == null) return;

            timer += Time.deltaTime;
            if (timer >= checkEverySeconds)
            {
                timer = 0f;
                ApplyPressureIfNeeded();
            }
        }

        private void ApplyPressureIfNeeded()
        {
            int tasksOnBoard = CountTasksOnBoard();

            if (tasksOnBoard >= boardLimit)
            {
                stats.AddStress(stressPerPressure);
                LastMessage = pressureText + $" (на доске: {tasksOnBoard})";
                Debug.Log(LastMessage);
            }
            else
            {
                LastMessage = "";
            }
        }

        private int CountTasksOnBoard()
        {
            // Если ты используешь слоты: boardRoot = SlotsRoot, а стикер внутри слота
            int count = 0;

            for (int i = 0; i < boardRoot.childCount; i++)
            {
                Transform slot = boardRoot.GetChild(i);
                if (slot == null) continue;

                // если в слоте есть ребёнок - это стикер
                if (slot.childCount > 0) count++;
            }

            return count;
        }
    }
}