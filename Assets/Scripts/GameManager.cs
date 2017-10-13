using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public enum GameState
    {
        PAUSE,
        STARTED
    }
    
    public GameState gameState { get; set; }

    public GameObject gameRuleUI;
    public GameObject winGameUI;
    public GameObject loseGameUI;

    public int needCaughtPokemonNumToWin;

    [HideInInspector] public int caughtPokemonNum;

    GameObject player;

    // Use this for initialization
    void Start () {
        if(instance == null)
        {
            instance = this;
            player = GameObject.FindGameObjectWithTag("Player");
            AudioManager.instance.PlayBGM("monsterWorld");
            ShowGameRuleUI();
        }
        else
        {
            instance.gameRuleUI = this.gameRuleUI;
            instance.winGameUI = this.winGameUI;
            instance.loseGameUI = this.loseGameUI;

            instance.player = GameObject.FindGameObjectWithTag("Player");
            Destroy(gameObject);
            return;
        }
    }

    public void ShowGameRuleUI()
    {
        gameState = GameState.PAUSE;
        player.GetComponent<Rigidbody>().Sleep();
        gameRuleUI.SetActive(true);
    }

    public void HideGameRuleUI()
    {
        gameState = GameManager.GameState.STARTED;
        AudioManager.instance.PlaySoundEffect("start");
        gameRuleUI.SetActive(false);
        player.GetComponent<Rigidbody>().WakeUp();
    }

    public void ShowLoseGameUI()
    {
        if (winGameUI.activeSelf)
            return;
        AudioManager.instance.PlaySoundEffect("lose");
        gameState = GameManager.GameState.PAUSE;
        loseGameUI.SetActive(true);
    }

    public void ShowWinGameUI()
    {
        if (loseGameUI.activeSelf)
            return;
        AudioManager.instance.PlaySoundEffect("win");
        gameState = GameManager.GameState.PAUSE;
        winGameUI.SetActive(true);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MiniGame");
        gameState = GameManager.GameState.STARTED;
        caughtPokemonNum = 0;
        AudioManager.instance.PlaySoundEffect("start");
    }
}
