using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rondas : MonoBehaviour
{
    GameObject Spawner;
    public ValoresEnemigos[] valoresEnemigos;
    public ValoresEnemigos enemigoActual;
    float tiempoEspera = 0;
    int numRondaActual = 0;
    int enemigosporCrear = 0;
    int enemigosporMatar = 0;

    // Start is called before the first frame update
    void Start()
    {
        NextRonda();
        Spawner = GameObject.FindGameObjectWithTag("Rondas");
    }

    void EnemigoMuerto() {
        enemigosporMatar--;
        if (enemigosporMatar <= 0)
            NextRonda();
    }

    void NextRonda() {
        numRondaActual++;
        enemigoActual = valoresEnemigos[numRondaActual - 1];
        enemigosporCrear = enemigoActual.numeroEnemigos;
        enemigosporMatar = enemigoActual.numeroEnemigos;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemigosporCrear > 0 && tiempoEspera <= 0) {
            Instantiate(enemigoActual.tipoEnemigo, Spawner.transform.position, Quaternion.identity);
            enemigosporCrear--;
            tiempoEspera = enemigoActual.tiempoEntreEnemigos;
        } else {
            tiempoEspera -= Time.deltaTime;
        }
    }
}
