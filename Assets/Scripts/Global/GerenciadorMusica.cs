using UnityEngine;
using System.Collections;

public class GerenciadorMusica : MonoBehaviour {

	//Campo privado para armazenar a instancia atual do Gerenciador de Musica
	private static AudioSource instancia = null;
	/// <summary>
	/// Obtem a instancia atual do Gerenciador de Musica, ou instancia uma nova caso
	///  nenhuma seja encontrada.
	/// </summary>
	/// <value>A instancia.</value>
	public static AudioSource Instancia{
		get {
			if(instancia == null){
				instancia = Instantiate(new AudioSource()) as AudioSource;
			}
			return instancia;
		}
	}

	/// <summary>
	/// Instancia a o Gerenciador de Musica, ou lança um erro caso ja se tenha algum
	///  Gerenciador instanciado
	/// </summary>
	public void Start(){
		if(instancia != null){
			Debug.LogWarning("Ja existe uma instancia de Gerenciador de Musica");
			return;
		}
		GameObject audioSource = Instantiate(new GameObject()) as GameObject;
		audioSource.AddComponent("AudioSource");
		instancia = audioSource.GetComponent<AudioSource>();
		Debug.Log ("Boo!");
	}
}
