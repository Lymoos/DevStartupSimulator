using DevStartupSim.Core.Interactions;
using DevStartupSim.Core.Player;
using UnityEngine;
using DevStartupSim.UI;

namespace DevStartupSim.Player
{
    public class PlayerPickupFPS : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private Camera fpsCamera;
        [SerializeField] private Transform holdPoint;
        [SerializeField] private Transform workDeskPoint;

        [Header("Settings")]
        [SerializeField] private float pickupDistance = 2.5f;


        private PickupableSticky held;

        
        [SerializeField] private PlayerStats stats;
    private void Awake()
        {
            if (fpsCamera == null)
                fpsCamera = Camera.main;
            if (stats == null) stats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (held == null) TryPickup();
                else DropOnDesk();
            }
        }

        private void TryPickup()
        {
            Ray ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
            {
                var sticky = hit.collider.GetComponentInParent<PickupableSticky>();
                if (sticky == null) return;

                held = sticky;
                held.SetHeld(true);

                held.transform.SetParent(holdPoint);
                held.transform.localPosition = Vector3.zero;
                held.transform.localRotation = Quaternion.identity;
            }
        }

        private void DropOnDesk()
        {
            if (workDeskPoint == null)
            {
                held.transform.SetParent(null);
                held.transform.position = fpsCamera.transform.position + fpsCamera.transform.forward * 1.0f;
                held.SetHeld(false);
                held = null;
                return;
            }


            held.transform.SetParent(null);
            held.transform.position = workDeskPoint.position;
            held.transform.rotation = workDeskPoint.rotation;

            held.SetHeld(false);
            var view = held.GetComponent<StickyNoteView>();
            if (view != null && stats != null && view.CurrentTask != null)
            {
                stats.AddMoney(view.CurrentTask.GetMoneyReward());
                stats.AddStress(-2); 
            }

            Destroy(held.gameObject, 0.1f);
            held = null;
        }
    }
}
