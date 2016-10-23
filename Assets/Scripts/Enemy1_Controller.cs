using UnityEngine;
using System.Collections;
using Leap.Unity;
using System.Collections.Generic;

public class Enemy1_Controller : MonoBehaviour {

    private Rigidbody enemy;
    public Transform player;
    public float speed_scalar;
	public LeapHandController myLeapHandControllerInstance;

	public float attackDistance = 1.0f;
	public float attackVelocity = 1.0f;

	private LeapProvider leapDataProvider;
	private float isAttacking;
	private bool destroyed;

	// Use this for initialization
	void Start () {
        enemy = GetComponent<Rigidbody>();
        //speed_scalar = 3;
		leapDataProvider = FindObjectOfType<LeapProvider>();
		isAttacking = 0;
		destroyed = false;
    }

    // Update is called once per frame
    void Update () {
		if (Time.time - isAttacking > 2) {
			Vector3 player_vec = player.position;
			Vector3 enemy_vec = enemy.position;
			Vector3 norm_velocity = Vector3.Normalize (player_vec - enemy_vec);
			enemy.velocity = norm_velocity * speed_scalar;
		} 
	}

	void OnCollisionEnter(Collision collision) {
//		Leap.Controller controller = new Leap.Controller ();
//		Leap.Frame frame = controller.Frame();
		Leap.Frame frame = leapDataProvider.CurrentFrame;
		if (frame.Hands != null && frame.Hands.Count > 0) {
			//Debug.Log ("num hands" + frame.Hands.Count);
			List<Leap.Hand> hands = frame.Hands;
			for (int i = 0; i < hands.Count; i++) {
				Leap.Hand hand = hands [i];

				Vector3 relPos = transform.position - hand.PalmPosition.ToVector3();
				//Debug.Log ("Distance from hand " + i + "is " + relPos.sqrMagnitude);
				if ((relPos.sqrMagnitude < attackDistance) && (gameObject != null)) {
					//Debug.Log ("detected");
					//isAttacking = Time.time;
					Vector3 palm_vel = hand.PalmVelocity.ToVector3 ();
					//Debug.Log ("velocity is " + palm_vel);
					if (palm_vel.sqrMagnitude > attackVelocity) {
						enemy.velocity = (palm_vel*5.0f);
						isAttacking = Time.time;
						if (!destroyed) {
							Object.Destroy (gameObject, 2.5f);
							Generator.decrementNumEnemy ();
						}
						destroyed = true;
					}
				}
			}

		}
		if (collision.gameObject.tag.CompareTo("Player") == 0){
			Debug.Log ("Player Hit ME");
			//Object.Destroy(gameObject,.5f);
		}
	}
}

