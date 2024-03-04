using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public static string GAMEPAD_CONTROL_SCHEME = "Gamepad";
    public static string MNK_CONTROL_SCHEME = "MNK";

    [HideInInspector] public PlayerInputActions playerInputActions;
    GameObject PauseUI;

    [SerializeField] GameObject[] levels;

    private bool isPaused;
    private void Awake()
    {
        object[] objs = FindObjectsOfType(typeof(GameManager));
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        playerInputActions = new PlayerInputActions();
    }

    public enum GamePhase
    {
        Menu,
        LevelOne,
        LevelTwo,
        LevelThree,
    }
    public GamePhase curPhase;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            DisableGameplayInputs();
        }
        else
        {
            EnableGameplayInputs();
            LoadLevelOne();
        }

        curPhase = GamePhase.Menu;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (playerInputActions.Gameplay.Pause.WasPerformedThisFrame()||playerInputActions.UI.Continue.WasPerformedThisFrame())
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void EnableGameplayInputs()
    {
        playerInputActions.Gameplay.Enable();
        playerInputActions.UI.Disable();
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void DisableGameplayInputs()
    {
        playerInputActions.Gameplay.Disable();
        playerInputActions.UI.Enable();
        Cursor.lockState = CursorLockMode.None;
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        PauseUI.SetActive(false);
        EnableGameplayInputs();
    }
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        PauseUI.SetActive(true);
        DisableGameplayInputs();
    }
    public void ResetPlayerPos()
    {

    }
    public void ReloadCurrentLevel()
    {
        print("Restart");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
    }
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            DisableGameplayInputs();
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            PauseUI = GameObject.FindGameObjectWithTag("PauseMenu");
            Instantiate(levels[0]);
            ResumeGame();
        }

        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }
    public void LoadLevelTwo()
    {
        Debug.Log("第二关启动");
    }
    public void LoadLevelThree()
    {

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
