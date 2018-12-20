using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour {

    NavMeshAgent agente;
    // ES EL OBJETIVO
    //public Transform target;


    public GameObject targetCircle;

    // PARA HACER LA ANIMACION
    Animator animador;

    // ESTADO DEL PLAYER
    enum Estado { Idle, Andando, Corriendo, Saltando, Disparando };
    // LO INICIALIZAMOS A ESTADO QUIETO
    Estado estado = Estado.Idle;

    // capa
    public LayerMask walkableLayer;

    // BALA
    [SerializeField] GameObject bala;
    [SerializeField] Transform puntoGeneracion;
    int fuerzaDisparo = 500;

    // Use this for initialization
    void Start() {
        agente = GetComponent<NavMeshAgent>();
        //agente.destination = target.position;

        animador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            // PARA MOVER EL CIRCULITO A ESA POSICION DEL RATON
            ManageMouseClick();
        }

        // VAMOS HACER QUE DISPARE ALGO Y QUE EL VIGILANTE CAMBIE SU DESTINO
        if (Input.GetKeyDown(KeyCode.V)) {
            LanzarBala();
        }

        //EVALUAMOS LOS ESTADOS
        switch (estado) {
            case Estado.Idle:
                // NO HAGO NADA
                break;

            case Estado.Andando:
                // TENEMOS QUE CONTROLAR SI CUANDO ANDAS SI HAS LLEGADO AL DESTINO
                ComprobarDestino();
                break;

            case Estado.Corriendo:

                break;

            case Estado.Saltando:

                break;

            case Estado.Disparando:

                break;

        }


        /*// DISTANCIA QUE HAY DESDE LA POSICION DEL AGENTE HASTA EL CIRCULO
        // PORQUE SE LO HEMOS PUESTO EN EL DESTINO
        if (agente.remainingDistance < 1 ){
            animador.SetBool("Andando", false);
        }*/

        /*if (agente.remainingDistance <= agente.stoppingDistance) { 
            animador.SetBool("Andando", false);
        }*/
    }


    private void LanzarBala() {
        GameObject nuevaBala = Instantiate(bala, puntoGeneracion.position, puntoGeneracion.rotation);
        // TIENE QUE TENER UN RIGID PARA APLICAR UNA FUERZA
        nuevaBala.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * fuerzaDisparo);
    }

    private void ManageMouseClick() {
        // CON LA CAMARA PRINCIPAL POR SI HUBIESE PROBLEMAS
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit rch;
        //bool hasTouch = Physics.Raycast(ray, out rch);
        // Modificamos con respecto al Layer que hemos creado, la capa que haga de filtro
        // rch es la direccion hacia conde quiero que vaya
        bool hasTouch = Physics.Raycast(ray, out rch, Mathf.Infinity, walkableLayer);
        if (hasTouch) {

            //EVALUAMOS LOS ESTADOS
            switch (estado) {
                case Estado.Idle:
                    // SI ESTAMOS EN REPOSO Y DAMOS UN CLICK VAMOS A ANDAR
                    // LE PASAMOS EL RCH, TAMBIEN SE PODRIA HACER PUBLICA PERO LO VERIAN TODOS LOS METODOS
                    // LO MEJOR ES PASAR POR PARAMETRO
                    Andar(rch);
                    break;

                case Estado.Andando:
                    Andar(rch);
                    break;

                case Estado.Corriendo:

                    break;

                case Estado.Saltando:

                    break;

                case Estado.Disparando:

                    break;

            }

            /*targetCircle.transform.position = rch.point;
            targetCircle.transform.rotation = Quaternion.LookRotation(rch.normal);
            agente.destination = targetCircle.transform.position;


            // VAMOS HACER LA ANIMACION
            animador.SetBool("Andando", true);*/
        }

    }

    // PARA QUE ANDE y le pasamos el rch
    private void Andar(RaycastHit rch) {

        targetCircle.transform.position = rch.point;
        targetCircle.transform.rotation = Quaternion.LookRotation(rch.normal);
        agente.destination = targetCircle.transform.position;


        // VAMOS HACER LA ANIMACION
        animador.SetBool("Andando", true);

        // CAMBIAMOS EL ESTADO
        estado = Estado.Andando;
    }


    // COMPROBAMOS EL DESTINO
    private void ComprobarDestino() {
        // SI HA CALCULADO LA RUTA, QUE ES == FALSE
        // TARDA EN CALCULAR LA RUTA
        if (!agente.pathPending) {
            // DISTANCIA QUE QUEDA POR RECORRER <= A LA DISTANCIA QUE SE PARA
            if (agente.remainingDistance <= agente.stoppingDistance) {
                animador.SetBool("Andando", false);
                // CAMBIAMOS SU ESTADO A PARADO
                estado = Estado.Idle;
            }
        }
    }


}
