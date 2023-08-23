using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public TMP_Text highscoreText;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("highscore", 0);
        highscoreText = GameObject.Find("HighscoreText").GetComponent<TMP_Text>();

        highscoreText.text = highscoreText.text + PlayerPrefs.GetInt("highscore");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
    }

    public void Exit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
