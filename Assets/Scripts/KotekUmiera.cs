using UnityEngine;
[RequireComponent(typeof(Animator))]
public class KotekUmiera : MonoBehaviour
{
    [Header("Y‑position monitoring")]
    [Tooltip("How much Y may change before the Animator is turned off")]
    public float yThreshold = 0.5f;   // przykładowo 0.5 jednostki

    [Header("Debug")]
    public bool showDebugLog = true;

    private Animator _anim;
    private float _startY;
    private bool _animatorDisabled = false;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
            Debug.LogError("[YPositionLockAnimator] No Animator found on this GameObject.");

        // zapamiętaj początkową pozycję Y
        _startY = transform.position.y;
    }

    void Update()
    {
        // jeśli już wyłączono, nie sprawdzaj dalej
        if (_animatorDisabled) return;

        float currentY = transform.position.y;
        float deltaY = Mathf.Abs(currentY - _startY);

        if (deltaY > yThreshold)
        {
            _animatorDisabled = true;
            _anim.enabled = false;                     // <‑‑ wyłącza Animator komponent [web:28]
            if (showDebugLog)
                Debug.Log($"[YPositionLockAnimator] Y‑change {deltaY:F2} > threshold {yThreshold}. Animator disabled permanently.");
        }
    }
}
