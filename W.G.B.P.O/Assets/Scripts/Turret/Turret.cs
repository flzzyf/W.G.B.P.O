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

    Vector3 dragOffset;
    bool dragingNoAttacking = false;

    Vector3 parentPos;

    public LayerMask ignoreLayer;

    LayerMask defaultLayer;

    protected void Init () 
	{
        animator = GetComponent<Animator>();

        parentPos = transform.parent.position;
        parentPos.z = 0;
        Debug.Log(parentPos);

        defaultLayer = gameObject.layer;

        Debug.Log(defaultLayer.ToString());

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

        dragOffset = GetMousePos() - transform.position;
        dragOffset.z = 0;

        gameObject.layer = ignoreLayer;



    }
    //鼠标起来
    private void OnMouseUp()
    {
        GameManager.instance.draging = false;

        if(!dragingNoAttacking)
        {
            //可攻击
            if (canAttack())
                Attack();
        }

        dragingNoAttacking = false;


        //gameObject.layer = LayerMask.NameToLayer("Default");

    }

    private void OnMouseDrag()
    {
        Vector3 pos = GetMousePos() - dragOffset;
        transform.position = pos;

        if(Vector3.Distance(pos, parentPos) > 0.3)
        {
            //Debug.Log(pos);
            //Debug.Log(transform.parent.position);
            dragingNoAttacking = true;
        }
    }
    //获取鼠标位置
    Vector3 GetMousePos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        return pos;
    }

}
