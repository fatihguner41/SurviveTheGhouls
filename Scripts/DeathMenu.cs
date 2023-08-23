using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    TMP_Text scoreText;
    TMP_Text highscoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        highscoreText = GameObject.Find("Highscore").GetComponent<TMP_Text>();

        scoreText.text = scoreText.text + PlayerPrefs.GetInt("score");
        highscoreText.text = highscoreText.text + PlayerPrefs.GetInt("highscore");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    
}
