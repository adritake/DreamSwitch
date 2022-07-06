using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public bool IsTriggered => _isTriggered;

    private bool _isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        _isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isTriggered = false;
    }
}
