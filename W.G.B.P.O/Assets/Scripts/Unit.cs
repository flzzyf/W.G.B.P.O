using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public float speed = 3;

    public int maxHp = 1;
    int currentHp = 1;

    public GameObject GFX;

    bool isDead = false;

    Renderer renderer;

    Animator animator;

    private void Awake()
    {
        renderer = GFX.GetComponent<Renderer>();

    }

    private void Start()
    {
        currentHp = maxHp;

        AppearanceModify();

        animator = GetComponent<Animator>();


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
        renderer.material.color = ColorManager.instance.GetColor(currentHp - 1);
    }

    void Death()
    {
        Destroy(gameObject);
    }

    public int GetHp()
    {
        return currentHp;
    }

}
