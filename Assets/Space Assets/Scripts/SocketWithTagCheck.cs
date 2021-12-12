using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketWithTagCheck : XRSocketInteractor
{
    public string targetTag = string.Empty;

    public XRBaseInteractable SelectTarget;

    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && MatchUsingTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) &&
        MatchUsingTag(interactable) &&
        (selectTarget == null || selectTarget == interactable);
    }

    private bool MatchUsingTag(XRBaseInteractable interactable)
    {
        return interactable.CompareTag(targetTag);
    }

    // Make selectTarget a child of the tile
    public void makeChild()
    {
        selectTarget.transform.SetParent(this.transform.parent);
    }

    // Remove selectTarget as a child of the tile
    public void removeChild()
    {
        if (selectTarget != null)
        {
            if (
                !selectTarget.name.Contains("Test") &&
                selectTarget.name.StartsWith("Head")
            )
            {
                selectTarget
                    .transform
                    .GetComponent<CodeBarCreator>()
                    .removeEnclosure();
                selectTarget.transform.parent = null;
            }
        }
    }

    public void Update()
    {
    }
}
