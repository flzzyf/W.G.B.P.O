using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Movement : MonoBehaviour
{
    public float rotSpeed = 3;

    [HideInInspector]
    public int currentWayPointIndex = 0;

    Vector3 targetWayPoint;
    Unit unit;

    void Start()
    {
        unit = GetComponent<Unit>();
        ReachTarget();
    }

    void Update()
    {
        if (!GameManager.instance.gaming)
            return;

        //朝下个点方向
        Vector3 dir = targetWayPoint - transform.position;

        //移动
        //GetComponent<Rigidbody2D>().MovePosition(transform.position +  dir.normalized * unit.speed * Time.deltaTime);
        transform.Translate(dir.normalized * unit.speed * Time.deltaTime, Space.World);

        //旋转
        if (dir != Vector3.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);

        }
        //抵达
        //距离下个点很近，或者和下个点的方向与下个点到下下个点方向差别过大
        //if (dir.magnitude <= unit.speed * Time.deltaTime || 
        //    Vector2.Angle(dir, WayPointManager.wayPoints[currentWayPointIndex + 1].position - WayPointManager.wayPoints[currentWayPointIndex].position) > 100)
        if (dir.magnitude <= .5f)
        {
            ReachTarget();
        }
        if (dir.magnitude <= unit.speed * Time.deltaTime)
        {
            ReachTarget();
        }


    }

    //获取下个路径点
    public void GetNextWayPoint()
    {
        targetWayPoint = WayPointManager.wayPoints[currentWayPointIndex].position;
    }

    //到达目标点
    void ReachTarget()
    {
        currentWayPointIndex++;

        if (currentWayPointIndex == WayPointManager.wayPoints.Length)
        {
            //抵达终点
            unit.Death();

            PlayerStats.instance.EnemyReachTarget(unit);
        }
        else
        {
            //继续走
            GetNextWayPoint();
        }
    }
}
