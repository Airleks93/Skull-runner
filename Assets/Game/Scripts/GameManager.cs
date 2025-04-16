using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Settings")]
    public int _skullsNumberToCollect;
    public int _skullsCollected;
    public int _Health = 3;
    public int _HealthLeft;

    [SerializeField] private float timeLeft;
    public float timeLevel1 = 60f;
    public float timeLevel2 = 80f;
    public float timeLevel3 = 50f;

    [Header("UI References")]
    [SerializeField] private GameObject _GameOverCanvas;
    [SerializeField] private TMP_Text skull_UI;
    [SerializeField] private TMP_Text health_UI;
    [SerializeField] private TMP_Text time_UI;

    [Header("Audio")]
    public AudioSource backgroundAudio;

    [SerializeField] private bool _isCanvasActiveGameOverCanvas = false;

    public event System.Action OnAllSkullsCollected;

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
            return;
        }
    }

    private void Start()
    {
        ResetGameState();
        UpdateUI();
    }

    private void Update()
    {
        HandleGameTimer();
        HandleEscapeInput();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneChanger.HideCursor();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeScene(scene.buildIndex);
    }

    private void InitializeScene(int sceneIndex)
    {
        if (sceneIndex != 0) // Nicht im Hauptmenü
        {
            FindUIReferences();
            ResetGameState();
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            SceneChanger.ShowCursor();
        }

        if (_GameOverCanvas != null)
        {
            _GameOverCanvas.SetActive(false);
            _isCanvasActiveGameOverCanvas = false;
        }
        SceneChanger.SetCursor();
    }

    private void FindUIReferences()
    {
        _GameOverCanvas = GameObject.Find("GameOverCanvas");

        if (_GameOverCanvas != null)
        {
            skull_UI = GameObject.Find("Skull_UI")?.GetComponent<TMP_Text>();
            health_UI = GameObject.Find("Health_UI")?.GetComponent<TMP_Text>();
            time_UI = GameObject.Find("Time_UI")?.GetComponent<TMP_Text>();

        }
        else
        {
            Debug.LogError("GameOverCanvas not found!");
        }
    }

    private void ResetGameState()
    {
        _skullsCollected = 0;
        _HealthLeft = _Health;

        switch ((SceneManager.GetActiveScene().buildIndex))
        {
            case 1: 
                timeLeft = timeLevel1; 
                break;
            case 2:
                timeLeft = timeLevel2;
                break;
            case 3:
                timeLeft = timeLevel3;
                break;
            default:
                break;
        }

        _skullsNumberToCollect = GameObject.FindGameObjectsWithTag("whatIsSkull_Tag").Length;
        UpdateUI();
    }

    private void HandleGameTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            time_UI.text = $"Time: {timeLeft:F2}";
        }
        else
        {
            GameOver();
        }
    }

    private void HandleEscapeInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        _isCanvasActiveGameOverCanvas = !_isCanvasActiveGameOverCanvas;

        if (_GameOverCanvas != null)
        {
            _GameOverCanvas.SetActive(_isCanvasActiveGameOverCanvas);
            Time.timeScale = _isCanvasActiveGameOverCanvas ? 0 : 1;
            SceneChanger.SetCursor();
        }
    }

    public void UpdateSkullsCollected()
    {
        _skullsCollected++;
        UpdateUI();

        if (_skullsCollected >= _skullsNumberToCollect)
        {
            NextLevelTrigger._colliderPortal.enabled = true;
            OnAllSkullsCollected.Invoke();
            Debug.Log("All skulls collected");
        }
    }

    public void UpdateHealthLeft()
    {
        _HealthLeft--;
        UpdateUI();

        if (_HealthLeft <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        if (_GameOverCanvas != null) {
        _GameOverCanvas.SetActive(true);
        }
        SceneChanger.SetCursor();
    }

    private void UpdateUI()
    {
        if (skull_UI != null)
            skull_UI.text = $"Skulls: {_skullsCollected} / {_skullsNumberToCollect}";

        if (health_UI != null)
            health_UI.text = $"Health: {_HealthLeft} / {_Health}";

        if (time_UI != null)
            time_UI.text = $"Time: {timeLeft:F2}";
    }
}
