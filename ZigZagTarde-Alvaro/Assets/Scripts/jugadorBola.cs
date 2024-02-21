using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class jugadorBola : MonoBehaviour
{
    //Publicas
    public Camera camara;
    public GameObject suelo;
    public float velocidad = 10.0f; 
    //Privadas
    private Vector3 offSet;
    private float ValX, ValZ;
    private Vector3 DireccionActual;
    private int suelos = 0;
    private int maxSuelo = 8;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        offSet = camara.transform.position;
        CrearSueloInicial();
        DireccionActual = Vector3.forward;
        rb = GetComponent<Rigidbody>();    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        camara.transform.position = transform.position + offSet;
        if (Input.GetKeyUp(KeyCode.Space)) 
            cambiarDireccion();
        //rb.AddTorque(DireccionActual * velocidad);
        transform.Translate(DireccionActual * velocidad * Time.deltaTime);
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Suelo")) {
            StartCoroutine(BorrarSuelo(other.gameObject));
            if (suelos < maxSuelo) {
                StartCoroutine(CrearSuelo(other.gameObject));
            }
        }
    }

    IEnumerator CrearSuelo(GameObject suelo) {
        if (suelos < maxSuelo) {
            float aleatorio = Random.Range(0.0f, 1.0f);
            if (aleatorio > 0.5) ValX += 6.0f;
            else ValZ += 6.0f;
            Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            suelos++;
        } else yield return new WaitForSeconds(0.1f);
    }

    IEnumerator BorrarSuelo(GameObject suelo) {
        yield return new WaitForSeconds(1.0f);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = false;
        Destroy(suelo);
        suelos--;
    }

    void cambiarDireccion() {
        if (DireccionActual == Vector3.forward)
            DireccionActual = Vector3.right;
        else
            DireccionActual = Vector3.forward;
    }

    void CrearSueloInicial() {
        for (int j = 0; j < 3; j++) {
            ValZ += 6.0f;
            Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            suelos++;
        }
    }
}
