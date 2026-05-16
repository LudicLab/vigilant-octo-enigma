using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CSMovement : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 7f;
    public float slowWalkMultiplier = 0.55f;
    public float crouchMultiplier = 0.42f;

    public float acceleration = 80f;
    public float friction = 6f;
    public float airAcceleration = 6f;
    public float jumpForce = 5f;
    public float gravity = 20f;

    [Header("Crouch")]
    public float crouchHeightMultiplier = 0.6f;
    public float crouchLerp = 10f;

    [Header("Camera")]
    public Transform cam;
    public float sensitivity = 200f;

    [Header("Debug UI")]
    public TextMeshProUGUI positionUI;
    public TextMeshProUGUI speedUI;
    public TextMeshProUGUI rotationUI;
    public TextMeshProUGUI groundedUI;
    public TextMeshProUGUI stateUI;

    // Private
    private CharacterController controller;
    private Vector3 velocity;
    private Vector2 moveInput;

    private bool jumpPressed;
    private bool crouchHeld;
    private bool slowWalkHeld;

    private float originalHeight;
    private Vector3 originalCamLocalPos;
    private float camPitch;



    // =========================================
    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        originalCamLocalPos = cam.localPosition;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // =========================================
    void Update()
    {
        HandleCrouch();
        Move();
        UpdateUI();
    }

    // =========================================
    // INPUT
    public void OnMove(InputValue v) => moveInput = v.Get<Vector2>();
    public void OnJump(InputValue v)
    {
        if (v.isPressed && controller.isGrounded)
            jumpPressed = true;
    }

    public void OnCrouch(InputValue v) => crouchHeld = v.isPressed;
    public void OnWalk(InputValue v) => slowWalkHeld = v.isPressed;   // Slow walk button (Shift)

    public void OnLook(InputValue v)
    {
        Vector2 look = v.Get<Vector2>();

        camPitch -= look.y * sensitivity * Time.deltaTime;
        camPitch = Mathf.Clamp(camPitch, -89f, 89f);

        cam.localRotation = Quaternion.Euler(camPitch, 0, 0);
        transform.Rotate(Vector3.up * look.x * sensitivity * Time.deltaTime);
    }

    // =========================================
    // CROUCH SYSTEM
    void HandleCrouch()
    {
        float targetHeight = crouchHeld ? originalHeight * crouchHeightMultiplier : originalHeight;

        controller.height = Mathf.Lerp(controller.height, targetHeight, crouchLerp * Time.deltaTime);

        float heightDiff = originalHeight - controller.height;
        cam.localPosition = Vector3.Lerp(
            cam.localPosition,
            originalCamLocalPos - new Vector3(0, heightDiff * 0.5f, 0),
            crouchLerp * Time.deltaTime
        );
    }

    // =========================================
    // MOVEMENT (SOURCE-LIKE)
    void Move()
    {
        if (controller.isGrounded)
        {
            ApplyFriction();

            float speed = maxSpeed;

            if (slowWalkHeld)
                speed *= slowWalkMultiplier;

            if (crouchHeld)
                speed *= crouchMultiplier;

            Vector3 wishdir = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;

            Accelerate(wishdir, speed, acceleration);

            if (jumpPressed)
            {
                velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            }

            jumpPressed = false;
        }
        else
        {
            // Air movement
            float airWishSpeed = maxSpeed * 0.9f;

            Vector3 wishdir = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;

            AirAccelerate(wishdir, airWishSpeed, airAcceleration);
        }

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;
        else
            velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    // =========================================
    // SOURCE MOVEMENT HELPERS
    void ApplyFriction()
    {
        Vector3 flat = new Vector3(velocity.x, 0, velocity.z);
        float speed = flat.magnitude;

        if (speed < 0.1f)
            return;

        float drop = speed * friction * Time.deltaTime;
        float newSpeed = Mathf.Max(speed - drop, 0);

        newSpeed /= speed;
        flat *= newSpeed;

        velocity.x = flat.x;
        velocity.z = flat.z;
    }

    void Accelerate(Vector3 wishdir, float wishspeed, float accel)
    {
        float currentspeed = Vector3.Dot(velocity, wishdir);
        float addspeed = wishspeed - currentspeed;
        if (addspeed <= 0) return;

        float accelspeed = accel * Time.deltaTime * wishspeed;
        accelspeed = Mathf.Min(accelspeed, addspeed);

        velocity += wishdir * accelspeed;
    }

    void AirAccelerate(Vector3 wishdir, float wishspeed, float accel)
    {
        float currentspeed = Vector3.Dot(velocity, wishdir);
        float addspeed = wishspeed - currentspeed;

        if (addspeed <= 0)
            return;

        float accelspeed = accel * Time.deltaTime * wishspeed;
        accelspeed = Mathf.Min(accelspeed, addspeed);

        velocity += wishdir * accelspeed;
    }

    // =========================================
    // DEBUG UI
    void UpdateUI()
    {
        if (positionUI)
            positionUI.text = $"Position: {transform.position.x:F1} | {transform.position.y:F2} | {transform.position.z:F2}";

        if (speedUI)
        {
            Vector3 hv = new Vector3(velocity.x, 0, velocity.z);
            speedUI.text = $"Speed: {hv.magnitude:F2}";
        }

        if (rotationUI)
            rotationUI.text = $"Rotation: {transform.rotation.eulerAngles.x:F1} | {transform.rotation.eulerAngles.y:F1}";

        if (groundedUI)
            groundedUI.text = $"Grounded: {controller.isGrounded}";

        if (stateUI)
        {
            if (!controller.isGrounded)
                stateUI.text = "Air";
            else if (crouchHeld)
                stateUI.text = "Crouching";
            else if (slowWalkHeld && moveInput.magnitude > 0.1f)
                stateUI.text = "Slow Walking";
            else if (moveInput.magnitude > 0.1f)
                stateUI.text = "Moving";
            else
                stateUI.text = "Idle";
        }
    }
}
