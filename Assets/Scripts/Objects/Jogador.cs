using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
/// <summary>
/// Player locomotion.
/// This class handles all thier applied force tat moves the player as well as the rotator that turns the player to face the mouse.
/// </summary>

public class Jogador : MonoBehaviour {

	//Variavel que controlara a animaçao
	private Animador animador;

	private bool justAttacked = false;

	//Variavel que armazena o som que deve ser reproduzido
	public AudioClip sfxAttack;

	public float attackDelay;
	
	//Initializes targetPosition, gets updated in PlayerLocomotion.Rotate().
	private Vector3 targetPosition = Vector3.zero;
	
	//how fast can the player turn
	public float rotationSpeed;
	
	//how much force to apply to move.
	public float moveForce;
	
	//the maximum velocity change
	public float maxVelocityChange = 10.0f;
	
	public float distanceToStop = .5f;
	
	public float distance;
	
	private CharacterController controller;
	
	public ParticleSystem directionCursor;
	
	public Vector3 TargetPosition{
		get{ return targetPosition;}
		set{ targetPosition = value;}
	}
	
	void Awake(){
		animador = new Animador(GetComponent<Animator>());
		targetPosition = this.transform.position;
		controller = GetComponent<CharacterController>();
	}
	
	void Update(){
		Rotate();
		Walk();
	}
	
	void LateUpdate () {
		animador.SetSpeed(controller.velocity.magnitude);
	}
	
	public void ShowTargetCursor(){
		if(!directionCursor.enableEmission)
			directionCursor.enableEmission = true;
	}

	public void Attack(){
		//Se o jogador tiver acabado de atacar, retorne
		if(justAttacked)
			return;

		//Informa que o jogador acabou de atacar
		justAttacked = true;
		//Roda um timer para o jogador poder atacar de novo
		StartCoroutine(AttackDelay(attackDelay));
		//Faz com que o jogador nao tenha mais a intencao de se mover
		targetPosition = this.transform.position;

		animador.TriggerAttack();
	}

	IEnumerator AttackDelay(float delay){
		for(float wait = 0; wait < delay; wait += Time.deltaTime)
			yield return null;

		justAttacked = false;
	}
	
	//This rotates the player to face the mouse position, and also locks the x and z axis.
	public void Rotate()
	{
		if(distance > distanceToStop){
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(targetPosition - this.transform.position), rotationSpeed * Time.deltaTime);
			this.transform.localEulerAngles = new Vector3(0, this.transform.localEulerAngles.y, 0);
		}
	}
	
	//Moves the player local forward.
	public void Walk()
	{
		distance = Vector3.Distance(this.transform.position, targetPosition);
		
		if(distance > distanceToStop)
		{
			// Calculate how fast we should be moving
			Vector3 targetVelocity = (targetPosition - this.transform.position);
			targetVelocity = targetVelocity.normalized;
			
			//targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= moveForce;
			
			controller.Move(targetVelocity*Time.deltaTime);
			if(directionCursor.enableEmission)
				directionCursor.transform.position = targetPosition;
		}
		else
		{
			targetPosition = transform.position;
			controller.Move(Vector3.zero);
			directionCursor.enableEmission = false;
		}
	}
}