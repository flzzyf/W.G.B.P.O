using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {

    List<GameObject> targetList = new List<GameObject>();

    private void Update()
    {
        if(!GameSpeedManager.isPause && !GameSpeedManager.isSpeedUp)
        {
            if(targetList.Count > 0)
            {
                GameSpeedManager.SetTimeScale(0.4f);

            }
            else
            {
                GameSpeedManager.BackToNormal();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetList.Add(collision.gameObject);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targetList.Remove(collision.gameObject);

    }

}
