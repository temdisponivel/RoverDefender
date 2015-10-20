using UnityEngine;
using System.Collections;

public class MenuStart : MonoBehaviour {
	public GameObject preesanydisplay;
	public Animator animatormenupanel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			if(Input.anyKeyDown){
				animatormenupanel.SetBool("painel",true);
				preesanydisplay.SetActive(false);
			}
	}
}
