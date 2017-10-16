using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {

    List<GameObject> targetList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetList.Add(collision.gameObject);

        if (targetList.Count == 1)
        {
            Debug.Log("减速");

            TimeSlow();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targetList.Remove(collision.gameObject);

        if(targetList.Count == 0)
        {
            GameSpeedManager.SetTimeScale(1f);

        }
    }

    void TimeSlow()
    {
        GameSpeedManager.SetTimeScale(0.4f);

    }
}
