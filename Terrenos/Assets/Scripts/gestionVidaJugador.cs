using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class gestionVidaJugador : MonoBehaviour
{
    
    public UnityEvent GolpeoJug;
    public UnityEvent FinPartida;
    public float vida = 10f;
    public float MaxVida = 10f;
    float timetoRegen = 5.0f;
    // Start is called before the first frame update

    void Update() {
        if (vida < MaxVida) {
            timetoRegen -= Time.deltaTime;
            if (timetoRegen <= 0) {
                if (MaxVida - vida >= 2) {
                    vida += 2f;
                } else {
                    vida += 1f;
                }
                GolpeoJug.Invoke();
                timetoRegen = 5.0f;
            }
        }
    }

    void herido(float fuerza) {
        Debug.Log("Jugador herido");
        vida -= fuerza;
        GolpeoJug.Invoke();
        if (vida <= 0) {
            FinPartida.Invoke();
        }
    }
}
