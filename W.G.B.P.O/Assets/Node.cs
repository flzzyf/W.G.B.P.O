using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public GameObject turretToBuild;

    GameObject turret;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if(turret == null)
        {
            //未建造炮塔
            turret = Instantiate(turretToBuild, transform.position, Quaternion.identity);

        }
        else
        {
            //已有炮塔
            turret.GetComponent<Turret>().Attack();
        }

    }
}
