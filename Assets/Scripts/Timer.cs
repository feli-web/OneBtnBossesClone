using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float time;
    TextMeshProUGUI timerText;
    bool isRunning = true;
    public GameObject winScreen;
    public GameObject loseScreen;

    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        timerText = GetComponent<TextMeshProUGUI>();

        PlayerPrefs.GetFloat("HighScore");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRunning)
        {
            time += Time.deltaTime;
            timerText.text = time.ToString("F3");
        }
    }

    public void WinScenary()
    {
        winScreen.SetActive(true);
        Destroy(loseScreen);
        isRunning = false;
    }
    public void LoseScenary()
    {
        loseScreen.SetActive(true);
        Destroy(winScreen);
        isRunning = false;
    }
}
