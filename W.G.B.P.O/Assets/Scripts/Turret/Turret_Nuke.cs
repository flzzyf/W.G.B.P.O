using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Nuke : Turret {

    public GameObject effect_Launch;

    public GameObject rangeDisplay;

    void Start () {
        Init();
	}
	
	void Update () {
        base.Update();
	}

    public override void Attack()
    {
        base.Attack();

        //创建特效
        GameObject fx = Instantiate(effect_Launch, transform.position, Quaternion.identity);
        Destroy(fx, 0.5f);

        //搜索攻击
        foreach (GameObject item in GetTargets())
        {
            item.GetComponent<Unit>().TakeDamage(1);
            
            //回合伤害量增加
            roundDamage++;
        }
    }

    List<GameObject> GetTargets()
    {
        List<GameObject> targets = new List<GameObject>();

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var item in cols)
        {
            if(item.gameObject.tag == enemyTag)
            {
                targets.Add(item.gameObject);
            }
        }

        return targets;
    }

    private void OnMouseOver()
    {
        rangeDisplay.SetActive(true);
    }

    private void OnMouseExit()
    {
        rangeDisplay.SetActive(false);

    }


}
