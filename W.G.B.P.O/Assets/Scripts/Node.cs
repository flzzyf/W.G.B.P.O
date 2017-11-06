using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public GameObject turretToBuild;

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
            animator.SetBool("Hover", true);

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

    private void OnMouseUp()
    {
        if(HasTurret())
        {
            //已有炮塔
            turret.GetComponent<Turret>().Attack();

        }
        else
        {
            //未建造炮塔
            turret = Instantiate(turretToBuild, transform.position, Quaternion.identity, transform);
            animator.SetBool("Hover", false);

        }

    }

    bool HasTurret()
    {
        return turret == true;
    }
}
