using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGlass : MonoBehaviour
{
    public GameObject particlePrefab;
    public Material puddleMaterial;
    public LayerMask layerMask;

    Rigidbody rb;

    float prevSpeed = 0;
    float currentSpeed = 0;

    Vector3 hitPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        prevSpeed = currentSpeed;
        currentSpeed = rb.velocity.magnitude;

        if (prevSpeed > 5 && currentSpeed < 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                hitPos = hit.point;
                GameObject puddles = Instantiate(particlePrefab, hit.point, Quaternion.identity);
                puddles.GetComponentsInChildren<ParticleSystemRenderer>()[1].material = puddleMaterial;

                Destroy(gameObject);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hitPos, 0.1f);
    }
}
