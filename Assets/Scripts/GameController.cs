using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject player;
	Canvas ui;
    player_state ps;
	float deadTime;
	// Use this for initialization
	void Start () {
        ps = player.GetComponent<player_state>();
		ui = GameObject.FindObjectOfType<Canvas> ();
    
		deadTime = 0;
		ui.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (ps.get_health() <= 0)
        {
			ui.enabled = true;
			Time.timeScale = 0.2f;
			if (deadTime == 0.0f) {
				deadTime = Time.time;
			} else if (Time.time - deadTime >= 1.4f) {
				deadTime = 0.0f;
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
				Time.timeScale = 1f;
			}
        }
	}
}
