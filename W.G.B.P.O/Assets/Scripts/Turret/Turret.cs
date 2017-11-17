using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public float range = 3;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject reloadPicture;

    Animator animator;

    [HideInInspector]
    public int roundDamage = 0;

    public LayerMask enemyLayer;

    public AudioClip sound_Launch;

    [HideInInspector]
    public GameObject parentNode;

    protected void Init () 
	{
        animator = GetComponent<Animator>();
	}
	
	protected void Update ()
    {
        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;

            Vector3 scale = new Vector3(1, fireCountdown / fireRate, 1);
            reloadPicture.transform.localScale = scale;
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

    public virtual void Attack()
    {

        //播放音效
        SoundManager.instance.PlaySound(sound_Launch);

        animator.SetBool("Reloading", true);

		fireCountdown = fireRate;

    }

    protected Collider2D[] GetTarget()
    {
        Collider2D[] target = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        return target;
    }

    bool canAttack(){
        return fireCountdown <= 0;
    }

    //鼠标按下
    private void OnMouseDown()
    {
        GameManager.instance.draging = true;

        GameManager.instance.dragingTurret = gameObject;

    }
    //鼠标起来
    private void OnMouseUp()
    {
        if (GameManager.instance.draging)
        {
            GameManager.instance.draging = false;
        }else
        {
            //可攻击
            if (canAttack())
                Attack();
        }



    }

    private void OnMouseOver()
    {
        
    }


}
