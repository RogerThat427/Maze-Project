using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionChase : MonoBehaviour {
    public Transform player;
    public Player pInstance;
    public GameObject rockBundle;

	public int maxHealth = 100;
	public int damageToTake = 10;
	public int timePostDeath = 20;
    public int ptsPerHit = 10;
    public int ptsPerKill = 100;
	public bool isDead;

	public Animator anim;

    Vector3 stopingPoint;
    NavMeshAgent agent;

    private int currentHealth;

    private Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = this.transform.position;
        FloatingTextController.Initialize();
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        stopingPoint = player.position;

        isDead = false;
        currentHealth = maxHealth;
        anim.SetBool("isDead", false);
		anim.SetBool("isRunning", false);
		anim.SetBool("isIdle", true);
		anim.SetBool("isAttacking", false);
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < 15 && isDead == false){
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;

            //this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                       //Quaternion.LookRotation(direction), 0.1f);
            anim.SetBool("isIdle", false);
            if(distance > 4){
                //this.transform.Translate(0, 0, 0.05f);
                agent.destination = player.position;
                anim.SetBool("isRunning", true);
                anim.SetBool("isAttacking", false);
            }
            else{
                anim.SetBool("isAttacking", true);
                anim.SetBool("isRunning", false);
            }
        }
        else if (distance >= 15 && isDead == false){
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
        }
        else{
            StartCoroutine(WaitToGo());
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerRock"){
            Destroy(collision.gameObject);
            currentHealth -= damageToTake;
            if(currentHealth <= 0){
				anim.SetBool("isDead", true);
				anim.SetBool("isRunning", false);
				anim.SetBool("isIdle", false);
				anim.SetBool("isAttacking", false);
                if (isDead == false){
                    FloatingTextController.CreateFloatingText(ptsPerKill.ToString(), transform);
                    pInstance.AddPoints(ptsPerKill);
                }
                DIE();
            }
            else{
                if (isDead == false)
                {
                    FloatingTextController.CreateFloatingText(ptsPerHit.ToString(), transform);
                    pInstance.AddPoints(ptsPerHit);
                }
            }
        }
        Destroy(collision.gameObject);
    }

    IEnumerator WaitToGo(){
		yield return new WaitForSeconds(timePostDeath);
        //Destroy(this.gameObject);
        Respawn();
	}

    private void DIE(){
        if (isDead == false){
			GameObject bundleInstance = (GameObject)Instantiate(rockBundle, this.transform.position, this.transform.rotation);
            isDead = true;
            gameObject.GetComponent<Collider>().isTrigger = true;
			StartCoroutine(WaitToGo());
		}
    }

    private void Respawn(){
		this.transform.position = startingPos;
        gameObject.GetComponent<Collider>().isTrigger = false;
		anim = this.GetComponent<Animator>();
		agent = this.GetComponent<NavMeshAgent>();
		stopingPoint = player.position;

		isDead = false;
		currentHealth = maxHealth;
		anim.SetBool("isDead", false);
		anim.SetBool("isRunning", false);
		anim.SetBool("isIdle", true);
		anim.SetBool("isAttacking", false);
    }
}
