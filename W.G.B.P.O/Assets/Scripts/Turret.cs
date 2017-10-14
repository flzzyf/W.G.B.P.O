using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public float range = 3;

    public GameObject explode;

	CircleCollider2D collider;

    Animator animator;

    List<GameObject> targetList = new List<GameObject>();

    #region 用Collider获取可攻击目标列表
    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetList.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targetList.Remove(collision.gameObject);
    }

    #endregion

    void Start () 
	{
		collider = GetComponent<CircleCollider2D> ();
		collider.radius = range;

        animator = GetComponent<Animator>();
	}
	
	void Update () {
		
	}

    //选中显示范围
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, range);
	}

    public void Attack()
    {
        GameObject fx = Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(fx, 0.5f);

        GameObject[] targets = new GameObject[targetList.Count];

        targets = targetList.ToArray();

        foreach (GameObject target in targets)
        {
            target.GetComponent<Unit>().TakeDamage(1);

        }
    }
}
