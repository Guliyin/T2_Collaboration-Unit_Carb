using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{

    [HideInInspector] public PlayerInputActions playerInputActions;
    GameObject PauseUI;

    [SerializeField] Vector3 resetPos;
    [SerializeField] float deadTimer;
    [SerializeField] GameObject[] levels;
    GameObject[] LevelTemp = new GameObject[3];

    private bool isPaused;
    public bool EnableGrowth = true;
    private float volume;
    private float sensitivity;
    public float Volume => volume;
    public float Sensitivity => sensitivity;

    private int damageDealtAll;
    private int damageDealtOnce;
    private float gameStartTime;
    private int deathCount;
    private int damageBonus;
    public int DamageDealtAll => damageDealtAll;
    public int DamageDealtOnce => damageDealtOnce;
    public int DamageBounus => damageBonus;
    public int DeathCount => deathCount;
    public float GameTime => Time.time - gameStartTime;

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
        EventCenter.AddListener(FunctionType.PlayerDead, PlayerDead);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            curPhase = GamePhase.Menu;
            DisableGameplayInputs();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            EnableGameplayInputs();
            StartGame();
            PauseUI = GameObject.FindGameObjectWithTag("PauseMenu");
            LevelTemp[0] = Instantiate(levels[0]);
            curPhase = GamePhase.LevelOne;
            ResumeGame();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {

        }
        else
        {
            EnableGameplayInputs();
            PauseUI = GameObject.FindGameObjectWithTag("PauseMenu");
            LevelTemp[0] = Instantiate(levels[0]);
            curPhase = GamePhase.LevelOne;
            ResumeGame();
        }

        ChangeVolume(0.5f);
        MouseSensitivity(0.3f);
        SetPlayerGrowth(true);

        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (playerInputActions.Gameplay.Pause.WasPerformedThisFrame() || playerInputActions.UI.Continue.WasPerformedThisFrame())
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
    void StartGame()
    {
        gameStartTime = Time.time;
        deathCount = 0;
        damageDealtAll = 0;
        damageDealtOnce = 0;
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
    public void ChangeVolume(float amount)
    {
        volume = amount;
    }
    public void MouseSensitivity(float amount)
    {
        sensitivity = amount;
    }
    public void SetPlayerGrowth(bool b)
    {
        EnableGrowth = b;
    }
    public void ResetPlayerPos()
    {
        var player = FindObjectOfType<PlayerController>();
        player.gameObject.transform.position = resetPos;
    }
    public void ReloadCurrentLevel()
    {
        foreach (var level in LevelTemp)
        {
            if (level != null)
            {
                Destroy(level);
            }
        }
        ResetPlayerPos();
        switch ((int)curPhase)
        {
            case 0:
                LoadMenu();
                break;
            case 1:
                LoadLevelOne();
                break;
            case 2:
                LoadLevelTwo();
                break;
            case 3:
                LoadLevelThree();
                break;
            default:
                LoadMenu();
                break;
        }
    }
    public void LoadMenu()
    {
        Loader.Load(Loader.Scene.MenuScene);
        curPhase = GamePhase.Menu;
    }
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            DisableGameplayInputs();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            EnableGameplayInputs();
            StartGame();
            PauseUI = GameObject.FindGameObjectWithTag("PauseMenu");
            LevelTemp[0] = Instantiate(levels[0]);
            curPhase = GamePhase.LevelOne;
            ResumeGame();
        }

        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }
    public void LoadLevelOne()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            LevelTemp[0] = Instantiate(levels[0]);
        }
        else
        {
            Loader.Load(Loader.Scene.GameScene);
        }
        curPhase = GamePhase.LevelOne;
    }
    public void LoadLevelTwo()
    {
        LevelTemp[1] = Instantiate(levels[1]);
        curPhase = GamePhase.LevelTwo;
    }
    public void LoadLevelThree()
    {
        LevelTemp[2] = Instantiate(levels[2]);
        curPhase = GamePhase.LevelThree;
    }
    public void CollectDamage(int amount)
    {
        damageDealtAll += amount;
        damageDealtOnce += amount;
    }
    void PlayerDead()
    {
        StartCoroutine(DeadTimer());
    }
    IEnumerator DeadTimer()
    {
        yield return new WaitForSeconds(deadTimer);
        damageBonus = Mathf.RoundToInt(damageDealtAll / 1000f);
        deathCount++;
        damageDealtOnce = 0;
        EventCenter.Broadcast(FunctionType.RestartLevel);
        ReloadCurrentLevel();
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
        EventCenter.RemoveListener(FunctionType.PlayerDead, PlayerDead);
    }
}
