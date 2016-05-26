using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minijuego1 : MonoBehaviour {

	public GameObject probetaRoja;
	public GameObject probetaVerde;
	public GameObject probetaAzul;
	public GameObject probetaAnterior;
	public GameObject probetaAnterior2;
	public string []colores;
	public Text texto;

	void Start () {
		probetaAnterior.SetActive (false);
		probetaAnterior2.SetActive (false);
		string colorAzar = colores[generarColorAleatorio()];
		texto.text = "Combina colores "+ colorAzar;
		colorAzar = colores[generarColorAleatorio()];
		texto.text += " " + colorAzar;
		colorAzar = colores[generarColorAleatorio()];
		texto.text += " " + colorAzar;
		probetaRoja.SetActive (true);
		probetaVerde.SetActive (true);
		probetaAzul.SetActive (true);
	}

	void Update () {
		
	}

	int generarColorAleatorio(){
		return Random.Range (0,colores.Length);
	}
}