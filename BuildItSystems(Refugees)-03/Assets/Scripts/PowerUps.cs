using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
	public bool speedUp;
	public float powerTime = 0;
    public float limit = 5;
	public GameObject effect;
	public float multi = 8f;

    
    void OnTriggerEnter2D(Collider2D other)
	{
        
        if (other.name == "Player Object")
		{
			Pick(other);
		}
	}

    void Pick(Collider2D player)
	{
		Player p = player.GetComponent<Player>();
		Instantiate(effect, transform.position, transform.rotation);

        p.runSpeed += multi;
	
        Destroy(gameObject);
	}
}
