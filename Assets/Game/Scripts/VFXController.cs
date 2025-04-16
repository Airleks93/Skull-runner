using System.Collections;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public GameObject vfxObject;
    public float activeTime = 5f;
    public float pauseTime = 5f;

    private void Start()
    {
        StartCoroutine(VFXCycle());
    }

    private IEnumerator VFXCycle()
    {
        while (true)
        {
            vfxObject.SetActive(true);
            yield return new WaitForSeconds(activeTime);

            vfxObject.SetActive(false);
            yield return new WaitForSeconds(pauseTime);
        }
    }
}


