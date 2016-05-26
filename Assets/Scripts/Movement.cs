using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

	public float playerMovSpeed;// Velocidad de movimiento de la Cardboard.
	public GameObject probetas;// La probeta del Canvas, se necesita aquí para poder llenarla.
	public GameObject head; // La cabeza del jugador (Es el objeto llamado Head dentro de la Cardboard) se necesita para saber a donde está mirando la cardboard.
	private bool playerWantsToMove;//un booleano que me permite saber si el usuario ha presionado el boton, cada que lo presiona cambia de estado.
	private Vector3 playerMovDir; // El vector hacia donde está mirando la cardboard.
	private bool llenar; // Un booleano para controlar si la probeta del canvas puede o no ser llenada (si está o no cerca del rio).
	public GameObject acomodador; // un objeto que da el vector que necesitamos para acomodar la carboard frente a las probetas.
	private bool enMinijuego; // un booleano que me permite saber si el jugador está o no dentro de un minijuego


	void Start () {
		//Al principio el jugador no debe estar moviendose, ni cerca del rio y no está en un minijuego.
		//acomodar();
		playerWantsToMove = false;
		enMinijuego = false;
		llenar = false;
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

	//Para saber si la cardboard ha colisionado con un objeto cuyo colisionador sea de tipo Trigger (para la parte del rio)
	void OnTriggerStay(Collider Other){		
		//Si está colisionando con el rio y no tiene la probeta llena, entonces llene la probeta (el llenado se hace desde el otro Script).
		if (Other.CompareTag ("Rio") && probetas.GetComponent<CtrlProbetas> ().miImagen.fillAmount < 1) {
			llenar = true;
			probetas.GetComponent<CtrlProbetas> ().puedeLlenar = llenar;
			if (llenar) {
				probetas.GetComponent<CtrlProbetas> ().rellenarImagen ();// Llamado a la función de llenado del otro Script.
			}
			//Aquí se pone todo en falso porque la función de llenado del otro script es recursiva (se llama a sí misma).
			llenar = false;
			probetas.GetComponent<CtrlProbetas> ().puedeLlenar = llenar;
		} 
		//Si no está llenando la probeta con agua sucia, entonces que la imagen de probeta deje de llenarse.
		else {
			llenar = false;
			probetas.GetComponent<CtrlProbetas> ().puedeLlenar = llenar;
		}
	}

	//Para la parte de los minijuegos
	void OnTriggerEnter(Collider Other){
		if (Other.CompareTag ("Probetas")&& probetas.GetComponent<CtrlProbetas>().getTerminoLlenado()==true) {
			acomodar ();
			enMinijuego = true;
			probetas.GetComponent<CtrlProbetas>().setTerminoLlenado(true);
		}
	}

	void acomodar(){
		transform.position = acomodador.transform.position;
		transform.rotation = acomodador.transform.rotation;		
		playerWantsToMove = !playerWantsToMove;
		gameObject.GetComponent<Minijuego1>().enabled=true;
	}

}