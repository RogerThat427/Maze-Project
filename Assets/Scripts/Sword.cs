using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
    public MinionChase minionScript;
    Collider col;


	// Use this for initialization
	void Start () {
        col = gameObject.GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
        if(minionScript.isDead == true){
            if (col.isTrigger == true){
                col.isTrigger = false;
            }
        }
        else{
            if (col.isTrigger == false){
                col.isTrigger = true;
            }
        }
	}
}
