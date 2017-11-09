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

    public GameObject dragingNode;

    public GameObject[] turrets;

    public GameObject[] nodes;

    private void Start()
    {
        //BuildTurret(startNode);

        nodes = GameObject.FindGameObjectsWithTag("Node");

        RandomBuildTurret();

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

}
