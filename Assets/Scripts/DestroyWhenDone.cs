using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenDone : MonoBehaviour
{
    public List<ParticleSystem> particleSystems;

    void Update()
    {
        bool done = true;

        foreach (ParticleSystem particleSystem in particleSystems)
        {
            if (particleSystem.isPlaying)
            {
                done = false;
                break;
            }
        }

        if (done)
        {
            Destroy(gameObject);
        }
    }
}
