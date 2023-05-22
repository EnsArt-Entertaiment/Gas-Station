using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 5f;

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
        rb.velocity = movement * speed;
    }
}
