using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minijuego1 : MonoBehaviour {

	public GameObject probetaRoja;
	public GameObject probetaVerde;
	public GameObject probetaAzul;
	public GameObject probetaAnterior;
	public GameObject probetaAnterior2;
	public GameObject probetasEntorno;
	public string []colores;
	public Text texto;
	private RaycastHit hit;
	private Vector3 playerLookDir;
	public GameObject head;
	public GameObject colisionadorMesa;
	void Start () {
		colisionadorMesa.SetActive (false);
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
		probetasEntorno.SetActive (false);
	}

	void Update () {
		playerLookDir = new Vector3(head.transform.forward.x, head.transform.forward.y,head.transform.forward.z);
		Debug.DrawRay (transform.position, playerLookDir);
		if (Physics.Raycast (transform.position, playerLookDir, out hit, 10.0f)) {
			Debug.Log ("asdf " + hit.collider.tag);
		}
	}

	int generarColorAleatorio(){
		return Random.Range (0,colores.Length);
	}
}