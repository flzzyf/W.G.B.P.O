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

    public GameObject[] turrets;

    private void Start()
    {
        BuildTurret(startNode);
    }

    public void BuildTurret(GameObject _node)
    {
        Node node = _node.GetComponent<Node>();

        int a = Random.Range(0, turrets.Length);

        node.BuildTurret(turrets[a]);

    }

}
