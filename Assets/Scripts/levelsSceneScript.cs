using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelsSceneScript : MonoBehaviour
{
    
    [SerializeField]
    Button lvlBtn, LockedLvlBtn;
    [SerializeField]
    Transform levelsGrid,LoadingPanel;
    [SerializeField]
    Slider progressBar;
    [SerializeField]
    Text progressTxt;
    

    public static int selectedLevel;

    private Dictionary<Button,int> clickableButtons = new Dictionary<Button, int>();

    PlayerData pData;
    int playerLevel;
    int totalNumOfLevels;
    private void Start()
    {
        pData = LevelManager.LoadGame();
        playerLevel = pData.getLevel();
     //   Debug.Log("PlayerLevel = " + playerLevel);
        totalNumOfLevels = playerLevel + 10;
        
       for(int i = 1; i<=playerLevel; i++)
        {
            Button btn = Instantiate(lvlBtn);
            btn.GetComponentInChildren<Text>().text = i+"";
            btn.transform.SetParent(levelsGrid);
            btn.transform.localScale = new Vector3(1, 1, 1);
            clickableButtons.Add(btn, i);
        }

        for (int i = playerLevel+1; i<totalNumOfLevels; i++)
        {
            Button btn = Instantiate(LockedLvlBtn);
            btn.transform.SetParent(levelsGrid);
            btn.transform.localScale = new Vector3(1, 1, 1);
        }

        //Bind Each Button With Its Level.
        foreach (KeyValuePair<Button,int> entry in clickableButtons)
        {
            entry.Key.onClick.AddListener(() => LaodLevel(entry.Value));
        }
    }

    private void LaodLevel(int levelNumber)
    {
       // Debug.Log("Loading level: "+levelNumber);
        selectedLevel = levelNumber;
        StartCoroutine(LoadAsynchronously(2));
    }

    private IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingPanel.gameObject.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            progressBar.value = progress;
            progressTxt.text = Mathf.FloorToInt(progress * 100f) + "%";

            yield return null;
        }
    }
}