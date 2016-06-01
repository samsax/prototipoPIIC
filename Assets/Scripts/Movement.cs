using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

	public GameObject probeta;
	public float playerMovSpeed;// Velocidad de movimiento de la Cardboard.
	public GameObject head; // La cabeza del jugador (Es el objeto llamado Head dentro de la Cardboard) se necesita para saber a donde está mirando la cardboard.
	private bool playerWantsToMove;//un booleano que me permite saber si el usuario ha presionado el boton, cada que lo presiona cambia de estado.
	private Vector3 playerMovDir; // El vector hacia donde está mirando la cardboard.
	private bool enMinijuego; // un booleano que me permite saber si el jugador está o no dentro de un minijuego
	public Collider mesaColl;
	public GameObject rioSucio;
	public GameObject form;
	private bool formArriba;
	public GameObject posicionadorAbajo;
	public GameObject posicionadorArriba;	
	public GameObject unChulo;
	public GameObject dosChulos;
	public GameObject tresChulos;
	private int chulos;

	void Start () {
		//Al principio el jugador no debe estar moviendose, ni cerca del rio y no está en un minijuego.
		//acomodar();
		playerWantsToMove = false;
		enMinijuego = false;
		formArriba = false;
		chulos = 0;
	}

	//Cada frame se pregunta a unity si el usuario ha presionado el magneto de la Cardboard.
	void Update () {
		if (Cardboard.SDK.VRModeEnabled && Cardboard.SDK.Triggered) {
			playerWantsToMove = !playerWantsToMove;
		}
		//En el momento en que esté true, la cardboard se moverá hasta que presionen de nuevo el magneto.
		if (playerWantsToMove == true) {
			movePlayer();
		}
		if (head.transform.forward.y<-0.5f&&!formArriba) {
			subirForm ();
		}else if (head.transform.forward.y>0.0f&&formArriba) {
			bajarForm ();
		}
	}

	public void subirForm(){
		if (chulos==0) {
			form.transform.position = posicionadorArriba.transform.position;
		}else if (chulos==1) {
			unChulo.transform.position = posicionadorArriba.transform.position;
		}
		else if (chulos==2) {
			dosChulos.transform.position = posicionadorArriba.transform.position;
		}
		else if (chulos==3) {
			tresChulos.transform.position = posicionadorArriba.transform.position;
		}
		formArriba = true;
	}

	public void bajarForm(){
		if (chulos==0) {
			form.transform.position = posicionadorAbajo.transform.position;
		}else if (chulos==1) {
			unChulo.transform.position = posicionadorAbajo.transform.position;
		}
		else if (chulos==2) {
			dosChulos.transform.position = posicionadorAbajo.transform.position;
		}
		else if (chulos==3) {
			tresChulos.transform.position = posicionadorAbajo.transform.position;
		}
		formArriba = false;
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
		if (Other.CompareTag("Rio")&&!enMinijuego) {
			Renderer rend = probeta.GetComponent<Renderer>();
			rend.material.SetColor("_Color", Color.gray);
			unChulo.SetActive (true);
			chulos = 1;
			form.SetActive (false);
		}
		if (Other.CompareTag("Rio")&&enMinijuego&&gameObject.GetComponent<Minijuego1>().getTerminado()) {
			Debug.Log ("Hola");
			tresChulos.SetActive (true);
			dosChulos.SetActive (false);
			chulos = 3;
			Destroy (rioSucio.gameObject);
		}
	}

	public int getChulos(){
		return chulos;
	}

	public void setChulos(int c){
		this.chulos = c;
	}

	void acomodar(){	
		playerWantsToMove = !playerWantsToMove;
		gameObject.GetComponent<Minijuego1>().enabled=true;
		mesaColl.enabled = false;
	}

}