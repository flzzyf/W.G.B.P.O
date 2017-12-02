using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public Wave[] wave;

	public float timeBetweenWaves = 3f;

	int currentWaveIndex = 0;

	float spawnCountDown = 0;

	bool spawningWave = false;

	void Update ()
	{
        if (!GameManager.instance.gaming)
        {
            return;
        }

		//仍在波次中
		if (spawningWave == true)
			return;

		//完成全部波数
		if(currentWaveIndex == wave.Length)
		{
            Debug.Log("胜利");
			//胜利
			this.enabled = false;
		}

		if(spawnCountDown <= 0)
		{
			spawnCountDown = timeBetweenWaves;
			StartCoroutine(SpawnWave());

		}
		else
		{
			spawnCountDown -= Time.deltaTime;
		}
	}
    //回合开始
	IEnumerator SpawnWave()
	{

		spawningWave = true;

        //Debug.Log(currentWaveIndex);

		Wave currentWave = wave[currentWaveIndex];

		for (int i = 0; i < currentWave.waveUnits.Length; i++)
		{

			for (int j = 0; j < currentWave.waveUnits[i].num; j++) 
			{
				GameObject unit = Instantiate(currentWave.waveUnits[i].waveUnit, WayPointManager.wayPoints[0].position, Quaternion.identity);
                unit.GetComponent<Unit>().maxHp = currentWave.waveUnits[i].hp;

				yield return new WaitForSeconds(currentWave.waveUnits[i].rate);

			}
		}

        TurnEnd();
	}

    //回合结束
    void TurnEnd()
    {
        currentWaveIndex++;

        spawningWave = false;

        //随机送个塔
        GameManager.instance.RandomBuildTurret();

    }

}
