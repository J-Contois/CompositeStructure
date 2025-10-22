using UnityEngine;

public class GameObjectToggleComponent : BaseToggleComponent
{
    protected override void ActivateComponent()
    {
        gameObject.SetActive(true);
    }

    protected override void DeactivateComponent()
    {
        gameObject.SetActive(false);
    }
}
