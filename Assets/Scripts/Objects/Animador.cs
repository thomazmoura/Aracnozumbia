using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Animador {

	Animator animator;

	public Animador(Animator animator){
		this.animator = animator;
	}

	public void SetSpeed(float speed){
		animator.SetFloat("Speed", speed);
	}

	public void TriggerAttack(){
		animator.SetTrigger("Attack");
	}


}
