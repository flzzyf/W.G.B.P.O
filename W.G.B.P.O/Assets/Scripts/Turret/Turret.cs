using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 3;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject reloadPicture;

    Animator animator;

    #region 回合造成伤害
    protected int roundDamage = 0;

    public int GetRoundDamage() { return roundDamage; }

    public void ClearRoundDamage() { roundDamage = 0; }

    #endregion

    public SoundEffect sound_Launch;

    Vector3 dragOffset;
    bool dragingNoAttacking = false;

    public LayerMask ignoreLayer;

    LayerMask defaultLayer;

    public GameObject gfx;

    public float force = 2;

    public GameObject rangeDisplay;

    void Start()
    {
        animator = gfx.GetComponent<Animator>();

        defaultLayer = gameObject.layer;

        //攻击范围显示
        rangeDisplay.transform.localScale *= range;
    }

    protected void Update()
    {
        if (!GameManager.instance.gaming)
            return;

        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;

            Vector3 scale = new Vector3(1, fireCountdown / fireRate, 1);
            reloadPicture.transform.localScale = scale;
        }
        else
        {
            //冷却完成状态
            animator.SetBool("Reloading", false);

            //拖动中无法攻击
            if (dragingNoAttacking)
                return;

            //搜索敌人
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (var item in cols)
            {
                if (item.gameObject.tag == GameManager.enemyTag)
                {
                    Attack();

                    break;
                }
            }
        }
    }

    //选中显示范围
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public virtual void Attack()
    {
        //播放音效
        SoundManager.instance.PlaySound(sound_Launch);

        animator.SetBool("Reloading", true);

        fireCountdown = fireRate;

    }

    bool canAttack()
    {
        return fireCountdown <= 0;
    }

    //鼠标按下
    private void OnMouseDown()
    {
        GameManager.instance.draging = true;

        GameManager.instance.dragingTurret = gameObject;

        dragOffset = GetMousePos() - transform.position;
        dragOffset.z = 0;

        //手机版点击显示范围
#if UNITY_IPHONE || UNITY_ANDROID
        rangeDisplay.SetActive(true);
#endif

    }
    //鼠标起来
    private void OnMouseUp()
    {
        GameManager.instance.draging = false;

        if (!dragingNoAttacking)
        {
            //可攻击
            if (canAttack())
                Attack();
        }

        dragingNoAttacking = false;

        gameObject.layer = defaultLayer;

        transform.position = transform.parent.position + Vector3.back;

        rangeDisplay.SetActive(false);
    }
    //鼠标拖动
    private void OnMouseDrag()
    {
        Vector3 pos = GetMousePos() - dragOffset;
        transform.position = pos;

        Vector3 parentPos = transform.parent.position;
        parentPos.z = 0;

        if (Vector3.Distance(pos, parentPos) > 0.3)
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

    void OnMouseEnter()
    {
#if UNITY_IPHONE || UNITY_ANDROID
        return;
#endif

        rangeDisplay.SetActive(true);
    }

    void OnMouseExit()
    {
        if (!GameManager.instance.draging)
            rangeDisplay.SetActive(false);
    }
}
