﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minijuego1 : MonoBehaviour {
	
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

	void OnTriggerEnter(Collider Other){
		if (!terminado) {
			if (Other.CompareTag("Rojo")) {
				rojo = 1.0f;
				saberRojo = true;
				color = new Color (rojo,verde,azul,1.0f);
				rend.material.SetColor("_Color", color);
			}
			if (Other.CompareTag("Verde")&&saberRojo) {
				verde = 1.0f;
				saberVerde = true;
				color = new Color (rojo,verde,azul,1.0f);
				rend.material.SetColor("_Color", color);
			}
			if (Other.CompareTag("Azul")&&saberRojo&&saberVerde) {
				azul = 1.0f;
				saberAzul = true;
				color = new Color (rojo,verde,azul,1.0f);
				rend.material.SetColor("_Color", color);
			}
		}
		if (!terminado&&saberAzul&&saberRojo&&saberVerde) {
			terminado = true;
			gameObject.GetComponent<Movement> ().dosChulos.SetActive(true);
			gameObject.GetComponent<Movement> ().setChulos (2);
			gameObject.GetComponent<Movement> ().unChulo.SetActive (false);
		}
	}

	public bool getTerminado(){
		return terminado;
	}
}