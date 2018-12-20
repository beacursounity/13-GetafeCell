using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveScript : MonoBehaviour {

    public Animator animatorPuerta;

    // QUIERO HABLAR CON EL SCRIPT
    public VigilanteScript vs;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Player") {
            print("ha colisionado con el Player");
            // ACTIVAMOS EL SISTEMA DE PARTICULAS
            GetComponent<ParticleSystem>().Play();

            // ABRIMOS LA PUERTA CON LA ANIMACION
            AbrirPuerta();
        }
    }

    // METODO DE ABRIR PUERTA
    void AbrirPuerta() {
        // PARA QUE VAYA AL METODO DEL VIGILANTE Y LO QUE TENGA QUE HACER LO HAGA ALLI
        // vs.SetTarget(transform.position);

        vs.SetDistraccion(transform.position);

        animatorPuerta.SetBool("AbreteSesamo", true);
        Destroy(gameObject);
    }
}
