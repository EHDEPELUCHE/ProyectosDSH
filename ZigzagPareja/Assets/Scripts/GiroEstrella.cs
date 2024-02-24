using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueBola : MonoBehaviour
{
    public GameObject particulas;
    public float speed = 30;
    private GameObject particulasInstance;  // Almacena una referencia a la instancia de "particulas"

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World ); 
        EsperaDestruye();
    }

    private void OnDestroy()
    {
        Vector3 posactual = new Vector3 (transform.position.x, 0.5f, transform.position.z);
        particulasInstance = Instantiate(particulas, posactual, particulas.transform.rotation);
        Destroy(particulasInstance, 5f);  // Destruye la instancia de "particulas" después de 5 segundos
    }

    void EsperaDestruye(){
        if(jugadorBola.Destruirya)
            DestroyImmediate(this,true);   
        else 
            Destroy(this, 7f);
    }
}