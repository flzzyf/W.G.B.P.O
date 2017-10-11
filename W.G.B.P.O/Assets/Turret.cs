using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public float range = 3;

	CircleCollider2D collider;

	#region 用Collider获取可攻击目标列表
	List<GameObject> targetList = new List<GameObject>();

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log ("trigger");
		targetList.Add(other.gameObject);

	}

	private void OnTriggerExit(Collider other)
	{
		targetList.Remove(other.gameObject);
	}
	#endregion

	void Start () 
	{
		collider = GetComponent<CircleCollider2D> ();
		collider.radius = range;
	}
	
	void Update () {
		
	}

	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
