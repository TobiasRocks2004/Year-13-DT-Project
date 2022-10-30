using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGlass : MonoBehaviour
{
    public GameObject particlePrefab;
    public Material puddleMaterial;
    public LayerMask layerMask;

    public float breakSpeed;

    Rigidbody rb;

    float prevSpeed = 0;
    float currentSpeed = 0;

    private ChemicalColor color;

    Vector3 hitPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        prevSpeed = currentSpeed;
        currentSpeed = rb.velocity.magnitude;

        if (prevSpeed > breakSpeed && currentSpeed < 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                GameObject particles = Instantiate(particlePrefab, hit.point, Quaternion.identity);

                Color matColor = new Color((float)color.r / 3, (float)color.g / 3, (float)color.b / 3, 1);

                Material puddleMat = transform.GetChild(0).GetComponent<Renderer>().material;

                particles.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = puddleMat;

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
