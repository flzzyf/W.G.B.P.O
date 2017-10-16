using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int maxHp = 5;
    int currentHp;

    public GameObject heart;
    Renderer renderer;

    bool isDead = false;

    public static PlayerStats instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start ()
    {
        currentHp = maxHp;

        renderer = heart.GetComponent<Renderer>();
        AppearanceModify();

    }

    //修改外形
    public void AppearanceModify()
    {
        renderer.material.color = ColorManager.instance.GetColor(currentHp - 1);
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
        Destroy(heart);
    }

}
