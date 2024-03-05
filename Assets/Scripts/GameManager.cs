using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{

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
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            EnableGameplayInputs();
            LoadLevelOne();
        }
        else
        {
            EnableGameplayInputs();
            PauseUI = GameObject.FindGameObjectWithTag("PauseMenu");
            Instantiate(levels[0]);
            curPhase = GamePhase.LevelOne;
            ResumeGame();
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
        curPhase = GamePhase.Menu;
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
            curPhase = GamePhase.LevelOne;
            ResumeGame();
        }

        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }
    public void LoadLevelTwo()
    {
        Instantiate(levels[1]);
        curPhase = GamePhase.LevelTwo;
    }
    public void LoadLevelThree()
    {
        Instantiate(levels[2]);
        curPhase = GamePhase.LevelThree;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
