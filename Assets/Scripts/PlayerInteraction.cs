using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    LayerMask layerMask;
    Transform cam;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Player");
        cam = transform.Find("Camera");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // =========================================
    // INPUT

    public void OnAttack(InputValue v)
    {
        float distance = Mathf.Infinity;
        // float distance = 1.5f;
        int damage = 5;
        int bullets = 10;
        float spread = 0.07f;
        for (int i = 0; i < bullets; i++)
        {
            Vector3 direction = (
                cam.transform.forward +
                cam.transform.right * UnityEngine.Random.Range(-spread, spread) +
                cam.transform.up * UnityEngine.Random.Range(-spread, spread)
            ).normalized;

            Color rayColor = Color.white;

            RaycastHit[] hits = Physics.RaycastAll(cam.position, direction, distance);
            foreach (RaycastHit hit in hits)
            {
                Debug.Log($"Hit {hit.collider.gameObject.name} at distance {hit.distance}");
                if (hit.collider.TryGetComponent<Health>(out var health))
                {
                    health.TakeDamage(damage);
                    rayColor = Color.yellow;
                }
            }

            Debug.DrawRay(cam.position, direction * Math.Min(distance, 1000), rayColor, 7.5f);
        }

    }
}
