using Unity.VisualScripting;
using UnityEngine;

public class ItemSkullBehavior : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("whatIsPlayer_Tag"))
        {

            if (_gameManager != null) {
                _gameManager.UpdateSkullsCollected();
            }
            Destroy(gameObject);
        }
    }


}
