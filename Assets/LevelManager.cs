using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [Header("Levels")]
    public GameObject levelsGameObject;
    public GameObject[] usefulThings;
    public GameObject[] levels;
    public GameObject winPanel;

    [Header("GroundLevels")]
    public GameObject groundLevelsGameObject;
    public GameObject[] groundLevels;

    [Header("UIPanel")]
    public GameObject notificationPanel;
    public Text notificationText;
    void Start()
    {
        instance = this;
        ActiveUsefulThings();
        StartGame();
        
    }
    public void ActiveUsefulThings()
    {
        foreach (var things in usefulThings)
        {
            things.SetActive(true);
        }
    }
    public void StartGame()
    {
        levels[0].SetActive(true);
    }
    public void LevelComplete()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void NotificationSet(string value)
    {
        notificationText.text = value;
        StartCoroutine(ShowNotification());
    }
    IEnumerator ShowNotification()
    {
        notificationPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        notificationPanel.SetActive(false);
    }
}
