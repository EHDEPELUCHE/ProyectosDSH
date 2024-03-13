using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class gestionVida : MonoBehaviour
{
    public UnityEvent heSidoTocado;
    public UnityEvent muerte;

    public float vida = 5.0f;
    public float MaxVida = 5.0f;
    
    void tocado(float fuerza) {
        Debug.Log("Quito fuerza");
        vida -= fuerza;
        heSidoTocado.Invoke();
        if (vida <= 0) {
            muerte.Invoke();
        }
    }


}
