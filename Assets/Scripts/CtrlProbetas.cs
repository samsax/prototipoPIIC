using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CtrlProbetas : MonoBehaviour {
	
	public Text miTexto;//el texto que le aparece al usuario, con esta variable se modifica.
	public Image miImagen;//la imagen que se rellena.

	public float velocidadPorcentajeLlenado;//el # que se suma al porcentaje actual (mientras más grande, va más rápido).
	public float velocidadLlamadoRellenar;//el tiempo que demora en volver a llamar a la función de rellenar.
	private float porcentajeLlenadoImagen;//el porcentaje final de llenado de la imagen.
	private bool puedeLlenar;// Un booleano que dice si está o no parado en el rio.

	void Start () {
		porcentajeLlenadoImagen = 0.0f;
		puedeLlenar = false;
		if (puedeLlenar) {
			rellenarImagen();
		}
	}

	void rellenarImagen(){
		if (puedeLlenar) {
			porcentajeLlenadoImagen += velocidadPorcentajeLlenado*Time.deltaTime;
			miImagen.fillAmount = porcentajeLlenadoImagen;
			Invoke("rellenarImagen",velocidadLlamadoRellenar);
		}
	}

	/*void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Rio")) {
			Debug.Log ("Empieza el llenado");
			puedeLlenar = true;
		} else
			Debug.Log ("Nada");
	}*/

	void OnTriggerEnter(Collider other){
		Destroy (other.gameObject);
	}

}