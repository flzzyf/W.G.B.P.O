using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public static GameObject textCanvasPrefab;

    //创建浮动文字
    public static void CreateFloatingText(Vector2 _pos, string _text)
    {
        GameObject floatingText = Instantiate(textCanvasPrefab, _pos, Quaternion.identity);

        Text text = floatingText.GetComponentInChildren<Text>();

        text.text = _text;

    }

}
