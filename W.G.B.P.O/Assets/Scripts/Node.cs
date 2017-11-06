using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    GameObject turret;
    Animator animator;


    void Start ()
    {
        animator = GetComponent<Animator>();

    }

    //鼠标悬浮
    private void OnMouseOver()
    {
        if (HasTurret())
        {
            //已有炮塔
            turret.GetComponent<Animator>().SetBool("Hover", true);
        }
        else
        {
            //无炮塔

            animator.SetBool("Hover", true);


            if (GameManager.instance.draging)
            {

            }
            else
            {

            }

        }

    }
    //鼠标离开
    private void OnMouseExit()
    {
        if (HasTurret())
        {
            //已有炮塔
            turret.GetComponent<Animator>().SetBool("Hover", false);
        }
        else
        {
            animator.SetBool("Hover", false);

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

        }


    }

    private void OnMouseUp()
    {
        GameManager.instance.draging = false;

        if (HasTurret())
        {
            //已有炮塔
            turret.GetComponent<Turret>().Attack();

        }

    }

    private void OnMouseDrag()
    {
        if (HasTurret())
        {
            //已有炮塔

            //获取鼠标位置点
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePoint = ray.origin;
            //移动炮台到鼠标
            turret.transform.position = mousePoint;
        }
        else
        {
            //无炮塔

        }



    }

    public void BuildTurret(GameObject _turret)
    {
        turret = Instantiate(_turret, transform.position, Quaternion.identity, transform);

    }
}
