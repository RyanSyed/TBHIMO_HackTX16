using UnityEngine;
using System.Collections;

public class player_state : MonoBehaviour
{

    public int recovery_time;
    public float range;
    private int health;
    private float old_time;

    // Use this for initialization
    void Start()
    {
        range = 1;
        health = 1000;
        recovery_time = 2;
    }

    // Update is called once per frame
    void Update()
    {
        int enemy_mask = 1 << 8;
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, enemy_mask);
        foreach (Collider c in enemies)
        {
            if (Mathf.FloorToInt(Time.time * 60) % (recovery_time*60) == 0  )
            {
                health -= 10;
                Debug.Log("Health = " + health);
                
            }
        }
    }
    
    public float get_health()
    {
        return health;
    }
}
