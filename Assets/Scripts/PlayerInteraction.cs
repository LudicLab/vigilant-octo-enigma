using System;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.InputSystem;

#nullable enable

public class PlayerInteraction : MonoBehaviour
{
    Transform cam;
    Rigidbody? grabbedObject = null;
    float? grabbedDistance = null;
    Dictionary<string, float> penetration = new Dictionary<string, float> {
            {"Wall",  0.0f},
            {"Explosive", 0.0f}
        };

    [SerializeField] float grabMaxDistance = 4.0f;
    [SerializeField] float grabMoveSpeed = 20f;
    [SerializeField] float grabMaxSpeed = 15f;

    void Awake()
    {
        cam = transform.Find("Camera");
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (grabbedObject is Rigidbody grObject && grabbedDistance is float grDistance)
        {
            Vector3 targetPosition = cam.position + cam.forward * grDistance;
            Vector3 toTarget = targetPosition - grabbedObject.position;

            grabbedObject.linearVelocity = Vector3.ClampMagnitude(toTarget * grabMoveSpeed, grabMaxSpeed);
        }
    }

    public void OnAttack(InputValue v)
    {
        float distance = Mathf.Infinity;
        // float distance = 1.5f;
        float damage = 100.0f;
        int bullets = 1;
        float spread = 0.0f;
        for (int i = 0; i < bullets; i++)
        {
            Vector3 direction = (
                cam.transform.forward +
                cam.transform.right * UnityEngine.Random.Range(-spread, spread) +
                cam.transform.up * UnityEngine.Random.Range(-spread, spread)
            ).normalized;

            Color rayColor = Color.white;

            RaycastHit[] hits = Physics.RaycastAll(cam.position, direction, distance);

            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            foreach (RaycastHit hit in hits)
            {
                Debug.Log($"Hit {hit.collider.gameObject.name} at distance {hit.distance}");
                if (hit.collider.TryGetComponent<Health>(out var health))
                {
                    health.TakeDamage(damage);
                    rayColor = Color.yellow;
                }
                Debug.Log(hit.collider.gameObject.tag);
                float multiplier;
                if(penetration.TryGetValue(hit.collider.gameObject.tag, out multiplier))
                {
                    damage *= multiplier;
                }
                
                if(damage <= 0.1)
                {
                    break;
                }

            }

            Debug.DrawRay(cam.position, direction * Math.Min(distance, 1000), rayColor, 7.5f);
        }

    }

    public void OnInteract(InputValue v)
    {
        if (grabbedObject is Rigidbody)
        {
            grabbedObject = null;
            grabbedDistance = null;
            Debug.Log("dropped held item");
        }
        else
        {
            Vector3 direction = cam.transform.forward;
            RaycastHit hit;
            Color color = Color.green;

            if (Physics.Raycast(cam.position, direction, out hit, grabMaxDistance))
            {
                Debug.Log($"hit rigidbody {hit.rigidbody?.gameObject}");
                grabbedObject = hit.rigidbody;
                grabbedDistance = hit.distance;
                color = Color.blue;
            }

            Debug.DrawRay(cam.position, direction * grabMaxDistance, color, 7.5f);
        }
    }
}
