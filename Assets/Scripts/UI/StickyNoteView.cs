using TMPro;
using UnityEngine;
using DevStartupSim.Tasks;

namespace DevStartupSim.UI
{
    public class StickyNoteView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer noteRenderer;
        [SerializeField] private TMP_Text titleText;

        private DevStartupSim.Tasks.TaskBase currentTask;
        public DevStartupSim.Tasks.TaskBase CurrentTask => currentTask;

        public void SetTask(TaskBase task, Material yellow, Material red, Material orange)
        {
            // 1) текст
            titleText.text = task.Title;

            currentTask = task;


            // 2) цвет по типу
            if (task.Type == TaskType.Feature) noteRenderer.material = yellow;
            else if (task.Type == TaskType.Bug) noteRenderer.material = red;
            else noteRenderer.material = orange;
        }
    }
}
