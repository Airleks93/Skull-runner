using UnityEngine;

public class AnmDoorGate : MonoBehaviour
{
    [SerializeField]
    private Animator gateAnimator; // Referenz zum Animator



    // Trigger-Ereignis f�r Spielerinteraktion
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("whatIsPlayer_Tag")) // �berpr�ft, ob der Spieler das Tor erreicht hat
        {
            gateAnimator.SetTrigger("isOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("whatIsPlayer_Tag")) // �berpr�ft, ob der Spieler den Trigger-Bereich verl�sst
        {
            gateAnimator.SetTrigger("isClose");
        }
    }


}
