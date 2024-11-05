using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float time;
    TextMeshProUGUI timerText;
    public bool isRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
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
}
