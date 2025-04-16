using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("whatIsPlayer_Tag"))
        {
            Debug.Log("Player entered the DeadZone. Respawning...");

            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
                other.transform.position = respawnPoint.position;
                other.transform.rotation = respawnPoint.rotation;
                characterController.enabled = true;
                GameManager.instance.UpdateHealthLeft();
            }
        }
    }
}
