using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {

    protected static string enemyTag = "Enemy";

    public float range = 3f;

    private void Update()
    {
        if(!GameSpeedManager.isPause && !GameSpeedManager.isSpeedUp)
        {
            if(GetTargets().Count > 0)
            {
                GameSpeedManager.SetTimeScale(0.4f);

            }
            else
            {
                GameSpeedManager.BackToNormal();

            }
        }
    }

    private void OnDestroy()
    {
        GameSpeedManager.BackToNormal();

    }

    List<GameObject> GetTargets()
    {
        List<GameObject> targets = new List<GameObject>();

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (var item in cols)
        {
            if (item.gameObject.tag == enemyTag)
            {
                targets.Add(item.gameObject);
            }
        }

        return targets;
    }

    //选中显示范围
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
