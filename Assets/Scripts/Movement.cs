using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

	public GameObject probeta;
	public float playerMovSpeed;// Velocidad de movimiento de la Cardboard.
	public GameObject head; // La cabeza del jugador (Es el objeto llamado Head dentro de la Cardboard) se necesita para saber a donde está mirando la cardboard.
	private bool playerWantsToMove;//un booleano que me permite saber si el usuario ha presionado el boton, cada que lo presiona cambia de estado.
	private Vector3 playerMovDir; // El vector hacia donde está mirando la cardboard.
	public GameObject acomodador; // un objeto que da el vector que necesitamos para acomodar la carboard frente a las probetas.
	private bool enMinijuego; // un booleano que me permite saber si el jugador está o no dentro de un minijuego


	void Start () {
		//Al principio el jugador no debe estar moviendose, ni cerca del rio y no está en un minijuego.
		//acomodar();
		playerWantsToMove = false;
		enMinijuego = false;
	}

	//Cada frame se pregunta a unity si el usuario ha presionado el magneto de la Cardboard.
	void Update () {
		if (Cardboard.SDK.VRModeEnabled && Cardboard.SDK.Triggered && !enMinijuego) {
			playerWantsToMove = !playerWantsToMove;
		}
		//En el momento en que esté true, la cardboard se moverá hasta que presionen de nuevo el magneto.
		if (playerWantsToMove == true) {
			movePlayer();
		}
	}

	public void movePlayer(){
		//Se calcula la dirección hacia la que debe moverse la cardboard, que depende de donde esté mirando.
		playerMovDir = new Vector3(-head.transform.forward.x,Mathf.Clamp(head.transform.forward.y,0,0),-head.transform.forward.z);
		//Esta instrucción transalada la cardboard hacia donde le diga el vector. 
		transform.Translate(playerMovDir*playerMovSpeed*Time.deltaTime);
	}


	void OnTriggerEnter(Collider Other){
		//Para la parte de los minijuegos
		if (Other.CompareTag ("Probetas")) {
			acomodar ();
			enMinijuego = true;
		}
		if (Other.CompareTag("Rio")) {
			Renderer rend = probeta.GetComponent<Renderer>();
			rend.material.SetColor("_Color", Color.gray);
		}
	}

	void acomodar(){
		transform.position = acomodador.transform.position;
		transform.rotation = acomodador.transform.rotation;		
		playerWantsToMove = !playerWantsToMove;
		gameObject.GetComponent<Minijuego1>().enabled=true;
	}

}