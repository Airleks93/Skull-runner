using UnityEngine;

public class AnmDoorGate : MonoBehaviour
{
    [SerializeField]
    private Animator gateAnimator; // Referenz zum Animator



    // Trigger-Ereignis für Spielerinteraktion
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("whatIsPlayer_Tag")) // Überprüft, ob der Spieler das Tor erreicht hat
        {
            gateAnimator.SetTrigger("isOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("whatIsPlayer_Tag")) // Überprüft, ob der Spieler den Trigger-Bereich verlässt
        {
            gateAnimator.SetTrigger("isClose");
        }
    }


}
