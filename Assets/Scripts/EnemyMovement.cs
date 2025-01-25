using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour {

	public float speed = 1f;
    
    private Transform target;
	private Transform[] waypoints;
	private int waypointIndex = 0;

	private Health magicFlowerHealth;
	private MagicFlower magicFlower;

	private string sceneName;

	private Transform lastTarget;

	private string pointName;

	int random;

	void Start()

	{

		sceneName = SceneManager.GetActiveScene().name;

		random = UnityEngine.Random.Range(0,2);
		try 
        {
			
			if(sceneName == "TutorialLevel"){
				waypoints = Waypoints.pointsTutorial;
            }

			else if(sceneName == "ForestLevel"){
				waypoints = Waypoints.pointsForest;
            }

            else if (sceneName == "DesertLevel"){
				waypoints = Waypoints.pointsDesert;

            }
			else if (sceneName == "DesertLevel2"){
				waypoints = Waypoints.pointsDesert2;

            }

            else if (sceneName == "MarshLevel"){
				waypoints = Waypoints.pointsMarsh;
            }
			else if (sceneName == "MarshLevel2"){
				waypoints = Waypoints.pointsMarsh;
            }

			else {
				Debug.Log("ERROR: Scene name does not match any of the scenes listed in Enemy Movement to find their waypoints.");
			}

			target = waypoints[0];
            
        }
        catch (NullReferenceException)
        {
            Debug.Log("Could not find empty waypoint object with the Waypoints script. Make sure it has the component Waypoints.");
        }

		try 
        {
            magicFlowerHealth = GameObject.Find("magic_flower").GetComponent<Health>();
			magicFlower = GameObject.Find("magic_flower").GetComponent<MagicFlower>();
        }
        catch (NullReferenceException)
        {
            Debug.Log("MagicPlant or its health component could not be found. Check name of the obj and that it has a health component.");
        }

	}

	void Update()
	{


		if (sceneName == "MarshLevel2"){
			gameObject.transform.Rotate(0.5f, 1, 0.25f);
			// StartCoroutine(Disappear());
		}


		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.1f)
		{
			lastTarget = target;

			if (sceneName == "DesertLevel2" || sceneName == "MarshLevel2"){

				if (random == 0){
					GetNextWaypointNew(true);
				}
				else {
					GetNextWaypointNew(false);

				}			
		}
		else{
			GetNextWaypoint();

		}

		}
	}

	public void Stop() {
		speed = 0;
	}

	IEnumerator Disappear(){
		SpriteRenderer pic = gameObject.GetComponent<SpriteRenderer>();

		int random1 = UnityEngine.Random.Range(0,10);

		for (int i = 0; i < random1; i++){
			if(i < random1){
			pic.enabled = false;
			yield return new WaitForSeconds(0.5f);
			pic.enabled = true;
			yield return new WaitForSeconds(0.7f);
			}
			else{
				yield return new WaitForSeconds(7f);
				i=0;
			}
		}


		yield return new WaitForSeconds(10f);
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}

	void GetNextWaypoint()
	{
		if (waypointIndex >= waypoints.Length - 1){
			EndPath();
			return;
		}
		waypointIndex++;
		target = waypoints[waypointIndex];


		if(target.transform.position.x == lastTarget.transform.position.x){

			if (target.transform.position.y > lastTarget.transform.position.y){
				gameObject.transform.eulerAngles = new Vector3(0f,0f,90f);
			}
			else if (target.transform.position.y < lastTarget.transform.position.y){
				gameObject.transform.eulerAngles = new Vector3(0f,0f,-90f);
			}
		} 

		else if (target.transform.position.y == lastTarget.transform.position.y){

			if (target.transform.position.x > lastTarget.transform.position.x){
				gameObject.transform.eulerAngles = new Vector3(0f,0f,0f);

			}
			else if (target.transform.position.x < lastTarget.transform.position.x){
				gameObject.transform.eulerAngles = new Vector3(0f,180f,0f);
			}
			
		}
	}


	void GetNextWaypointNew(bool tag)
	{

		if (waypointIndex >= waypoints.Length - 1){
					EndPath();
					return;
				}
				
					waypointIndex++;
					target = waypoints[waypointIndex];

		if (tag == false){

			while(target.tag == "Top"){
				if (waypointIndex >= waypoints.Length - 1){
					EndPath();
					return;
				}
				
					waypointIndex++;
					target = waypoints[waypointIndex];
			}
		}
		else{
			while(target.tag != "Top"){

				if (waypointIndex >= waypoints.Length - 1){
					EndPath();
					return;
				}
				
					waypointIndex++;
					target = waypoints[waypointIndex];
			}
		}


		if(sceneName != "MarshLevel2"){
			if(target.transform.position.x == lastTarget.transform.position.x){

			if (target.transform.position.y > lastTarget.transform.position.y){
				gameObject.transform.eulerAngles = new Vector3(0f,0f,90f);
			}
			else if (target.transform.position.y < lastTarget.transform.position.y){
				gameObject.transform.eulerAngles = new Vector3(0f,0f,-90f);
			}
		} 

		else if (target.transform.position.y == lastTarget.transform.position.y){

			if (target.transform.position.x > lastTarget.transform.position.x){
				gameObject.transform.eulerAngles = new Vector3(0f,0f,0f);

			}
			else if (target.transform.position.x < lastTarget.transform.position.x){
				gameObject.transform.eulerAngles = new Vector3(0f,180f,0f);
			}
			
		}

		}
	}

    void EndPath()
	{
		if(magicFlowerHealth.health > 1) {
			Debug.Log("magic flower took one damage.");
			magicFlowerHealth.TakeDamage(1);
		}
		else if(!magicFlower.hasAttacked) {
			magicFlower.nearDeathAttack();
		}
		else {
			magicFlowerHealth.TakeDamage(1);
			Debug.Log("end game");
			GameObject.Find("Canvas"+sceneName).GetComponent<EndLevel>().lose();
		}
		Destroy(gameObject);
	}

}