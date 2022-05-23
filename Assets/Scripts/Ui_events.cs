
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui_events : MonoBehaviour
{
    public static int totalBalance;
    private const int KNIGHT1 = 1, KNIGHT2 = 2, KNIGHT3 = 3;

    //prefabs
    public GameObject knight1, knight2, knight3;

    [SerializeField]
    Transform castle; //-> Hero's castle

    [SerializeField]
    Text levelsTxt, balanceTxt, healthTxt;

    [SerializeField]
    Button knight1Btn, knight2Btn, knight3Btn;

    [SerializeField]
    Image knight1BtnOverlay, knight2BtnOverlay, knight3BtnOverlay;

    [SerializeField]
    AudioSource playAudio, winAudio, loseAudio;

    [SerializeField]
    RectTransform pausePanel, winPanel;

    private bool knight1Delay, knight2Delay, knight3Delay;

    private int currentSortingOrder = 1;

    private Vector3 spawnPosition;

    bool canPlay = true;

    [SerializeField]
    Text winTitlePanel, score, killes, level;

    [SerializeField]
    Image star1, star2, star3;

    [SerializeField]
    Sprite starImage;

    [SerializeField]
    Button moveToNextLvlBtn;

    private int currentPlayerLevel;

    private void Start()
    {
       
        currentPlayerLevel = levelsSceneScript.selectedLevel;
        EnemyCastleScript.setUpCastleForLevel(currentPlayerLevel);
        HerosCastleScript.setUpCastleForLevel(currentPlayerLevel);
        SetupStartMoneyForLevel(currentPlayerLevel);
        levelsTxt.text = "Level " + currentPlayerLevel;
        balanceTxt.text = totalBalance + " $";
        spawnPosition = new Vector3(castle.position.x + 2, -1, 0);
    }
    private void Update()
    {
        knight1Btn.interactable = (totalBalance >= 5); 
        knight2Btn.interactable = (totalBalance >= 15); 
        knight3Btn.interactable = (totalBalance >= 30);

        knight1Delay =  ControlBtn(knight1Delay, knight1Btn, knight1BtnOverlay);
        knight2Delay =  ControlBtn(knight2Delay, knight2Btn, knight2BtnOverlay);
        knight3Delay =  ControlBtn(knight3Delay, knight3Btn, knight3BtnOverlay);

        updateBalanceTxt();

        if (EnemyCastleScript.isWin)
        {
            if (canPlay)
            {
                winAudio.PlayOneShot(winAudio.clip);
                canPlay = false;
                winPanel.gameObject.SetActive(true);
                score.text = (ORCsScript.killes * 5).ToString();
                killes.text = ORCsScript.killes.ToString();
                moveToNextLvlBtn.gameObject.SetActive(true);
                Time.timeScale = 0;
                LevelManager.SaveGame(new PlayerData(currentPlayerLevel+1)); // currentLevel
                EnemyCastleScript.isWin = false; //resetting the game
            }
            
        }

        if (HerosCastleScript.isLose)
        {
            if (canPlay)
            {
                loseAudio.PlayOneShot(loseAudio.clip);
                canPlay = false;
                winPanel.gameObject.SetActive(true);
                winTitlePanel.text = "Game Over";
                star1.sprite = starImage;
                star2.sprite = starImage;
                star3.sprite = starImage;
                moveToNextLvlBtn.gameObject.SetActive(false);
                score.text = (ORCsScript.killes * 5).ToString();
                killes.text = ORCsScript.killes.ToString();
                Time.timeScale = 0;
                HerosCastleScript.isLose = false;
            }
        }

    }

    public void instantiateKnight(int type)
    {
        buyKnight(type, totalBalance);
    }

    private void buyKnight(int type, int balance)
    {
        GameObject heroInstance= null;
        switch (type)
        {
            case KNIGHT1:
                if (balance >= 5)
                {
                    totalBalance -= 5;
                    heroInstance = Instantiate(knight1, spawnPosition, knight1.transform.rotation);
                    knight1BtnOverlay.gameObject.SetActive(true);
                    knight1Delay = true;
                }
                break;

            case KNIGHT2:
                if (balance >= 15)
                {
                    totalBalance -= 15;
                    heroInstance = Instantiate(knight2, spawnPosition, knight2.transform.rotation);
                    knight2BtnOverlay.gameObject.SetActive(true);
                    knight2Delay = true;
                }
                break;

            case KNIGHT3:
                if (balance >= 30)
                {
                    totalBalance -= 30;
                    heroInstance = Instantiate(knight3, spawnPosition, knight3.transform.rotation);
                    knight3BtnOverlay.gameObject.SetActive(true);
                    knight3Delay = true;
                }
                break;
        }
        heroInstance.GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder;
        currentSortingOrder++;
       // updateBalanceTxt();
    }

    private void updateBalanceTxt()
    {
        balanceTxt.text = totalBalance + " $";
    }

    //-> For Buy Buttons
    private bool ControlBtn(bool isDelaied,Button btn, Image overlay)
    {
        if (isDelaied)
        {
            btn.interactable = false;
            overlay.fillAmount -= 0.35f * Time.deltaTime;
            if (overlay.fillAmount <= 0)
            {
                isDelaied = false;
                overlay.fillAmount = 1;
                overlay.gameObject.SetActive(false);
                btn.interactable = true;
            }
        }

        return isDelaied;

    }

    public void PauseGame()
    {
        pausePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        ResumeGame();
        SceneManager.LoadScene("StartScene");
    }

    public void RestartGame()
    {
        ORCsScript.AliveORCsList.Clear();
        HerosScript.AliveHerosList.Clear();
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevelBtn()
    {
        levelsSceneScript.selectedLevel = currentPlayerLevel + 1;
        RestartGame();
    }
    public void WinGame()
    {
        winAudio.PlayOneShot(winAudio.clip);
    }

    public void LoseGame()
    {
        loseAudio.PlayOneShot(loseAudio.clip);
    }

    private void SetupStartMoneyForLevel(int level)
    {
        totalBalance = 15 * level;
    }
}
