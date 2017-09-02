using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour {
    public float timeAlive = 20.0f;
    private float timer;

	// Use this for initialization
	void Start () {
        Debug.Log("HELLO");
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += 1.0F * Time.deltaTime;
        if (timer >= timeAlive){
            GameObject.Destroy(gameObject);
        }
        //Debug.Log(gameObject.transform.position);
        if (gameObject.transform.position.y <= .05f){
            Debug.Log("Floor");
            gameObject.tag = "Dead Rock";
        }
	}
}
