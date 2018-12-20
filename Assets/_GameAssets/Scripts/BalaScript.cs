using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript : MonoBehaviour {

    // QUIERO HABLAR CON EL SCRIPT
    private VigilanteScript vs;

    bool primeraVez = true;

    public int timeToDestroy = 3;
  

    private void Start() {
        // QUE BUSQUE EL VIGILANTE Y QUE BUSCQUE EL SCRIPT
        vs = GameObject.Find("Vigilante").GetComponent<VigilanteScript>();
    }

    private void OnCollisionEnter(Collision collision) {
        // para que no se destruya en unos segundos
        if (primeraVez) {
            // PARA QUE VAYA AL METODO DEL VIGILANTE Y LO QUE TENGA QUE HACER LO HAGA ALLI
            vs.SetDistraccion (transform.position);
            primeraVez = false;
            // el this destruye el Script
            Destroy(this.gameObject,timeToDestroy);
        }


        /*vs.SetTarget(transform.position);
        // DESTRUYE EL SCRIPT
        Destroy(this);
        */

    }

}
