using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Rondas : MonoBehaviour
{
    GameObject Spawner;
    GameObject Spawner2;
    GameObject Spawner3;
    public ValoresEnemigos[] valoresEnemigos;
    public ValoresEnemigos enemigoActual;
    float tiempoEspera = 0;
    int numRondaActual = 0;
    int enemigosporCrear = 0;
    int enemigosporMatar = 0;
    int Rondastot;
    public Text tronda;

    // Start is called before the first frame update
    void Start()
    {
        NextRonda();
        Rondastot = valoresEnemigos.Length;
        Spawner = GameObject.FindGameObjectWithTag("Rondas");
        Spawner2 = GameObject.FindGameObjectWithTag("Spawner2");
        Spawner3 = GameObject.FindGameObjectWithTag("Spawner3");
    }

    void EnemigoMuerto() {
        enemigosporMatar--;
        if (enemigosporMatar <= 0 && numRondaActual < Rondastot)
            NextRonda();
        else if(enemigosporMatar <= 0 && numRondaActual == Rondastot)
            SceneManager.LoadScene("Victoria");
    }

    void NextRonda() {
        numRondaActual++;
        StartCoroutine(Muestraronda());
        enemigoActual = valoresEnemigos[numRondaActual - 1];
        enemigosporCrear = enemigoActual.numeroEnemigos;
        enemigosporMatar = enemigoActual.numeroEnemigos;
    }

    IEnumerator Muestraronda(){
        tronda.text = "RONDA " + numRondaActual;
        tronda.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        
        tronda.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (enemigosporCrear > 0 && tiempoEspera <= 0) {
            float aleatorio = Random.Range(0.0f, 1.0f);
            if(aleatorio < 0.3f)
                Instantiate(enemigoActual.tipoEnemigo, Spawner.transform.position, Quaternion.identity);
            else if (aleatorio > 0.7f)
                Instantiate(enemigoActual.tipoEnemigo, Spawner2.transform.position, Quaternion.identity);
            else
                Instantiate(enemigoActual.tipoEnemigo, Spawner3.transform.position, Quaternion.identity);
            enemigosporCrear--;
            tiempoEspera = enemigoActual.tiempoEntreEnemigos;
        } else {
            tiempoEspera -= Time.deltaTime;
        }
    }
}
