using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float laneDistance = 3f;
    [SerializeField] float laneSwitchSpeed = 10f;
    [SerializeField] float switchCooldown = 0.2f;

    private Rigidbody rb;
    private Vector2 _moveDirection;
    private int currentLane = 1;
    private float targetXPosition;
    private float coolDownTimer = 0f;


    public InputActionReference moveRL;
    public InputActionReference jump;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        targetXPosition = transform.position.x;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, speed);
    }

    void Update()
    {
        if(coolDownTimer > 0)
            coolDownTimer -= Time.deltaTime;

        _moveDirection = moveRL.action.ReadValue<Vector2>();

        if(coolDownTimer <= 0 && Mathf.Abs(_moveDirection.x) > 0.5f)
        {
            int laneChange = _moveDirection.x > 0 ? 1 : -1;
            int newLane = currentLane + laneChange;

            if(newLane >= 0 && newLane <= 2)
            {
                currentLane = newLane;
                targetXPosition = (currentLane - 1) * laneDistance;
                coolDownTimer = switchCooldown;
            }
        }

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, targetXPosition, laneSwitchSpeed * Time.deltaTime);
        transform.position = newPosition;
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
