using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public GameObject turret;
    Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();

    }

    //鼠标悬浮
    private void OnMouseOver()
    {
        animator.SetBool("Hover", true);

        if (HasTurret())
        {
            //已有炮塔
            //turret.GetComponent<Animator>().SetBool("Hover", true);
        }
        else
        {
            //无炮塔
            if (GameManager.instance.draging)
            {
                //移动炮塔到新节点
                GameManager.instance.dragingTurret.GetComponent<Node>().turret.transform.position = transform.position;

                turret = GameManager.instance.dragingTurret;


                GameManager.instance.dragingTurret.GetComponent<Node>().ClearTurret();


                GameManager.instance.dragingTurret = gameObject;

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
            turret.GetComponent<Animator>().SetBool("Hover", false);
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
