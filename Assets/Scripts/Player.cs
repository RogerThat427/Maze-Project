using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public GameObject rockPrefab;
    public Transform slingshotShooter;
    public Transform leftPrint, rightPrint;

    public GameObject DeadScreen;
    public GameObject EscapeScreen;

    public Texture blood01, blood02, blood03, blood04;

    public Text currentScore;
    public Text rocksLeft;
    //public Transform damageCanvas;

    public float maxThrowSpeed = 25.0f;
    public int maxHealth = 100;
    public int healthToTake = 10;
    public bool fireReady = false;
    public int amoCount = 100;
    public int amoPickup = 12;
    public float regenTime = 0.5f;

    private int health;
    private float downTime, upTime, pressTime;
	private int score;
    public int minionsKilledCount;
    public int jelewsCollectedCount;

    public Text dFinalScore;
    public Text dMinionsKilled;
    public Text dJelewsCollected;

    public Text eFinalScore;
    public Text eMinionsKilled;
    public Text eJelewsCollected;

    private bool isRegenerating;

    private float totalTime;
    public float timeBetweenFootprints = 1;

    private bool isLeft;

    // Use this for initialization
	void Start () {
        isLeft = true;
        Time.timeScale = 1;
        score = 0;
        health = maxHealth;
        fireReady = false;
        downTime = 0;
        upTime = 0;
        pressTime = 0;
        AddPoints(0);
        isRegenerating = false;
        totalTime = 0;

        minionsKilledCount = 0;
        jelewsCollectedCount = 0;
        Cursor.visible = false;

		rocksLeft.text = "Rocks Remaining: " + (amoCount).ToString();
	}

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
        if (Input.GetButtonDown("Fire1")){
            downTime = Time.time;
        }
        if(Input.GetButtonUp("Fire1")){
            upTime = Time.time;
            pressTime = upTime - downTime;
            if(pressTime > 1){
                pressTime = 1;
            }
            if (amoCount > 0)
            {
                ThrowRock(pressTime);
            }
        }
        if ((health < maxHealth) && (isRegenerating == false)){
            StartCoroutine(healthRegen());
        }

		totalTime += Time.deltaTime;
		if (totalTime > timeBetweenFootprints)
		{
            if (isLeft == true)
            {
                //Instantiate(footprints, GetComponent<Transform>().position, footprints.rotation);
                leftPrint.transform.rotation = GetComponent<Transform>().rotation;
                leftPrint.transform.rotation = leftPrint.transform.rotation * Quaternion.Euler(90, 0, 0);
                Instantiate(leftPrint, new Vector3(GetComponent<Transform>().position.x, 0.16f, GetComponent<Transform>().position.z), leftPrint.rotation);
                totalTime = 0;
                isLeft = false;
            }
            else{
				rightPrint.transform.rotation = GetComponent<Transform>().rotation;
				rightPrint.transform.rotation = rightPrint.transform.rotation * Quaternion.Euler(90, 0, 0);
                Instantiate(leftPrint, new Vector3(GetComponent<Transform>().position.x, 0.16f, GetComponent<Transform>().position.z), rightPrint.rotation);
				totalTime = 0;
				isLeft = true;
            }
		}
	}

    void ThrowRock(float power){
        GameObject rockInstance = (GameObject)Instantiate(rockPrefab, slingshotShooter.transform.position, slingshotShooter.transform.rotation);
        rockInstance.GetComponent<Rigidbody>().velocity = rockInstance.transform.forward * (maxThrowSpeed * power);
        amoCount--;
        ShowAmoCount();
    }

    public void DoDamage(){
        Debug.Log("I'VE BEEN HIT");
        health -= healthToTake;
        if(health <= 0){
            Death();
        }
    }

    public void CollectRocks(){
        amoCount = amoCount + amoPickup;
        ShowAmoCount();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MinionSword")
        {
            DoDamage();
        }
        else if(other.gameObject.tag == "RockBundle"){
            Destroy(other.gameObject);
            CollectRocks();
        }
        else if(other.gameObject.tag == "Freedom"){
            Escape();
        }
    }

    public void AddPoints(int points){
        score += points;
        currentScore.text = "Score: " + (score).ToString();
    }

    public void ShowAmoCount(){
        rocksLeft.text = "Rocks Remaining: " + (amoCount).ToString();
    }

    IEnumerator healthRegen(){
        isRegenerating = true;
        while (health < maxHealth)
        {
            yield return new WaitForSeconds(regenTime);
            health++;
        }
        isRegenerating = false;
	}

    private void OnGUI()
    {
        if(health < 100 && health > 75){
            GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), blood01);
		}
        else if(health <= 75 && health > 50){
            GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), blood02);
        }
        else if(health <= 50 && health > 25){
            GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), blood03);
        }
        else if(health <= 25){
			GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), blood04);
		}
    }

    private void Death(){
        Time.timeScale = 0;
        dFinalScore.text = "Final Score: " + score;
        dMinionsKilled.text = "Minions Killed: " + minionsKilledCount;
        dJelewsCollected.text = "Jewels Collected: " + jelewsCollectedCount;
        DeadScreen.gameObject.SetActive(true);
        Cursor.visible = true;
    }

    private void Escape(){
		Time.timeScale = 0;
		eFinalScore.text = "Final Score: " + score;
		eMinionsKilled.text = "Minions Killed: " + minionsKilledCount;
		eJelewsCollected.text = "Jewels Collected: " + jelewsCollectedCount;
		EscapeScreen.gameObject.SetActive(true);
		Cursor.visible = true;
    }
}
