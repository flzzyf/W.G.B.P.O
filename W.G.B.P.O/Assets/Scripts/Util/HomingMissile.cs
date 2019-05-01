using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour {

    public Transform target;

    public float speed = 5f;

    public float rotSpeed = 300f;

    Rigidbody2D rb;

    [HideInInspector]
    public Transform hive;

    public bool returnHive = true;

    [Header("自动搜索新目标")]
    public bool autoSearchNewTarget = true;

    public float searchRange = 3f;

    public string targetTag = "Enemy";

	public Sound sound_Hit;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        if(target == null){
            //若目标为空则寻找新目标。没有则返回巢穴或自毁
            if(autoSearchNewTarget){
                if(GetNewTarget()){
                    return;
                }
            }

            if (returnHive)
                target = hive;
            else
                Destroy(gameObject);
        }
    }

    void FixedUpdate ()
    {
        if(target == null){
            return;
        }

        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotAmount * rotSpeed;

        rb.velocity = transform.up * speed;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag(GameManager.enemyTag) || collision.transform == target)
        {
            if (collision.gameObject.CompareTag(GameManager.enemyTag))
            {
                collision.gameObject.GetComponent<Unit>().TakeDamage(1);

                collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * 2, ForceMode2D.Impulse);

				//音效和镜头震动
				SoundManager.instance.PlaySound(sound_Hit);
				CameraShake.instance.Shake(.06f, .06f);
			}

            Destroy(gameObject);
        }
    }

    //能找到目标
    bool GetNewTarget(){
        //List<GameObject> targets = new List<GameObject>();

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, searchRange);

        foreach (var item in cols){
            if (item.gameObject.tag == targetTag){
                
                target = item.transform;

                return true;

            }

        }
        return false;

    }
}
