using UnityEngine;
using UnityEngine.AI;

namespace DevStartupSim.Core.AI
{
    public class ManagerNPC : MonoBehaviour
    {
        [Header("Nav")]
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float waitSecondsAtPoint = 2f;

        [Header("Board logic")]
        [SerializeField] private Transform boardRoot; // куда спавнятся стикеры (SpawnArea)
        [SerializeField] private int maxAllowedStickiesOnBoard = 3;

        [Header("Punish")]
        [SerializeField] private int stressPenalty = 5;

        private int index = 0;
        private float waitTimer = 0f;

        private void Awake()
        {
            if (agent == null) agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            GoToNextPoint();
        }

        private void Update()
        {
            if (patrolPoints == null || patrolPoints.Length == 0) return;

            // дошёл до точки
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitSecondsAtPoint)
                {
                    waitTimer = 0f;

                    // если сейчас у доски — проверяем задачи
                    TryProcessBoard();

                    GoToNextPoint();
                }
            }
        }
        private void RemoveOneStickyFromBoard()
        {
            if (boardRoot == null) return;

            // boardRoot содержит Slot1/Slot2/Slot3
            for (int i = 0; i < boardRoot.childCount; i++)
            {
                Transform slot = boardRoot.GetChild(i);
                if (slot == null) continue;

                // Внутри слота должен быть стикер как дочерний объект
                if (slot.childCount > 0)
                {
                    Transform sticky = slot.GetChild(0);
                    Destroy(sticky.gameObject);
                    Debug.Log($"Manager: removed sticky from slot {slot.name}");
                    return;
                }
            }

            Debug.Log("Manager: no stickies found on board");
        }
        private void GoToNextPoint()
        {
            index = (index + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[index].position);
        }

        private void TryProcessBoard()
        {
            if (boardRoot == null) return;

            int stickyCount = boardRoot.childCount;

            // Если задач больше лимита — “менеджер давит”
            if (stickyCount > maxAllowedStickiesOnBoard)
            {
                Debug.Log("Manager: too many tasks! Pressure...");
                var playerStats = FindObjectOfType<DevStartupSim.Core.Player.PlayerStats>();
                if (playerStats != null) playerStats.AddStress(stressPenalty);
            }

            // “Забираем” одну задачу (просто удаляем один стикер)
            if (stickyCount > 0)
            {
                RemoveOneStickyFromBoard();
                Debug.Log("Manager: assigned one task (removed from board).");
            }
        }
    }
}
