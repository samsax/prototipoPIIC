using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CtrlProbetas : MonoBehaviour {
	
	public Text miTexto;//el texto que le aparece al usuario, con esta variable se modifica.
	public Image miImagen;//la imagen que se rellena.
	public float velocidadPorcentajeLlenado;//el # que se suma al porcentaje actual (mientras más grande, va más rápido).
	public float velocidadLlamadoRellenar;//el tiempo que demora en volver a llamar a la función de rellenar.
	private float porcentajeLlenadoImagen;//el porcentaje final de llenado de la imagen.
	public bool puedeLlenar;// Un booleano que dice si está o no parado en el rio.
	public GameObject flechaGuia; // La flecha azul que apuntará a las probetas.
	private bool terminoLlenado; // un booleano para controlar que no se entre a un minijuego sin haber hecho el llenado de la probeta.

	void Start () {
		porcentajeLlenadoImagen = 0.0f;
		puedeLlenar = false;
		terminoLlenado = false;
	    rellenarImagen();
	}

	public bool getTerminoLlenado(){
		return terminoLlenado;
	}
	public void setTerminoLlenado(bool t){
		this.terminoLlenado = t;
	}

	public float getVelLlam(){
		return velocidadLlamadoRellenar;
	}

	public void rellenarImagen(){
		if (puedeLlenar && miImagen.fillAmount < 1) {
			porcentajeLlenadoImagen += (velocidadPorcentajeLlenado * Time.deltaTime) / 60.0f;
			miImagen.fillAmount = porcentajeLlenadoImagen;
			Invoke ("rellenarImagen", velocidadLlamadoRellenar);
		} 
		if (miImagen.fillAmount>=1) {
			miTexto.text=" Ahora realiza una mezcla en la mesa ";
			flechaGuia.SetActive(true);
			terminoLlenado = true;
		}
	}
}