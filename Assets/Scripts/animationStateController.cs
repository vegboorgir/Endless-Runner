using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    int isJumpingHash;

    [SerializeField] float jumpForce = 5f;
    private bool isGrounded = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    void Update()
    {
        bool jumpPressed = UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame;

        if (isGrounded && jumpPressed)
        {
            isGrounded = false;
            animator.SetBool(isJumpingHash, true);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        animator.SetBool(isJumpingHash, false);
    }
}
