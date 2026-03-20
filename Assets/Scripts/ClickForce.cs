using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Camera))]
public class ClickForce : MonoBehaviour
{
    [Header("Ustawienia siły")]
    public float pushForce = 15f;   // siła impulsowa
    public ForceMode forceMode = ForceMode.Impulse;

    [Header("Raycast")]
    public float maxDistance = 100f;
    public LayerMask pickLayers = ~0;   // wszystko domyślnie

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
            Debug.LogError("[ClickPush] Skrypt musi być na obiekcie z komponentem Camera!");
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // Jednorazowe kliknięcie LPM (nowy Input System)
        if (Mouse.current.leftButton.wasPressedThisFrame)
            TryPush();
    }

    void TryPush()
    {
        // Pobierz pozycję myszki i wygeneruj promień z kamery
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(mousePos);

        // Opционально: widać promień w Scene view (pomaga w debugowaniu)
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.cyan, 0.2f);

        if (!Physics.Raycast(ray, out RaycastHit hit, maxDistance, pickLayers))
        {
            //Debug.Log("[ClickPush] Raycast nic nie trafił.");
            return;
        }

        // Szukamy Rigidbody na trafionym obiekcie lub w jego rodzicu
        Rigidbody rb = hit.rigidbody != null ? hit.rigidbody
                         : hit.collider.GetComponentInParent<Rigidbody>();

        if (rb == null)
        {
            //Debug.Log($"[ClickPush] Trafiony obiekt {hit.collider.name} nie ma Rigidbody.");
            return;
        }

        if (rb.isKinematic)
        {
           // Debug.Log($"[ClickPush] Rigidbody na {rb.name} jest Kinematic – wyłącz Is Kinematic.");
            return;
        }

        // Kierunek siły: dokładnie przeciwny do forward kamery (od kamery na zewnątrz)
        Vector3 pushDirection = -cam.transform.forward;

        // Aplikuj impuls
        rb.AddForce(pushDirection * -pushForce, forceMode);

        Debug.Log($"[ClickPush] Popycham {rb.name} siłą {pushForce} w kierunku {pushDirection} (od kamery)");
    }
}
