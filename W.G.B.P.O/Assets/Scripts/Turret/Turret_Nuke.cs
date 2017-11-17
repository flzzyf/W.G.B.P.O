using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Nuke : Turret {

    public GameObject effect_Launch;

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
        foreach (Collider2D item in GetTarget())
        {
            item.gameObject.GetComponent<Unit>().TakeDamage(1);

        }
    }


}
