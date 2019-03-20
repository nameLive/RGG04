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
    Animator anim;
    bool isStunned;

	public delegate void OnStunState();
	public event OnStunState EventOnBeginStun;
	public event OnStunState EventOnEndStun;


	private void Start()
	{
		healthRef = GetComponent<EnemyHealth>();
		healthRef.EventOnDeath += BeginStun;
        anim = GetComponent<Animator>();

		damager = GetComponent<Damager>();

		colorEffect = GetComponent<DamageBlinkColorEffect>();
		EventOnBeginStun += colorEffect.StopTakingDamage;
	}

    void Update()
    {
        anim.SetBool("IsStunned", isStunned);
    }

	void BeginStun()
	{
		EventOnBeginStun();
		Debug.Log("Enter Stun State");
		spriteToAffect.color = stunColor;
        isStunned = true;

		damager.canDealDamage = false;
		Invoke("EndStun", stunDuration);
	}

	void EndStun()
	{
		EventOnEndStun();
		Debug.Log("Exit Stun State");
		spriteToAffect.color = normalColor;
		damager.canDealDamage = true;
        isStunned = false;
	}




}
