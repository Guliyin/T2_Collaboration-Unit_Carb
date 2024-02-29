using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
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
        curPhase = GamePhase.Menu;
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ReloadCurrentLevel()
    {
        print("Restart");
    }
    public void LoadMenu()
    {

    }
    public void LoadLevelOne()
    {

    }
    public void LoadLevelTwo()
    {

    }
    public void LoadLevelThree()
    {

    }
}
