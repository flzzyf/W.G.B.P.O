using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Movement : MonoBehaviour {

    public float rotSpeed = 3;

    [HideInInspector]
    public int currentWayPointIndex = 0;

	Vector3 targetWayPoint;
	Unit unit;

	void Start ()
	{
		unit = GetComponent<Unit>();
		ReachTarget();
	}

	void Update ()
	{
		//朝下个点方向
		Vector3 dir = targetWayPoint - transform.position;
        //移动
        transform.Translate(dir.normalized * unit.speed * Time.deltaTime, Space.World);
        //旋转
        if (dir != Vector3.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            //旋转
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
        }
        //抵达
        if (dir.magnitude <= unit.speed * Time.deltaTime)
		{
			ReachTarget();
		}
    }

    //获取下个路径点
    void GetNextWayPoint()
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
			Destroy(gameObject);
            PlayerStats.instance.TakeDamage(unit.GetHp());

            GameManager.instance.EnemyReachTarget();
		}
		else
		{
			//继续走
			GetNextWayPoint();
		}
	}

}
