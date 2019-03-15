using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : MonoBehaviour
{
	[SerializeField]
	float stunDuration = 3f;

	[SerializeField]
	SpriteRenderer spriteToAffect;

	[SerializeField]
	Color stunColor;

	[SerializeField]
	Color normalColor = Color.white;

	EnemyHealth healthRef;
	Damager damager;
	DamageBlinkColorEffect colorEffect;

	public delegate void OnStunState();
	public event OnStunState EventOnBeginStun;
	public event OnStunState EventOnEndStun;


	private void Start()
	{
		healthRef = GetComponent<EnemyHealth>();
		healthRef.EventOnDeath += BeginStun;

		damager = GetComponent<Damager>();

		colorEffect = GetComponent<DamageBlinkColorEffect>();
		EventOnBeginStun += colorEffect.StopTakingDamage;
	}

	void BeginStun()
	{
		EventOnBeginStun();
		Debug.Log("Enter Stun State");
		spriteToAffect.color = stunColor;
		damager.DeactivateCollider();
		damager.canDealDamage = false;
		Invoke("EndStun", stunDuration);
	}

	void EndStun()
	{
		EventOnEndStun();
		Debug.Log("Exit Stun State");
		spriteToAffect.color = normalColor;
		damager.canDealDamage = true;
		damager.ActivateCollider();
	}




}
