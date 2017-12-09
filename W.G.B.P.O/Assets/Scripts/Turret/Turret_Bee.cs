using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Bee : Turret {

    public GameObject missilePrefab;

    public int attackCount = 3;

    public GameObject rangeDisplay;

    void Start()
    {
        Init();

        rangeDisplay = Instantiate(rangeDisplay, transform.position, transform.rotation, transform);

        rangeDisplay.transform.localScale *= range;
    }

    void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();

        StartCoroutine(BeeAttack());
    }

    List<GameObject> GetTargets()
    {
        List<GameObject> targets = new List<GameObject>();

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var item in cols)
        {
            if (item.gameObject.tag == GameManager.enemyTag)
            {
                targets.Add(item.gameObject);
            }
        }

        return targets;
    }

    void LaunchMissile(Transform _target)
    {
        Quaternion rot = Quaternion.Euler(_target.position - transform.position);

        GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);

        missile.GetComponent<HomingMissile>().target = _target;

        missile.GetComponent<HomingMissile>().hive = transform;
    }

    IEnumerator BeeAttack(){

        for (int i = 0; i < attackCount; i++)
        {
            List<GameObject> list = GetTargetOrderedList();

            if(list.Count == 0){
                break;
            }

            LaunchMissile(list[0].transform);

            yield return new WaitForSeconds(.1f);

        }

    }

    List<GameObject> GetTargetOrderedList(){

        List<GameObject> list = GetTargets();

        for (int i = 0; i < list.Count - 1; i++)
        {
            int nextPoint1 = list[i].GetComponent<Unit_Movement>().currentWayPointIndex;
            int nextPoint2 = list[i + 1].GetComponent<Unit_Movement>().currentWayPointIndex;

            Vector3 nextPoint = WayPointManager.wayPoints[nextPoint1].position;

            //i在i+1之后
            if(nextPoint1 < nextPoint2 ||
               (nextPoint1 == nextPoint2 && 
                Vector3.Distance(nextPoint, list[i].transform.position) > 
                Vector3.Distance(nextPoint, list[i + 1].transform.position))){
                //对换顺序
                list.Reverse(i, 2);
            }
        }

        return list;

    }

    private void OnMouseEnter()
    {
        rangeDisplay.SetActive(true);
    }


    private void OnMouseExit()
    {
        if (!GameManager.instance.draging)
            rangeDisplay.SetActive(false);

    }

}