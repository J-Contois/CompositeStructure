using UnityEngine;

public class ParticleSystemToggleComponent : BaseToggleComponent
{
    [SerializeField] private ParticleSystem m_ParticleSystem;

    protected override void ActivateComponent()
    {
        m_ParticleSystem.Play();
    }

    protected override void DeactivateComponent()
    {
        m_ParticleSystem.Stop();
    }
}
