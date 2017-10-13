using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public float speed = 3;

    public int maxHp = 1;
    int currentHp;

    public Color[] color = new Color[5];

    bool isDead = false;

    Renderer renderer;

    private void Start()
    {
        currentHp = maxHp;

        renderer = GetComponent<Renderer>();
        AppearanceModify();

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

    //修改外形
    void AppearanceModify()
    {
        renderer.material.color = color[currentHp - 1];
    }

    void Death()
    {
        Destroy(gameObject);
    }

}
