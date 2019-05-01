using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoObject : MonoBehaviour
{
	public Sprite icon;
	public string text_Title;
	[TextArea(2, 4)]
	public string text_Info;

    void OnMouseEnter()
	{
		Panel_Info.instance.Show(icon, text_Title, text_Info);

	}

	void OnMouseExit()
	{
		Panel_Info.instance.Hide();
	}
}
