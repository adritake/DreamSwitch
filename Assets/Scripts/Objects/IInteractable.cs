using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void OnLookedBegin();
    public void OnLookedEnd();
    public void OnInteractBegin();
    public void OnInteractEnd();
}
