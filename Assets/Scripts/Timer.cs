using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
    float time;
    TextMeshProUGUI timerText;
    bool isRunning = true;
    public GameObject winScreen;
    public GameObject loseScreen;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI highScore;

    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRunning)
        {
            time += Time.deltaTime;
            timerText.text = time.ToString("F2");
        }
    }

    public void WinScenary()
    {
        winScreen.SetActive(true);
        Destroy(loseScreen);
        isRunning = false;
        if (time <= PlayerPrefs.GetFloat("HighScore", 10000))
        {
            PlayerPrefs.SetFloat("HighScore", time);
        }
        currentScore.text ="Time: " + time.ToString("F2");
        highScore.text = $"Best Time: {PlayerPrefs.GetFloat("HighScore", 0).ToString("F2")}";
    }
    public void LoseScenary()
    {
        loseScreen.SetActive(true);
        Destroy(winScreen);
        isRunning = false;
    }
}
