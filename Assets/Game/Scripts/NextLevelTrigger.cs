using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{

    public static NextLevelTrigger instance;

    [SerializeField]
    private SceneChanger _sceneChanger;

    public static Collider _colliderPortal;

    private void Start()
    {
        _colliderPortal = GetComponent<Collider>();
        _colliderPortal.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("whatIsPlayer_Tag"))
        {
            Debug.Log("Collision with Player detected");
            _sceneChanger.LoadNextLevel();
        }
        else
        {
            Debug.Log($"Trigger entered by: {other.name}");
        }
    }
}
