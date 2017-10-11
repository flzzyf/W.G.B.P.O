using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Movement : MonoBehaviour {

	int currentWayPointIndex = 0;
	Vector3 targetWayPoint;
	Unit unit;

	void Start ()
	{
		unit = GetComponent<Unit>();
		ReachTarget();
	}

	void Update ()
	{
		//朝下个点移动
		Vector3 dir = targetWayPoint - transform.position;

		transform.Translate(dir.normalized * unit.speed * Time.deltaTime, Space.World);


		if(dir.magnitude <= unit.speed * Time.deltaTime)
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
		}
		else
		{
			//继续走
			GetNextWayPoint();
		}
	}

}
