using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minijuego1 : MonoBehaviour {
	public Text texto;
	private RaycastHit hit;
	private Vector3 playerLookDir;
	public GameObject head;
	private Color color;
	public GameObject probeta;
	private float rojo;
	private float verde;
	private float azul;
	private bool saberRojo;
	private bool saberVerde;
	private bool saberAzul;
	private bool terminado;
	private Renderer rend;

	void Start () {
		color = new Color(0.5f,0.5f,0.5f,1.0f);
		terminado = false;
		rojo = 0.0f;
		verde = 0.0f;
		azul = 0.0f;
		rend = probeta.GetComponent<Renderer>();
	}

	void Update () {
		playerLookDir = new Vector3(head.transform.forward.x, head.transform.forward.y,head.transform.forward.z);
		if (!terminado) {
			Debug.DrawRay (transform.position, playerLookDir);
			if (Physics.Raycast (transform.position, playerLookDir, out hit, 10.0f)) {
				if (hit.collider.CompareTag("Rojo")) {
					rojo = 1.0f;
					saberRojo = true;
					color = new Color (rojo,verde,azul,1.0f);
					rend.material.SetColor("_Color", color);
				}if (hit.collider.CompareTag("Verde")&&saberRojo) {
					verde = 1.0f;
					saberVerde = true;
					color = new Color (rojo,verde,azul,1.0f);
					rend.material.SetColor("_Color", color);
				}if (hit.collider.CompareTag("Azul")&&saberRojo&&saberVerde) {
					azul = 1.0f;
					saberAzul = true;
					color = new Color (rojo,verde,azul,1.0f);
					rend.material.SetColor("_Color", color);
				}
			}
			if (saberAzul&&saberRojo&&saberVerde) {
				texto.text="Lo has logrado";
				terminado = true;
			}
		}
	}
}