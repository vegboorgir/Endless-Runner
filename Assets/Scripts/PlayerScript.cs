using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float laneDistance = 3f;
    [SerializeField] float laneSwitchSpeed = 10f;
    [SerializeField] float switchCooldown = 0.2f;
    [SerializeField] float fallMultiplier = 3f;


    private Rigidbody rb;
    private Vector2 _moveDirection;
    private int currentLane = 1;
    private float targetXPosition;
    private float coolDownTimer = 0f;

    public InputActionReference moveRL;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        targetXPosition = transform.position.x;
    }

    void FixedUpdate()
    {
        float newX = Mathf.Lerp(rb.position.x, targetXPosition, laneSwitchSpeed * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(
            (newX - rb.position.x) / Time.fixedDeltaTime,
            rb.linearVelocity.y,   // gravity handles Y completely
            speed
        );
    }
    void Update()
    {
        if (coolDownTimer > 0)
            coolDownTimer -= Time.deltaTime;

        _moveDirection = moveRL.action.ReadValue<Vector2>();

        if (coolDownTimer <= 0 && Mathf.Abs(_moveDirection.x) > 0.5f)
        {
            int laneChange = _moveDirection.x > 0 ? 1 : -1;
            int newLane = currentLane + laneChange;
            if (newLane >= 0 && newLane <= 2)
            {
                currentLane = newLane;
                targetXPosition = (currentLane - 1) * laneDistance;
                coolDownTimer = switchCooldown;
            }
        }
    }


    private void OnEnable()
    {
        moveRL.action.Enable();      // ← add this
    }

    private void OnDisable()
    {
        moveRL.action.Disable();     // ← add this
    }
}
