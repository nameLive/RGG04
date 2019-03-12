using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamageEvents : MonoBehaviour
{
	bool tookDamage = false;
	float timer = 0f;
	float colorTimer = 0f;
	bool myBool = false;
	int counter = 0;

	SpriteRenderer spriteRenderer;


	// Start is called before the first frame update
	void Start()
	{
		PlayerHealth.OnHealthDecreased += TookDamage;
		spriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
	}

	void TookDamage()
	{
		tookDamage = true;
		myBool = true;
		Debug.Log("TookDamage event fired");
	}

	void CharacterSpriteAnimationThing()
	{
		if (myBool)
		{
			SetColorRed();
			colorTimer += Time.deltaTime * 10f;
			if (colorTimer >= 1f)
			{
				myBool = false;
				colorTimer = 0f;
				counter++;

			}

		}
		else
		{
			SetColorNone();
			colorTimer += Time.deltaTime * 10f;
			if (colorTimer >= 1f)
			{
				myBool = true;
				colorTimer = 0f;
				counter++;
			}
		}
	}

	private void Update()
	{
		if (tookDamage)
		{
			timer += Time.deltaTime;
			if (timer >= 1f)
			{
				tookDamage = false;
				//SetColorNone();
				timer = 0f;
				counter = 0;
			}
			else
			{
				CharacterSpriteAnimationThing();
				//Debug.Log("Character switch colors");
			}
		}
		else
		{
			SetColorNone();
		}
	}

	void SetColorRed()
	{
		spriteRenderer.color = Color.red;
	}

	void SetColorNone()
	{
		spriteRenderer.color = Color.white;
	}

}
