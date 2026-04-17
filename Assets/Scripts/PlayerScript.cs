using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    private Rigidbody rb;

    private Vector2 _moveDirection;

    public InputActionReference moveRL;
    public InputActionReference jump;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, speed);
    }

    void Update()
    {
         _moveDirection = moveRL.action.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        jump.action.started += JUMP;
    }

    private void JUMP(InputAction.CallbackContext obj)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void OnDisable()
    {
        jump.action.started -= JUMP;
    }

}
