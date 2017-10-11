using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour {

	public static Transform[] wayPoints;

	private void Awake()
	{
		//将所有子物体放入数组
		wayPoints = new Transform[transform.childCount];
		for (int i = 0; i < wayPoints.Length; i++)
		{
			wayPoints[i] = transform.GetChild(i);
		}
	}

}
