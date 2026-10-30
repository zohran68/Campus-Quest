using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;

    public float jumpHeight = 2f;
    public float gravity = -20f;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // =========================
        // MOVEMENT
        // =========================

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float speed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }

        controller.Move(move * speed * Time.deltaTime);


        // =========================
        // JUMP
        // =========================

        bool jumpPressed =
            Input.GetKeyDown(KeyCode.Tab) ||
            Input.GetKeyDown(KeyCode.Space);

        if (jumpPressed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        // =========================
        // GRAVITY
        // =========================

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}