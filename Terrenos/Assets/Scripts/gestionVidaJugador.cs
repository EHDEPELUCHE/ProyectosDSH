using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gestionVidaJugador : MonoBehaviour
{
    
    public UnityEvent GolpeoJug;
    public UnityEvent FinPartida;
    public float vida = 10f;
    public float MaxVida = 10f;
    // Start is called before the first frame update
 
    void herido(float fuerza) {
        //Debug.Log("Quito fuerza");
        vida -= fuerza;
        GolpeoJug.Invoke();
        if (vida <= 0) {
            FinPartida.Invoke();
        }
    }
}
