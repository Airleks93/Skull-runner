using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;
    private int currentLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadLevel1()
    {

        LoadLevel(1);
    }
    private void LoadLevel2()
    {
        LoadLevel(2);
    }
    private void LoadLevel3()
    {
        LoadLevel(3);
    }
    public void LoadLevel(int level)
    {
        try
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene($"Level{level}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error loading Level{level}: " + ex.Message);
            ShowCursor();
            throw;
        }
        SetCursor();
    }

    public void LoadMainMenu()
    {
        try
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error loading Main Menu: " + ex.Message);
            throw;
        }
        SetCursor();
    }

    public void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.Log("No more levels!");
            SceneManager.LoadScene(0);
        }
    }

    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void SetCursor()
    {
        if (Time.timeScale == 0)
        {
            ShowCursor();
        }
        else
        {
            HideCursor();
        }
    }

    public static void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
