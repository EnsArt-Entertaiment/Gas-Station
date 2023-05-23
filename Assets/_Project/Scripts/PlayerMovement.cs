using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public Animator animator;
    public Transform characters;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveX = joystick.Horizontal;
        float moveY = 0f; 
        float moveZ = joystick.Vertical;

        Vector3 movement = new Vector3(moveX, moveY, moveZ);
        rb.velocity = movement * GetSpeed();

        // Update rotation based on joystick input
        UpdateRotation(movement);

        // Update animator based on movement
        UpdateAnimator(movement);
    }

    private void UpdateRotation(Vector3 movement)
    {
        if (movement.magnitude > 0)
        {
            // Calculate the angle between the forward vector and the movement direction
            float targetAngle = Mathf.Atan2(-movement.x, -movement.z) * Mathf.Rad2Deg;

            // Smoothly rotate the "Characters" game object towards the target angle
            float rotationSpeed = 10f; // Adjust as needed
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            characters.rotation = Quaternion.Lerp(characters.rotation, targetRotation, rotationSpeed * Time.deltaTime);

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

        // Determine the animation state based on the magnitude of movement
        bool isWalking = moveMagnitude > 0 && moveMagnitude <= 0.5f;
        bool isRunning = moveMagnitude > 0.5f;
        bool isIdle = moveMagnitude == 0f;

        // Set the animator triggers based on the animation states
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isIdle", isIdle);
    }

    private float GetSpeed()
    {
        if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
        {
            if (joystick.Direction.magnitude > 0.5f)
            {
                return runSpeed;
            }
            else
            {
                return walkSpeed;
            }
        }
        else
        {
            return 0f;
        }
    }
}
