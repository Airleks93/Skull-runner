using UnityEngine;

public class VFXColliderControll : MonoBehaviour
{  
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.UpdateHealthLeft();
        Debug.Log("Trigger detected with: " + other.gameObject.name);
    }
}
