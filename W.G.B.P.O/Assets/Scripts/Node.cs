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
                GameManager.instance.dragingNode.GetComponent<Node>().turret.transform.position = transform.position;

                turret = GameManager.instance.dragingNode.GetComponent<Node>().turret;


                GameManager.instance.dragingNode.GetComponent<Node>().ClearTurret();


                GameManager.instance.dragingNode = gameObject;

                turret.transform.SetParent(transform);

            }
            else
            {

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

    private void OnMouseDown()
    {
        if (HasTurret())
        {
            //已有炮塔
            GameManager.instance.draging = true;

            GameManager.instance.dragingNode = gameObject;

        }


    }

    private void OnMouseUp()
    {
        if(GameManager.instance.draging)
        {
            GameManager.instance.draging = false;
        }

        if (HasTurret())
        {
            //已有炮塔
            turret.GetComponent<Turret>().Attack();

        }

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
