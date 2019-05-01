using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Info : Singleton<Panel_Info>
{
	Animator animator;

	public Image icon;
	public Text text_Title;
	public Text text_Info;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void Show(Sprite sprite, string title, string info)
	{
		animator.SetBool("Show", true);

		icon.sprite = sprite;
		text_Title.text = title;
		text_Info.text = info;
	}

	public void Hide()
	{
		animator.SetBool("Show", false);

	}
}
