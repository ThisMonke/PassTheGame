#if UNITY_EDITOR
#define UNITY_EDITOR_BUILD
#endif

using UnityEngine;

public class PlayerController : MonoBehaviour
{
#if UNITY_EDITOR_BUILD
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float flySpeed = 10f;
    public float mouseSensitivity = 2f; // Adjust the sensitivity here
    public Camera mainCamera;
#endif

    private Rigidbody rb;
    private bool isFlying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
#if UNITY_EDITOR_BUILD
        // Mouse Controls Camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        mainCamera.transform.Rotate(Vector3.left * mouseY);

        // WASD Movement relative to camera
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;
        Vector3 moveVelocity = moveDirection * moveSpeed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Flying Control
        if (isFlying)
        {
            float flyDirection = Input.GetAxis("Fly");
            Vector3 flyVelocity = mainCamera.transform.up * flyDirection * flySpeed;
            rb.velocity = new Vector3(rb.velocity.x, flyVelocity.y, rb.velocity.z);
        }
#endif
    }
}