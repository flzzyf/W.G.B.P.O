using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed = 3;

    public int maxHp = 1;
    int currentHp = 1;

    public GameObject gfx;

    bool isDead = false;

    SpriteRenderer spriteRenderer;
    Animator animator;

	//死亡后生成的单位
	public Unit generateUnit;

	public Sound sound_Death;

    private void Start()
    {
        currentHp = maxHp;

        spriteRenderer = gfx.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        AppearanceModify();
    }

    public void TakeDamage(int _amount)
    {
        if (!isDead)
        {
            animator.Play("Enemy_Hit");
            //还没死
            currentHp -= _amount;
            //Debug.Log(currentHp);

            if (currentHp <= 0)
            {
                //死了
                Death();
            }
            else
            {
                AppearanceModify();
            }
        }
    }

    //设置HP
    public void SetHp(int _amount)
    {
        currentHp = _amount;
        AppearanceModify();
    }

    //修改外形
    public void AppearanceModify()
    {
        spriteRenderer.material.color = ColorManager.instance.GetColor(currentHp - 1);
    }

    //死亡
    public void Death()
    {
        if (isDead)
            return;

        isDead = true;

        Destroy(gameObject);

		//如果是最低级敌人，敌人数量减1
		if(generateUnit == null)
			WaveSpawner.instance.enemiesAlive--;

        //播放死亡特效
        GameObject deathFx = Instantiate(GameManager.instance.colorParticle, transform.position, Quaternion.identity);
        Destroy(deathFx, 1.5f);

		//播放音效
		SoundManager.instance.PlaySound(sound_Death, true);

		//如果有死亡后生成单位，生成
		if(generateUnit != null)
		{
			Unit unit = Instantiate(generateUnit, transform.position, transform.rotation);
			unit.maxHp = 5;

			unit.GetComponent<Unit_Movement>().currentWayPointIndex = GetComponent<Unit_Movement>().currentWayPointIndex - 1;
			unit.GetComponent<Unit_Movement>().GetNextWayPoint();
		}
    }

    public int GetHp()
    {
        return currentHp;
    }

}
