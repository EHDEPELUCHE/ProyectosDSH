using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class jugadorBola : MonoBehaviour
{
    //Publicas
    public Camera camara;
    public GameObject suelo;
    public float velocidad = 10.0f; 
    public GameObject estrella;
    public GameObject estrellaesp;
    public Text textoPuntosBack;
    public Text textoPuntos;
    //Privadas
    private Vector3 offSet;
    private float ValX, ValZ;
    private Vector3 DireccionActual;
    private int suelos = 0;
    private int maxSuelo = 8;
    private Rigidbody rb;
    private int estrellas=0;
    private List<GameObject> instancedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        offSet = camara.transform.position;
        CrearSueloInicial();
        DireccionActual = Vector3.forward;
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
            cambiarDireccion();
        if (gameObject.transform.position.y < 0){
            //Debug.Log("Cayendo");
            SceneManager.LoadScene("Derrota", LoadSceneMode.Single);
        }
    }

    void FixedUpdate()
    {
        camara.transform.position = transform.position + offSet;
        Vector3 horizontalVelocity = DireccionActual * velocidad;
        horizontalVelocity.y = rb.velocity.y;  // Mantén la velocidad vertical actual
        rb.velocity = horizontalVelocity;
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
            GameObject newSuelo = Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            instancedObjects.Add(newSuelo);
            suelos++;
            aleatorio = Random.Range(0.0f, 1.0f);
            if (aleatorio > 0.7) Instantiate(estrella, new Vector3(Random.Range((ValX - 3),ValX), 1, Random.Range((ValZ - 3), ValZ)), Quaternion.identity);
            else if (aleatorio < 0.1) Instantiate(estrellaesp, new Vector3(Random.Range((ValX - 3),ValX), 1, Random.Range((ValZ - 3), ValZ)), Quaternion.identity);
        } else yield return new WaitForSeconds(0.1f);
    }

    IEnumerator BorrarSuelo(GameObject suelo) {
        yield return new WaitForSeconds(1.0f);
        if (suelo != null) {
            Rigidbody sueloRb = suelo.GetComponent<Rigidbody>();
            if (sueloRb != null) {
                sueloRb.isKinematic = false;
                sueloRb.useGravity = false;
            }
            instancedObjects.Remove(suelo);
            Destroy(suelo);
            suelos--;
        }
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

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("EstrellaBasica")){
            //Debug.Log("he tocado una estrella");
            Destroy(other.gameObject);
            estrellas++;
            textoPuntosBack.text = " " + estrellas;
            textoPuntos.text = " " + estrellas;
        }else if(other.gameObject.CompareTag("EstrellaEspecial")){
            //Debug.Log("he tocado una estrella especial");
            Destroy(other.gameObject);
            estrellas+=5;
            textoPuntosBack.text = " " + estrellas;
            textoPuntos.text = " " + estrellas;
        }

        if(estrellas >= 30){
            SceneManager.LoadScene("Nivel2", LoadSceneMode.Single);
        }
    }

    private void OnDisable() {
        foreach (GameObject obj in instancedObjects) {
            if (obj != null) Destroy(obj);
    }
}
}