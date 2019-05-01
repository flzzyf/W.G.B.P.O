using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHp = 5;
    int currentHp;

    public Heart heart;

    bool isDead = false;

    public static PlayerStats instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        currentHp = maxHp;

		heart.ChangeColor(currentHp);
	}

	//敌人抵达终点
	public void EnemyReachTarget(Unit _unit)
    {
        TakeDamage(1);

        SoundManager.instance.PlaySound(GameManager.instance.enemyReachSound);
    }

    public void TakeDamage(int _amount)
    {
        if (!isDead)
        {
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
				heart.ChangeColor(currentHp);

				heart.Hit();
			}
		}
    }

    void Death()
    {
        //游戏失败
        GameManager.instance.GameLose();

        heart.Die();
    }

}
