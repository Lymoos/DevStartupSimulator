using UnityEngine;
using DevStartupSim.Tasks;
using DevStartupSim.UI;

namespace DevStartupSim.Core
{
    public class TaskSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private StickyNoteView stickyPrefab;

        [Header("Слоты на доске (максимум 3)")]
        [Tooltip("Перетащи сюда Slot1, Slot2, Slot3 (объекты Transform, которые стоят на поверхности доски).")]
        [SerializeField] private Transform[] slots;

        [Header("Материалы цветов")]
        [SerializeField] private Material yellow;
        [SerializeField] private Material red;
        [SerializeField] private Material orange;

        [Header("Настройки генерации")]
        [SerializeField] private float spawnEverySeconds = 3f;

        private float timer;

        // В каждом слоте хранится ссылка на созданный стикер, чтобы:
        // - не создавать больше 3
        // - не налезать друг на друга
        private StickyNoteView[] spawned;

        private void Awake()
        {
            // На случай если slots не назначены
            if (slots == null)
                slots = new Transform[0];

            spawned = new StickyNoteView[slots.Length];
        }

        private void Update()
        {
            if (stickyPrefab == null)
            {
                Debug.LogError("TaskSpawner: stickyPrefab НЕ назначен в инспекторе!");
                return;
            }

            if (slots == null || slots.Length == 0)
            {
                Debug.LogError("TaskSpawner: slots пустой! Назначь Slot1/2/3 в инспекторе.");
                return;
            }

            timer += Time.deltaTime;
            if (timer >= spawnEverySeconds)
            {
                timer = 0f;
                SpawnOneSticky();
            }
        }


        private void SpawnOneSticky()
        {
            // 1) Если уже занято 3 слота (или сколько у тебя slots.Length) — не спавним
            int freeIndex = FindFreeSlotIndex();
            if (freeIndex == -1)
                return;

            // 2) создаём "логическую" задачу (ООП)
            TaskBase task = CreateRandomTask();

            // 3) берём нужный слот
            Transform slot = slots[freeIndex];
            if (slot == null)
                return;

            // 4) создаём стикер прямо в слоте (позиция/поворот слота)
            StickyNoteView view = Instantiate(stickyPrefab, slot.position, slot.rotation);

            // 5) делаем стикер дочерним слота — теперь он гарантированно "на доске"
            view.transform.SetParent(slot, worldPositionStays: true);

            // 6) на всякий — зафиксируем ровно в центре слота
            view.transform.localPosition = Vector3.zero;
            view.transform.localRotation = Quaternion.identity;

            // 7) "нарисовать" задачу на стикере
            view.SetTask(task, yellow, red, orange);

            // 8) помечаем слот занятым
            spawned[freeIndex] = view;

            Debug.Log($"Spawned in slot {freeIndex}: {task.Type}, reward={task.GetMoneyReward()}");
        }

        private int FindFreeSlotIndex()
        {
            for (int i = 0; i < spawned.Length; i++)
            {
                // Если объект был удалён вручную — ссылка станет null
                if (spawned[i] == null)
                    return i;
            }
            return -1;
        }

        private TaskBase CreateRandomTask()
        {
            int r = Random.Range(0, 100);
            SkillType skill = (SkillType)Random.Range(0, 3);
            int difficulty = Random.Range(1, 6);

            if (r < 55) return new FeatureTask("Add feature", skill, difficulty);
            if (r < 85) return new BugTask("Fix bug", skill, difficulty);
            return new HotfixTask("HOTFIX!", skill, difficulty);
        }
    }
}
