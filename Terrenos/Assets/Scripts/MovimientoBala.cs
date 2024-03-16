using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBala : MonoBehaviour
{
    public float speed = 20f;
    private float damage = 1f;
    // Update is called once per frame
    void Update()
    {
        float moveDistancia = Time.deltaTime * speed;
        transform.Translate(Vector3.forward * moveDistancia);
    }

    void OnTriggerEnter(Collider other) {
       // Debug.Log("tocado");
        other.SendMessage("tocado", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}