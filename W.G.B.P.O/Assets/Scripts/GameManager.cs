using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    }
    #endregion

    public GameObject startNode;

    public GameObject[] turrets;

    public Transform nodeParent;
    GameObject[] nodes;

    public Sound enemyReachSound;

    public bool gaming = false;

    public static string enemyTag = "Enemy";

    public GameObject colorParticle;

    public GameObject panel_GameWin;
    public GameObject panel_GameLose;

    public Animator wavePointer;

	public GameObject startingNode;

	public Sound sound_CreateTurret;

    private void Start()
    {
        //BuildTurret(startNode);

        nodes = GameObject.FindGameObjectsWithTag("Node");

        //FloatingText.CreateFloatingText(Vector2.zero, "qwe");

        gaming = true;

		//测试
		// WaveSpawner.instance.currentWaveIndex = 4;
		// for (int i = 0; i < 3; i++)
		// {
		//     RandomBuildTurret();
		// }

		StartCoroutine(CreateStartingTurret());
    }

	IEnumerator CreateStartingTurret()
	{
		yield return new WaitForSeconds(1);

		BuildTurret(startingNode, 1);
	}

	//在一个节点造炮塔
	public void BuildTurret(GameObject _node, int turretType = 0)
    {
        Node node = _node.GetComponent<Node>();

        GameObject go = Instantiate(turrets[turretType], node.transform.position + Vector3.back, Quaternion.identity, node.transform);

        node.turret = go;

		//音效
		SoundManager.instance.PlaySound(sound_CreateTurret);
    }
    //随机在一个闲置节点造炮塔
    public void RandomBuildTurret()
    {
        GameObject[] idleNodes = GetIdleNodes();

        int n = Random.Range(0, idleNodes.Length);

        int t = Random.Range(0, turrets.Length);

        BuildTurret(idleNodes[n], t);
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

    //选出本回合MVP炮塔
    public void TurretMVP()
    {
        int max = 0;
        List<GameObject> maxTurret = new List<GameObject>();

        //遍历每个炮塔
        foreach (var item in GameObject.FindGameObjectsWithTag("Turret"))
        {
            Turret turret = item.GetComponent<Turret>();
            if (turret.GetRoundDamage() == max)
            {
                maxTurret.Add(item);

            }
            else if (turret.GetRoundDamage() > max)
            {
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

    void GameOver()
    {
        GameManager.instance.gaming = false;
    }

    public void GameWin()
    {
        GameOver();

		StartCoroutine(GameWinCor());
    }

	IEnumerator GameWinCor()
	{
		yield return new WaitForSeconds(1);

		panel_GameWin.SetActive(true);
	}

	public void GameLose()
    {
        GameOver();

        panel_GameLose.SetActive(true);

        SoundManager.instance.audioSource_BGM.Stop();
    }
}
