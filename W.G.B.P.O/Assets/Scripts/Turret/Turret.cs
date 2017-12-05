using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public float range = 3;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject reloadPicture;

    Animator animator;

    //回合造成伤害
    protected int roundDamage = 0;

    public SoundEffect sound_Launch;

    Vector3 dragOffset;
    bool dragingNoAttacking = false;

    public LayerMask ignoreLayer;

    LayerMask defaultLayer;

    protected static string enemyTag = "Enemy";

    public GameObject gfx;

    protected void Init () 
	{
        animator = gfx.GetComponent<Animator>();
        
        defaultLayer = gameObject.layer;

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

    bool canAttack(){
        return fireCountdown <= 0;
    }

    //鼠标按下
    private void OnMouseDown()
    {
        Debug.Log("Turret");

        GameManager.instance.draging = true;

        GameManager.instance.dragingTurret = gameObject;

        dragOffset = GetMousePos() - transform.position;
        dragOffset.z = 0;




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

        gameObject.layer = defaultLayer;

        transform.position = transform.parent.position + new Vector3(0, 0, -3);

    }
    //鼠标拖动
    private void OnMouseDrag()
    {
        Vector3 pos = GetMousePos() - dragOffset;
        transform.position = pos;

        Vector3 parentPos = transform.parent.position;
        parentPos.z = 0;

        if(Vector3.Distance(pos, parentPos) > 0.3)
        {
            gameObject.layer = ignoreLayer;

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

    public void ClearRoundDamage()
    {
        roundDamage = 0;
    }

}
