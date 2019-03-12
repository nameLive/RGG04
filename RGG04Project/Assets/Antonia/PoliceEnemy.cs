using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEnemy : MonoBehaviour
{
	[SerializeField]
	GameObject targetLocationVisualizer;

	[SerializeField]
	GameObject player;

	[SerializeField]
	float distanceToPlayerThreshHold = 5f;

	float distanceToPlayer = 0f;
	float alpha = 1f;
	bool reachedTargetLocation = true;

	Vector3 directionToPlayer = new Vector3();
	Vector3 targetLocation = new Vector3();
	Vector3 startLocation = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
		//CalculateTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
		directionToPlayer = player.transform.position - transform.position;

		distanceToPlayer = directionToPlayer.magnitude;

		//Debug.Log("Distance to player:" + distanceToPlayer);

			if (alpha >= 1f)
			{
				CalculateTargetPosition();
				reachedTargetLocation = false;
			}

			MoveTowardsPlayer();
			Debug.Log("Alpha: " + alpha);
	

    }

	void CalculateTargetPosition()
	{
		alpha = 0f;
		startLocation = transform.position;
		targetLocation = directionToPlayer / directionToPlayer.magnitude;
		//targetLocation = targetLocation / 5f;
		//targetLocation += startLocation;
		targetLocationVisualizer.transform.position = targetLocation;
	}


	void MoveTowardsPlayer()
	{
		if (transform.position == targetLocation)
		{
			reachedTargetLocation = true;
		}

		Debug.DrawLine(transform.position, targetLocation, Color.red);


		Vector3 NewPosition = Vector3.Lerp(transform.position, targetLocation, alpha);

		alpha += Time.deltaTime/5f;

		transform.position = NewPosition;

	}
}
