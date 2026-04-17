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
        rb.linearVelocity = new Vector3(speed, rb.linearVelocity.y, 0);
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
    private void OnDesable()
    {
        jump.action.started -= JUMP;
    }

}
