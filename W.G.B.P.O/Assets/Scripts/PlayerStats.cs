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

        AppearanceModify();
    }

    //修改外形
    public void AppearanceModify()
    {
        heart.ChangeColor(currentHp);
    }

    //敌人抵达终点
    public void EnemyReachTarget(Unit _unit)
    {
        TakeDamage(_unit.GetHp());

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
                AppearanceModify();
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
