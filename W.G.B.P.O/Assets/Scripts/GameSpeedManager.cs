using UnityEngine;

public class GameSpeedManager : MonoBehaviour {

    private void Update()
    {
        //按1加快，2减慢，3恢复正常时间速度
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetTimeScale(Time.timeScale + 1);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetTimeScale(Time.timeScale - 0.1f);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && Time.timeScale > 0)
        {
            SetTimeScale(1);

        }

    }

    public static void SetTimeScale(float _amount)
    {
        Time.timeScale = _amount;
        Time.fixedDeltaTime = Time.timeScale * .02f;

    }

}
