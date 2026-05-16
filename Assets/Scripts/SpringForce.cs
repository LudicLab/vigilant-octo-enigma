using UnityEngine;
using UnityEngine.InputSystem;

public class SpringForce : MonoBehaviour
{
    private SpringJoint spring;
    private Rigidbody rb;
    public Rigidbody targetRb;
    public float pressForce = 800f;
    public float forceStrength = 1200f;
    public float pressThreshold = 0.15f;
    private Vector3 startPos;
    private bool wasPressed;
    private Camera cam;
    private Mouse mouse;

    void Awake()
    {
        mouse = Mouse.current;
    }

    void Start()
    {
        spring = GetComponent<SpringJoint>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = mouse.position.ReadValue();
            Ray ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                PressButton(hit.point);
            }
        }

        float depth = startPos.y - transform.localPosition.y;
        if (depth > pressThreshold && !wasPressed)
        {
            OnPressed();
            wasPressed = true;
        }
        else if (depth < pressThreshold / 2)
        {
            wasPressed = false;
        }
    }

    void PressButton(Vector3 hitPoint)
    {
        rb.AddForce(Vector3.down * pressForce, ForceMode.Impulse);
        if (targetRb) targetRb.AddForceAtPosition(Vector3.down * forceStrength, hitPoint, ForceMode.Impulse);
    }

    void OnPressed()
    {
        StartCoroutine(ButtonTimeout(0.2f));
    }

    private System.Collections.IEnumerator ButtonTimeout(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (GetComponent<MainMenu>())
            if(GetComponent<MainMenu>().enabled)
                GetComponent<MainMenu>().MainMenuPlayButton();
        else
            Debug.Log("Przycisk wciśnięty");
    }
}
