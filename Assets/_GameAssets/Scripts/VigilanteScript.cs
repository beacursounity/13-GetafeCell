using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VigilanteScript : MonoBehaviour {

    public Transform[] puntosPatrulla;

    NavMeshAgent agente;

    // ESTADO DEL VIGILANTE LO HE COGIDO DEL PLAYER YA QUE HARA LO MISMO
    enum Estado { Idle, Andando, Corriendo, Saltando, Disparando , Siguiendo , Distraido};
    // LO INICIALIZAMOS A ESTADO QUIETO
    Estado estado = Estado.Idle;

    // TIEMPO ESPERA ENTRE ASIGNACIONES DE PUNTOS DE PATRULLA
    const int TIEMPO_ESPERA = 1;

    // TEXTOS DEL CANVAS Y LOS TIENE QUE MOSTRAR CONTINUAMETNE EN EL UPDATE
    public Text textDTP;
    public Text textATP;

    public GameObject player;

    float anguloVision = 27;
    float distanciaVision = 59 ;

    // PARA SABER SI ESTA A TIRO EL PLAYER
    public Text aTiro;

    // Use this for initialization
    void Start() {
        agente = GetComponent<NavMeshAgent>();
        // EL DESTINO DE LA GENTE SERA EL PRIMER PUNTO DEL ARRAY
        //agente.destination = puntosPatrulla[0].position;
        // CREO METODO  CON LO MISMO
        AsignarPuntoPatrulla();
    }



    // Update is called once per frame
    void Update() {

        /*// DISTANCIA ENTRE POSICION DE LA VIGILANCIA Y EL PLAYER para mi es 59
        float distancia = Vector3.Distance(transform.position, player.transform.position);
        textDTP.text = "DTP: "+distancia;

        // ANGULO
        // HABRIA QUE COMPROBAR SI LA DISTANCIA ES LA ADECUADA HARIA NO NO EL CALCULO DEL ANGULO
        // esto esta mal ya que cojo al player en el angulo yu no tiene nada que ver
        //float angulo = Vector3.Angle(transform.position, player.transform.position);
        // DIRECCION DE MAGNITUD 1 EL VECTOR QUE ME LLEVA AL PLAYER
        // nos da igual el Normalize porque solo miramos la direccion
        Vector3 direccion = Vector3.Normalize(player.transform.position - transform.position); 
        float angulo = Vector3.Angle(direccion, transform.forward);

        // SI ME ESTA VIENDO EL VIGILANTE TENDRE QUE LANZAR UN RAYCAST 
        // PARA SABER SI LE DOY O NO POR SI TENGO COLISIONADORES POR ENMEDIO
        if (distancia < distanciaVision && angulo < anguloVision) {
            
            Debug.DrawLine(transform.position, player.transform.position, Color.red, 2);
            RaycastHit rch;
            if (Physics.Raycast(transform.position, direccion, out rch, Mathf.Infinity)){
                // para saber con quien me choco
                print(rch.transform.gameObject.name);
                if (rch.transform.gameObject.name == "Player") {
                    aTiro.text = "A tiro: SI";
                    // LE PONEMOS EL NUEVO DESTINO SE LA PONEMOS EN EL UPDATE EN EL SWITCH
                    //agente.destination = player.transform.position;
                    estado = Estado.Siguiendo;
                } else {
                    aTiro.text = "A tiro: NO";
                }
            }
        } 
        // SI NO ESTA DENTRO DE LA VISION LO PONEMOS A "NO"
        else {
            aTiro.text = "A tiro: NO";
        }
        
        textATP.text = "ATP: " + angulo; */

        if (estado != Estado.Distraido) {
            VerificarObjetivo();
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

            case Estado.Siguiendo:
                // LE PONEMOS EL NUEVO DESTINO
                agente.destination = player.transform.position;
                break;

            case Estado.Distraido:
                ComprobarDestino();
                break;

        }

    }

    /*    private void AsignarPuntoPatrulla() {
            // EL DESTINO DE LA GENTE SERA EL PRIMER PUNTO DEL ARRAY
            agente.destination = puntosPatrulla[0].position;
            estado = Estado.Andando;
        }*/

    // ASIGNAR PUNTO PATRULLA ALEATORIO
    private void AsignarPuntoPatrulla() {
        // EL DESTINO DE LA GENTE SERA EL PRIMER PUNTO DEL ARRAY
        // PERO HAREMOS UN AHORA UN RANGO
        // SI NO ESTA DISTRAIDO IRA AL PUNTO DE PATRULLA ELEGIDO ALEATORIAMENTE
        if ( estado != Estado.Distraido){
            int pp = Random.Range(0, puntosPatrulla.Length);
            agente.destination = puntosPatrulla[pp].position;
            estado = Estado.Andando;
        }

    }


    /* int pp = 0;
      private void AsignarPuntoPatrulla() {
        if (pp == puntosPatrulla.Length) pp = 00;
        // EL DESTINO DE LA GENTE SERA EL PRIMER PUNTO DEL ARRAY
        agente.destination = puntosPatrulla[pp].position;
        estado = Estado.Andando;
        // SE LE VUELVE A ASIGNAR EL PUNTO DE PATRULLA
    }*/

    // COMPROBAMOS EL DESTINO
    private void ComprobarDestino() {
        // SI HA CALCULADO LA RUTA, QUE ES == FALSE
        // TARDA EN CALCULAR LA RUTA
        if (!agente.pathPending) {
            // DISTANCIA QUE QUEDA POR RECORRER <= A LA DISTANCIA QUE SE PARA
            if (agente.remainingDistance <= agente.stoppingDistance) {
                //animador.SetBool("Andando", false);
                // CAMBIAMOS SU ESTADO A PARADO
                estado = Estado.Idle;
                // PARA QUE ROTE UN POCO PARA QUE QUEDE MEJOR
                transform.Rotate(0, 180, 0);
                Invoke("AsignarPuntoPatrulla", TIEMPO_ESPERA);
            }
        }
    }

    // CUANDO VA 
    /*public void SetTarget(Vector3 position) {
        agente.destination = position;
        estado = Estado.Andando;
    }*/

    // CUANDO VA 
    public void SetDistraccion(Vector3 position) {
        agente.destination = position;
        estado = Estado.Distraido;
    }


    public void VerificarObjetivo() {
        // DISTANCIA ENTRE POSICION DE LA VIGILANCIA Y EL PLAYER para mi es 59
        float distancia = Vector3.Distance(transform.position, player.transform.position);
        textDTP.text = "DTP: " + distancia;

        // ANGULO
        // HABRIA QUE COMPROBAR SI LA DISTANCIA ES LA ADECUADA HARIA NO NO EL CALCULO DEL ANGULO
        // esto esta mal ya que cojo al player en el angulo yu no tiene nada que ver
        //float angulo = Vector3.Angle(transform.position, player.transform.position);
        // DIRECCION DE MAGNITUD 1 EL VECTOR QUE ME LLEVA AL PLAYER
        // nos da igual el Normalize porque solo miramos la direccion
        Vector3 direccion = Vector3.Normalize(player.transform.position - transform.position);
        float angulo = Vector3.Angle(direccion, transform.forward);

        // SI ME ESTA VIENDO EL VIGILANTE TENDRE QUE LANZAR UN RAYCAST 
        // PARA SABER SI LE DOY O NO POR SI TENGO COLISIONADORES POR ENMEDIO
        if (distancia < distanciaVision && angulo < anguloVision) {

            Debug.DrawLine(transform.position, player.transform.position, Color.red, 2);
            RaycastHit rch;
            if (Physics.Raycast(transform.position, direccion, out rch, Mathf.Infinity)) {
                // para saber con quien me choco
                print(rch.transform.gameObject.name);
                if (rch.transform.gameObject.name == "Player") {
                    aTiro.text = "A tiro: SI";
                    // LE PONEMOS EL NUEVO DESTINO SE LA PONEMOS EN EL UPDATE EN EL SWITCH
                    //agente.destination = player.transform.position;
                    estado = Estado.Siguiendo;
                } else {
                    aTiro.text = "A tiro: NO";
                }
            }
        }
        // SI NO ESTA DENTRO DE LA VISION LO PONEMOS A "NO"
        else {
            aTiro.text = "A tiro: NO";
        }

        textATP.text = "ATP: " + angulo;
    }
}
