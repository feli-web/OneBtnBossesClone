using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    Timer timer;
    GameObject winScreen;
    GameObject loseScreen;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        winScreen = GameObject.Find("YouWin");
        loseScreen = GameObject.Find("YouLose");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
