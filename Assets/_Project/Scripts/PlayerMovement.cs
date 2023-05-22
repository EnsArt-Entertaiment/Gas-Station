using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 5f;
    public Animator animator;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

        private void FixedUpdate()
    {
        float moveX = joystick.Horizontal;
        float moveY = 0f; // Assuming you only want movement on the XZ plane, set moveY to 0
        float moveZ = joystick.Vertical;

        Vector3 movement = new Vector3(moveX, moveY, moveZ);
        rb.velocity = movement * speed;

        // Update rotation based on joystick input
        UpdateRotation(movement);
    }

    private void UpdateRotation(Vector3 movement)
    {
        if (movement.magnitude > 0)
        {
            // Calculate the angle between the forward vector and the movement direction
            float targetAngle = Mathf.Atan2(-movement.x, -movement.z) * Mathf.Rad2Deg;

            // Smoothly rotate the player object towards the target angle
            float rotationSpeed = 10f; // Adjust as needed
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Reset camera rotation to its initial rotation
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
        }
    }

    private void UpdateAnimator(Vector3 movement)
    {
        if (animator == null)
            return;

        // Calculate the magnitude of the movement vector
        float moveMagnitude = movement.magnitude;

        // Set animator parameters based on the magnitude of movement
        animator.SetFloat("Speed", moveMagnitude);

        // Determine the normalized movement direction
        Vector3 moveDirection = movement.normalized;
        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveZ", moveDirection.z);
    }
}
