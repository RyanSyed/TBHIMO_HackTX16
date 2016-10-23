using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float range;

	// Use this for initialization
	void Start () {
        range = 5;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Fire1"))
        {
            int enemy_mask = 256;
            Collider[] enemies = Physics.OverlapSphere(transform.position, range, enemy_mask);
            foreach(Collider c in enemies)
            {
                Vector3 toEnemy = c.transform.position - transform.position;
                toEnemy = Vector3.Normalize(toEnemy);
                if(Vector3.Dot(toEnemy, transform.forward) > 0.5)
                {
                    Object.Destroy(c.gameObject);
                }
            }
        }
	}
}
