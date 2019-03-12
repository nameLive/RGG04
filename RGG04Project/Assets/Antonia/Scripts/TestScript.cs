using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	PlayerHealth playerHealthReference;


	private void Start()
	{
		//playerHealthReference = gameObject.GetComponent<PlayerHealth>();
		PlayerHealth.OnHealthDecreased += MyTestEvent;

	}



	// Update is called once per frame
	void Update()
	{
	
	}

	void MyTestEvent()
	{
		Debug.Log("Delegate worked, triggered proper event");
	}
}
