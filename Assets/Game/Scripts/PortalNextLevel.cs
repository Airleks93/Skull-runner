using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PortalNextLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject portalVFX;      

    private bool portalActivated = false;


    private void Start()
    {
        DeactivatePortal();
        GameManager.instance.OnAllSkullsCollected += ActivatePortal;
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null) {
            GameManager.instance.OnAllSkullsCollected -= ActivatePortal;
        }
    }

    public void ActivatePortal()
    {
        if (portalVFX != null && !portalActivated)
        {
            portalVFX.SetActive(true);
            portalActivated = true;
            Debug.Log("Portal activated");
        }
    }

    public void DeactivatePortal()
    {
        if (portalVFX != null)
        {
            portalVFX.SetActive(false);
            portalActivated = false;
        }
    }
}

