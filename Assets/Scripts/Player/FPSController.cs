using UnityEngine;

namespace DevStartupSim.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class FPSController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch = 80f;

        private Rigidbody rb;
        private Vector3 input;
        private float yaw;
        private float pitch;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            LockCursor(true);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                LockCursor(true);
        }

        private void LockCursor(bool locked)
        {
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !locked;
        }
        private void Update()
        {
            // WASD
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            input = new Vector3(x, 0f, z).normalized;

            // Mouse look
            float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
            float my = Input.GetAxis("Mouse Y") * mouseSensitivity;

            yaw += mx;
            pitch -= my;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            transform.rotation = Quaternion.Euler(0f, yaw, 0f);
            cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }

        private void FixedUpdate()
        {
            Vector3 vel = rb.velocity;

            // жёсткий deadzone: если почти 0 — считаем 0
            if (input.sqrMagnitude < 0.01f)
            {
                rb.velocity = new Vector3(0f, vel.y, 0f);
                rb.angularVelocity = Vector3.zero;
                return;
            }

            Vector3 desired = transform.TransformDirection(input) * moveSpeed;
            rb.velocity = new Vector3(desired.x, vel.y, desired.z);
            rb.angularVelocity = Vector3.zero;
        }
    }
}
