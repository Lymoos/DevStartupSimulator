using UnityEngine;

namespace DevStartupSim.Core.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class PickupableSticky : MonoBehaviour
    {
        private Rigidbody rb;
        private Collider col;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
        }

        public void SetHeld(bool held)
        {
            if (rb != null)
            {
                rb.isKinematic = held;
                rb.useGravity = !held;
            }

            if (col != null)
                col.enabled = !held;
        }
    }
}
