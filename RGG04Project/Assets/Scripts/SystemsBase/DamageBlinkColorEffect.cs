using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlinkColorEffect : MonoBehaviour
{
	HealthBase healthReference;

	[SerializeField]
	SpriteRenderer spriteToAffect;

	[SerializeField]
	Color normalColor = Color.white;

	[SerializeField]
	Color damageColor = Color.red;

	//How fast it blinks
	[SerializeField]
	float blinkSpeed = 0.2f;

	//For how long it blinks
	[SerializeField]
	float blinkTime = 1f;

	int blinks = 0;
	int currentBlink = 0;

	private void Start()
	{
		healthReference = gameObject.GetComponent<HealthBase>();
		healthReference.EventOnHealthDecreased += BeginTakeDamage;
		blinks = Mathf.RoundToInt(blinkTime / blinkSpeed);
	}

	void BeginTakeDamage()
	{
		currentBlink = 0;
		CancelInvoke();
		BlinkDamage();
	}

	void BlinkNormal()
	{
		spriteToAffect.color = normalColor;
		Invoke("BlinkDamage", blinkSpeed);
		currentBlink++;
	}

	void BlinkDamage()
	{
		spriteToAffect.color = damageColor;
		currentBlink++;
		if (currentBlink < blinks)
		{
			Invoke("BlinkNormal", blinkSpeed);
		}
		else
		{
			Invoke("StopTakingDamage", blinkSpeed);
		}
	}

	void StopTakingDamage()
	{
		spriteToAffect.color = normalColor;
		CancelInvoke();
	}

}
