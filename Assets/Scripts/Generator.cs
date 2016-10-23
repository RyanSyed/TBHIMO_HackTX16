using UnityEngine;
using System.Collections;



public class Generator : MonoBehaviour {
	int constantlen = 10;
	const int maxEnemy = 10;
	float rand;
	float p;
	int count;
	private static int numEnemy;
	public int recovery_time;
	public GameObject Enemy;
	public GameObject gamePlayer;

	// Use this for initialization
	void Start () {
		p = 1.0f;
		recovery_time = 3;
		numEnemy = 0;
	}

	// Update is called once per frame
	void Update () {
		rand = Random.value;
		if ((rand < p) && (Mathf.FloorToInt(Time.time * 60) % (recovery_time * 60) == 0))
		{
			count = Mathf.FloorToInt (Mathf.Log (Time.time));
			for (int i = 0; i < count; i++)
			{
				if (numEnemy < maxEnemy) {
					numEnemy = numEnemy + 1;
					float x = Random.insideUnitCircle.x;
					float z = Random.insideUnitCircle.y;
					GameObject badguy = (GameObject)Instantiate (Enemy, new Vector3 (gamePlayer.transform.position.x + constantlen * x, gamePlayer.transform.position.y + 5 * Random.value, gamePlayer.transform.position.z + constantlen * z), Quaternion.identity);
					badguy.GetComponent<Enemy1_Controller> ().player = gamePlayer.transform;
					float speedster = 10 * Random.value * Mathf.Exp (Random.value * Mathf.Sqrt (Time.time));
					badguy.GetComponent<Enemy1_Controller> ().speed_scalar = speedster % 5;
					Debug.Log ("Num Enemies = " + numEnemy);
				} else {
					return;
				}
			}
		}
	}

	public static void decrementNumEnemy() {
		numEnemy--;
	}
}