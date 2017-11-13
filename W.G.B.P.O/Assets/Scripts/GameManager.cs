using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public GameObject startNode;

    [HideInInspector]
    public bool draging = false;

    [HideInInspector]
    public GameObject dragingNode;

    public GameObject[] turrets;

    GameObject[] nodes;

    public GameObject textCanvasPrefab;

    private void Start()
    {
        //BuildTurret(startNode);

        nodes = GameObject.FindGameObjectsWithTag("Node");

        RandomBuildTurret();

        CreateFloatingText(Vector2.zero, "qwe");

    }
    //在一个节点造炮塔
    public void BuildTurret(GameObject _node)
    {
        Node node = _node.GetComponent<Node>();

        int t = Random.Range(0, turrets.Length);

        node.BuildTurret(turrets[t]);

    }
    //随机在一个闲置节点造炮塔
    public void RandomBuildTurret()
    {
        GameObject[] idleNodes = GetIdleNodes();

        int n = Random.Range(0, idleNodes.Length);

        int t = Random.Range(0, turrets.Length);

        idleNodes[n].GetComponent<Node>().BuildTurret(turrets[t]);
    }

    //获取未建设炮塔的节点组
    GameObject[] GetIdleNodes()
    {
<<<<<<< HEAD
        GameObject[] nodesTemp = new GameObject[nodes.Length];
        //GameObject[] nodesTemp = (GameObject[])nodes.Clone();
=======
        //GameObject[] nodesTemp = (GameObject[])nodes.Clone();

        List<GameObject> nodesTemp = new List<GameObject>(nodes);
>>>>>>> 46ec8759aa8c7ab464b80f157795ad960773a8e0

        foreach (GameObject item in nodesTemp)
        {
            if(item.GetComponent<Node>().turret != null)
            {
<<<<<<< HEAD
                nodesTemp.
=======
                nodesTemp.Remove(item);
>>>>>>> 46ec8759aa8c7ab464b80f157795ad960773a8e0
            }
        }

        return nodesTemp.ToArray();
    }

    //创建浮动文字
    public void CreateFloatingText(Vector2 _pos, string _text)
    {
        GameObject text = Instantiate(textCanvasPrefab, _pos, Quaternion.identity);

        text.GetComponent<FloatingText>().SetText((_text));

    }


}
