using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingMovement : MonoBehaviour
{
    //Bob stuff
    [SerializeField]
    protected bool shouldBob = true;

    [SerializeField]
    float bobMagnitude = 0.5f;
    [SerializeField]
    float bobSpeed = 1f;
    float bobAlpha = 0f;
    bool bobUp = true;
    Vector2 startBobLocation = new Vector2(0f, 0f);
    Vector2 endBobLocation = new Vector2(0f, 0f);
    //no more bob stuff


    // Start is called before the first frame update
    protected void Start()
    {
        endBobLocation.y = -bobMagnitude;
		if (shouldBob)
		{
			shouldBob = false;
			float rand = Random.Range(0f, 2f);
			Invoke("SetShouldBob", rand);
		}
    }

    // Update is called once per frame
    protected void Update()
    {
        if (shouldBob)
        {
            BobbingMovementFunction();
        }
    }

	private void SetShouldBob()
	{
		shouldBob = true;
	}

    private void BobbingMovementFunction()
    {


        bobAlpha += Time.deltaTime * bobSpeed;


        if (bobAlpha >= 1f)
        {
            bobUp = !bobUp;
            bobAlpha = 0f;
            startBobLocation = transform.localPosition;
            endBobLocation.y = bobUp ? -bobMagnitude : 0f;
        }

        float targetY = Mathf.SmoothStep(startBobLocation.y, endBobLocation.y, bobAlpha);

        gameObject.transform.localPosition = new Vector2(0f, targetY);
    }
}
