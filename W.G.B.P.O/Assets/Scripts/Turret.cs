using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public float range = 3;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject explode;

    public GameObject reloadPicture;

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
	
	void Update ()
    {
        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;

            //Vector3 scale = reloadPicture.transform.localScale;
            Vector3 scale = new Vector3(1, fireCountdown / fireRate, 1);
            reloadPicture.transform.localScale = scale;
            //Debug.Log (fireCountdown);
        }
        else
        {
            animator.SetBool("Reloading", false);

        }
    }

    //选中显示范围
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, range);
	}

    public void Attack()
    {
		if(fireCountdown > 0){
			return;
		}

        animator.SetBool("Reloading", true);

		fireCountdown = fireRate;

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
