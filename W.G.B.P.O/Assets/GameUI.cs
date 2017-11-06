using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    public Text pauseText;

    public Button speedUpButton;

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

            GameSpeedManager.SetTimeScale(1);

        }
        else
        {
            //暂停
            GameSpeedManager.isPause = true;

            pauseText.text = "继续";

            GameSpeedManager.SetTimeScale(0);

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
