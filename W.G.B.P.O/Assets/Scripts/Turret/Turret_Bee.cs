using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Bee : Turret {

    public GameObject missilePrefab;

    void Start()
    {
        Init();
    }

    void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();

        //搜索攻击
        foreach (GameObject item in GetTargets())
        {
            Debug.Log("att");
            LaunchMissile(item.transform);

            //回合伤害量增加
            roundDamage++;
        }
    }

    List<GameObject> GetTargets()
    {
        List<GameObject> targets = new List<GameObject>();

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var item in cols)
        {
            if (item.gameObject.tag == enemyTag)
            {
                targets.Add(item.gameObject);
            }
        }

        return targets;
    }

    void LaunchMissile(Transform _target)
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);

        missile.GetComponent<HomingMissile>().target = _target;
    }

}
