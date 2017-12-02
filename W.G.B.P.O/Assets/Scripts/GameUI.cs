using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    public Text pauseText;

    public GameObject pausePanel;

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        GameSpeedManager.isPause = false;

        GameSpeedManager.BackToNormal();

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if (GameSpeedManager.isPause)
        {
            //取消暂停
            GameSpeedManager.isPause = false;

            pauseText.text = "暂停";

            GameSpeedManager.BackToNormal();

            pausePanel.SetActive(false);


        }
        else
        {
            //暂停
            GameSpeedManager.isPause = true;

            pauseText.text = "继续";

            GameSpeedManager.SetTimeScale(0);

            pausePanel.SetActive(true);

        }
    }

    public void SpeedUp()
    {
        if (GameSpeedManager.isPause)
            return;

        GameSpeedManager.isSpeedUp = true;

        GameSpeedManager.SetTimeScale(3);

    }

    public void SpeedDown()
    {
        if (GameSpeedManager.isPause)
            return;

        GameSpeedManager.isSpeedUp = false;

        GameSpeedManager.SetTimeScale(1);

    }

}
