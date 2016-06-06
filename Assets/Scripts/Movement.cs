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
	public Collider Mesa; // El collider que me indica que va a iniciar el minijuego (el jugador está en la mesa).
	public GameObject rioSucio;// El rio café, la idea es eliminarlo cuando se haya logrado el tercer objetivo.
	public GameObject form;//La primera imagen de las instrucciones.
	private bool formArriba;//un booleano que indica si las instrucciones deben o no estar arriba.
	public GameObject posicionadorAbajo;// un cubo que indica la posición donde debe bajar las instrucciones.
	public GameObject posicionadorArriba;	// un cubo que indica la posición donde debe subir las instrucciones.
	public GameObject unChulo;//La imagen de instrucciones con un chulo.
	public GameObject dosChulos;//La imagen de instrucciones con dos chulos.
	public GameObject tresChulos;//La imagen de instrucciones con tres chulos.
	private int chulos;//La cantidad de chulos que debería haber en la imagen (Chulos = instrucciones completadas)
	private Vector3 nulo;//Un vector que va a estar en 0.0.0 para que el objeto no rebote.

	void Start () {
		//Al principio el jugador no esta moviendose, ni cerca del rio, no está en un minijuego, las instrucciones están abajo y no hay chulos en las instrucciones.
		playerWantsToMove = false;
		enMinijuego = false;
		formArriba = false;
		chulos = 0;//(Chulos = instrucciones completadas)
		nulo = new Vector3 (0.0f, 0.0f, 0.0f);
	}


	void Update () {
		//Cada frame se pregunta a unity si el usuario ha presionado el magneto de la Cardboard, y si lo ha hecho,
		//cambio un booleano que indica si se mueve o no.
		if (Cardboard.SDK.VRModeEnabled && Cardboard.SDK.Triggered) {
			playerWantsToMove = !playerWantsToMove;
		}
		//En el momento en que esté true, la cardboard se moverá hasta que presionen de nuevo el magneto.
		if (playerWantsToMove == true) {
			movePlayer();
		}
		if (head.transform.forward.y<-0.5f&&!formArriba) {//Si las instrucciones están abajo y el usuario mira hacia el suelo
			subirForm ();//entonces vaya a una función que sube el formulario.
		}else if (head.transform.forward.y>0.0f&&formArriba) {//Si las instrucciones están arriba y el usuario NO mira hacia el suelo
			bajarForm ();//entonces vaya a una función que baja el formulario.
		}
	}

	public void subirForm(){
		// Sube la imagen que se requiere dependiendo de cuantos chulos haya conseguido el usuario (Chulos = instrucciones completadas)
		// posicionadorArriba siempre está quieto, y sirve para saber la posición a donde debe subir la imagen.
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
		formArriba = true;// el booleano que indica si las instrucciones están arriba se activa.
	}

	public void bajarForm(){
		// Naja la imagen que se requiere dependiendo de cuantos chulos haya conseguido el usuario (Chulos = instrucciones completadas)
		// posicionadorAbajo siempre está quieto, y sirve para saber la posición a donde debe bajar la imagen.
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
		formArriba = false;// el booleano que indica si las instrucciones están arriba se desactiva.
	}

	public void movePlayer(){
		//Se calcula la dirección hacia la que debe moverse la cardboard, que depende de donde esté mirando.
		playerMovDir = new Vector3(-head.transform.forward.x,Mathf.Clamp(head.transform.forward.y,0,0),-head.transform.forward.z);
		//Esta instrucción transalada la cardboard hacia donde le diga el vector. 
		transform.Translate(playerMovDir*playerMovSpeed*Time.deltaTime);
	}
		
	/*En unity, las colisiones funcionan de la siguiente manera
	 * Trigger: Es un tipo de colisión que no tiene en cuenta las físicas, es decir, como cuando se quiere cojer un objeto coleccionable,
	 * Este tipo de colisiones requiere que el otro colisionador con el que ha colisionado este objeto sea de tipo trigger. Si no lo es, esta
	 * función no se llama. 
	 * Collision: Es el tipo normal de colisión física. Unity tiene un motor de física que calcula por sí mismo los efectos de la colisión, como
	 * el movimiento y la rotación al chocar. Es complejo manejar las físicas. Las colisiones dependen de los materiales físicos que tienen
	 * los Collider. 
	 * Cualquiera de los dos tipos de colision tiene Enter, Stay y Exit.
	 * Enter: Lo que se quiere que pase cuando la colisión empiece.
	 * Stay : Lo que se quiere que pase cuando la colisión siga en curso.
	 * Exit: Lo que se quiere que pase cuando la colisión haya justo acabado de terminar.
	*/

	void OnCollisionStay(){
		//mientras el objeto colisione con algo, haga que la velocidad sea nula (la idea de esto es hacer que no rebote, pero al parecer no funciona).
		gameObject.GetComponent<Rigidbody> ().velocity = nulo;
	}

	void OnTriggerEnter(Collider Other){
		//Para la parte de los minijuegos
		if (Other.CompareTag ("Mesa")&&chulos==1) {// Si el usuario está en la mesa y ya recogió agua sucia (chulo==1 es que la instruccion1 ha sido cumplida)
			acomodar ();//Se llama a una funcion que activa el minijuego, y hace que el usuario deje de caminar.
			enMinijuego = true;//El booleano que indica que se está en el minijuego se activa.
		}
		if (Other.CompareTag("Rio")&&!enMinijuego&&!gameObject.GetComponent<Minijuego1>().getTerminado()) {//Si está en el rio y no está en minijuego y no ha terminado el minijuego
			Renderer rend = probeta.GetComponent<Renderer>();//el render que renderiza la probeta
			rend.material.SetColor("_Color", Color.gray);//se cambia el color del material de la probeta por otro .
			unChulo.SetActive (true);//se activa la imagen que muestra las instrucciones con un chulo
			setChulos (1);//se modifica la cantidad de chulos
			form.SetActive (false);// se desactiva la imagen que muestra las instrucciones sin chulos.
		}
		if (Other.CompareTag("Rio")&&gameObject.GetComponent<Minijuego1>().getTerminado()) {// Si está en el rio pero ya terminó el minijuego
			tresChulos.SetActive (true);////se activa la imagen que muestra las instrucciones con tres chulos
			setChulos (3);//se modifica la cantidad de chulos
			dosChulos.SetActive (false);// se desactiva la imagen que muestra las instrucciones con dos chulos.
			Destroy (rioSucio.gameObject);// se destruye el objeto que muestra el rio sucio.
		}
	}

	public int getChulos(){//sirve para saber cuántos chulos hay desde otro script
		return chulos;
	}

	public void setChulos(int c){//sirve para modificar la cantidad de chulos desde otro script
		this.chulos = c;
	}

	void acomodar(){	
		// pone al usuario queto, activa el script del minijuego y desactiva un colisionador que dice si el usuario está en la mesa
		playerWantsToMove =false;
		gameObject.GetComponent<Minijuego1>().enabled=true;
		Mesa.enabled = false;// si no se desactiva, el usuario siempre puede volver a jugar el minijuego.
	}

}