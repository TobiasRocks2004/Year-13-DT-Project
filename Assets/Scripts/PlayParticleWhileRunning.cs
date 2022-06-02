using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleWhileRunning : MonoBehaviour
{
    public CrafterObject crafter;
    public ParticleSystem particles;

    private void Update()
    {
        if (crafter.running && particles.isStopped)
        {
            particles.Play();
        }
        else if (!crafter.running && particles.isPlaying)
        {
            particles.Stop();
        }
    }
}
