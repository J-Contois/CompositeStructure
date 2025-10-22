using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    [SerializeField] private new ParticleSystem particleSystem = null;

    private void OnTriggerEnter(Collider other)
    {
        particleSystem.Play();
    }

}
