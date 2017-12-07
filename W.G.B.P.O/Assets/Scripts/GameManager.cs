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
    public GameObject dragingTurret;

    public GameObject[] turrets;

    GameObject[] nodes;

    public SoundEffect enemyReachSound;

    public bool gaming = false;

    public static string enemyTag = "Enemy";

    private void Start()
    {
        //BuildTurret(startNode);

        nodes = GameObject.FindGameObjectsWithTag("Node");

        RandomBuildTurret();

        //FloatingText.CreateFloatingText(Vector2.zero, "qwe");

        gaming = true;

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
        
        //t = 1;

        idleNodes[n].GetComponent<Node>().BuildTurret(turrets[t]);
    }

    //获取未建设炮塔的节点组
    GameObject[] GetIdleNodes()
    {
        List<GameObject> nodesTemp = new List<GameObject>(nodes);

        GameObject item;

        for (int i = 0; i < nodesTemp.Count; i++)
        {
            item = nodesTemp[i];
            if (item.GetComponent<Node>().turret != null)
            {
                nodesTemp.Remove(item);

            }

        }

        return nodesTemp.ToArray();
}

    public void EnemyReachTarget()
    {
        SoundManager.instance.PlaySound(enemyReachSound);

    }
    //选出本回合MVP炮塔
    public void TurretMVP(){
        int max = 0;
        List<GameObject> maxTurret = new List<GameObject>();

        //遍历每个炮塔
        foreach (var item in GameObject.FindGameObjectsWithTag("Turret"))
        {
            Turret turret = item.GetComponent<Turret>();
            if(turret.GetRoundDamage() == max){
                maxTurret.Add(item);

            }else if (turret.GetRoundDamage() > max){
                max = turret.GetRoundDamage();

                maxTurret.Clear();
                maxTurret.Add(item);
            }

            turret.ClearRoundDamage();
        }

        foreach (var item in maxTurret)
        {
            //CreateFloatingText(item.transform.position, "MVP");
        }

    }

}
