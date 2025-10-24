using UnityEngine;

public class TrailToggleComponent : BaseToggleComponent
{
    [SerializeField] private TrailRenderer m_TrailRenderer;
    
    protected override void ActivateComponent()
    {
        if (m_TrailRenderer != null)
        {
            m_TrailRenderer.emitting = true;
        }
    }

    protected override void DeactivateComponent()
    {
        if (m_TrailRenderer != null)
        {
            m_TrailRenderer.emitting = false;
        }
    }
}
