using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public ParticleSystem particle_death;

    //改变颜色
    public void ChangeColor(int _color)
    {
        spriteRenderer.material.color = ColorManager.instance.GetColor(_color - 1);
    }

	//被击中
	public void Hit()
	{
		CameraShake.instance.Shake(.2f, .2f);

	}

	//心碎掉
	public void Die()
    {
        spriteRenderer.enabled = false;

        particle_death.Play();

		CameraShake.instance.Shake(.5f, .3f);
    }
}
