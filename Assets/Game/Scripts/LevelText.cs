using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI levelText;

    void Start()
    {
        levelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
    }
}
