using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    [HideInInspector]
    public GameObject turret;
    Animator animator;

    public GameObject gfx;

    void Start()
    {
        animator = gfx.GetComponent<Animator>();

    }

    //鼠标悬浮
    private void OnMouseEnter()
    {
        if (HasTurret())
        {
            //已有炮塔
            //turret.GetComponent<Animator>().SetBool("Hover", true);
        }
        else
        {
            animator.SetBool("Hover", true);

            //无炮塔
            if (GameManager.instance.draging)
            {

                turret = GameManager.instance.dragingTurret;
                //前任父级节点清除炮塔链接
                turret.transform.parent.GetComponent<Node>().ClearTurret();
                //设置炮塔新的父级节点
                turret.transform.SetParent(transform);
            }
        }
    }
    //鼠标离开
    private void OnMouseExit()
    {
        animator.SetBool("Hover", false);


        if (HasTurret())
        {
            //已有炮塔
            //turret.GetComponent<Animator>().SetBool("Hover", false);
        }
        else
        {

        }

    }

    bool HasTurret()
    {
        return turret == true;
    }

    public void BuildTurret(GameObject _turret)
    {
        turret = Instantiate(_turret, transform.position, Quaternion.identity, transform);

    }

    public void ClearTurret()
    {
        turret = null;
    }
}
