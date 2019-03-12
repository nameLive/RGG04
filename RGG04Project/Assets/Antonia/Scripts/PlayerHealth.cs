using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField]
	int maxHealth = 5;

	int health = 5;

	public delegate void OnPlayerHealthChanged();
	public static event OnPlayerHealthChanged OnHealthDecreased;
	public static event OnPlayerHealthChanged OnDeath;

    // Start is called before the first frame update
    void Start()
    {
		health = maxHealth;
    }

	public void DecreaseHealth(int Amount)
	{
		health -= Mathf.Abs(Amount);
		Debug.Log("Current Health:" + health);
		OnHealthDecreased();

		if (health <= 0)
		{
			//OnDeath();
		}
	}

	public void IncreaseHealth(int Amount)
	{
		health += Mathf.Abs(Amount);
		Debug.Log("Current Health: " + health);
	}


}
