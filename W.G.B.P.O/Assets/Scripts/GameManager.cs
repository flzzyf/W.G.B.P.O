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

    public void BuildTurret(GameObject _node)
    {
        Node node = _node.GetComponent<Node>();

        int t = Random.Range(0, turrets.Length);

        node.BuildTurret(turrets[t]);

    }

    public void RandomBuildTurret()
    {
        int n = Random.Range(0, nodes.Length);

        int t = Random.Range(0, turrets.Length);

        nodes[n].GetComponent<Node>().BuildTurret(turrets[t]);
    }

    GameObject GetIdleNodes()
    {
        GameObject[] nodesTemp = new GameObject[nodes.Length];
        //GameObject[] nodesTemp = (GameObject[])nodes.Clone();

        foreach (GameObject item in nodesTemp)
        {
            if(item.GetComponent<Node>().turret != null)
            {
                nodesTemp.
            }
        }
    }


    public void CreateFloatingText(Vector2 _pos, string _text)
    {
        GameObject text = Instantiate(textCanvasPrefab, _pos, Quaternion.identity);

        text.GetComponent<FloatingText>().SetText((_text));

    }


}
