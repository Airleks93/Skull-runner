using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    [Header("Settings")]
    [SerializeField] private bool autoClose = true;

    private bool _isDoorOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("whatIsPlayer_Tag") && !_isDoorOpen)
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("whatIsPlayer_Tag") && autoClose && _isDoorOpen)
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        _isDoorOpen = true;
        _animator.SetBool("isDoorOpen", true);
    }

    private void CloseDoor()
    {
        _isDoorOpen = false;
        _animator.SetBool("isDoorOpen", false);
    }

    public void ToggleDoor()
    {
        if (_isDoorOpen)
            CloseDoor();
        else
            OpenDoor();
    }
}
