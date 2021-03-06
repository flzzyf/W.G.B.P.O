﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Nuke : Turret
{
    public GameObject effect_Launch;

    public override void Attack()
    {
        base.Attack();

		gfx.GetComponent<Animator>().SetTrigger("Attack");
    }

	public void NukeAttack()
	{
		//播放音效
		SoundManager.instance.PlaySound(sound_Launch);

		//创建特效
		GameObject fx = Instantiate(effect_Launch, transform.position, Quaternion.identity);
		Destroy(fx, 0.5f);

		//镜头抖动
		CameraShake.instance.Shake(.15f, .1f);

		//搜索攻击
		foreach (GameObject item in GetTargets())
		{
			item.GetComponent<Unit>().TakeDamage(1);

			//施力
			item.GetComponent<Rigidbody2D>().AddForce((item.transform.position - transform.position).normalized * force, ForceMode2D.Impulse);

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
            if (item.gameObject.tag == GameManager.enemyTag)
            {
                targets.Add(item.gameObject);
            }
        }

        return targets;
    }
}
