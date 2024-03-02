using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class jugadorBola2 : MonoBehaviour
{
    //Publicas
    public Camera camara;
    public GameObject suelo;
    public float velocidad = 10.0f; 
    public GameObject estrella;
    public GameObject estrellaesp;
    public GameObject trippy;
    private bool drogado = false;
    public Text textoPuntosBack;
    public Text textoPuntos;
    public AudioClip Est;
    //Privadas
    private Vector3 offSet;
    private float ValX, ValZ;
    private Vector3 DireccionActual;
    private int suelos = 0;
    private int maxSuelo = 8;
    private Rigidbody rb;
    private int estrellas=0;

    private AudioSource audioEst;
    
    
    private List<GameObject> instancedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        offSet = camara.transform.position;
        CrearSueloInicial();
        DireccionActual = Vector3.forward;
        rb = GetComponent<Rigidbody>(); 
        jugadorBola1.Destruirya = false;
        audioEst = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonDown(0)) // La segunda condicion es para poder usar el raton o pulsar en el movil
            cambiarDireccion();
        if (gameObject.transform.position.y < -5){
            //Debug.Log("Cayendo");
            jugadorBola1.Destruirya = true;
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
        if (!drogado) {
            if (suelos < maxSuelo) {
                float aleatorio = Random.Range(0.0f, 1.0f);
                if (aleatorio > 0.5) ValX += 6.0f;
                else ValZ += 6.0f;
                GameObject newSuelo = Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
                instancedObjects.Add(newSuelo);
                Destroy(newSuelo, 5f);
                suelos++;
                aleatorio = Random.Range(0.0f, 1.0f);
                if (aleatorio > 0.7) {
                    GameObject newEstrella = Instantiate(estrella, new Vector3(Random.Range((ValX - 3),ValX), 1, Random.Range((ValZ - 3), ValZ)), Quaternion.identity);
                    Destroy(newEstrella, 5f);
                }
                else if (aleatorio < 0.1) {
                    GameObject newEstrellaEsp = Instantiate(estrellaesp, new Vector3(Random.Range((ValX - 3),ValX), 1, Random.Range((ValZ - 3), ValZ)), Quaternion.identity);
                    Destroy(newEstrellaEsp, 5f);
                }
                aleatorio = Random.Range(0.0f, 20.0f);
                if (aleatorio < 1.0f) {
                    GameObject newTrippy =  Instantiate(trippy, new Vector3(Random.Range((ValX - 3),ValX), 1, Random.Range((ValZ - 3), ValZ)), Quaternion.identity);
                    Destroy(newTrippy, 5f);
                } 
            } else yield return new WaitForSeconds(0.05f);
        } else {
            if (suelos < maxSuelo) {
                float aleatorio = Random.Range(0.0f, 1.0f);
                if (aleatorio > 0.5) ValX -= 6.0f;
                else ValZ -= 6.0f;
                GameObject newSuelo = Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
                instancedObjects.Add(newSuelo);
                Destroy(newSuelo, 5f);
                suelos++;
                aleatorio = Random.Range(0.0f, 1.0f);
                if (aleatorio > 0.7) {
                    GameObject newEstrella = Instantiate(estrella, new Vector3(Random.Range((ValX + 3),ValX), 1, Random.Range((ValZ + 3), ValZ)), Quaternion.identity);
                    Destroy(newEstrella, 5f);
                }
                else if (aleatorio < 0.1) {
                    GameObject newEstrellaEsp = Instantiate(estrellaesp, new Vector3(Random.Range((ValX + 3),ValX), 1, Random.Range((ValZ + 3), ValZ)), Quaternion.identity);
                    Destroy(newEstrellaEsp, 5f);
                }
                aleatorio = Random.Range(0.0f, 20.0f);
                if (aleatorio < 1.0f) {
                    GameObject newTrippy =  Instantiate(trippy, new Vector3(Random.Range((ValX + 3),ValX), 1, Random.Range((ValZ + 3), ValZ)), Quaternion.identity);
                    Destroy(newTrippy, 5f);
                }
            } else yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator BorrarSuelo(GameObject suelo) {
        yield return new WaitForSeconds(0.75f);
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
        if (!drogado) {
            if (DireccionActual == Vector3.forward)
                DireccionActual = Vector3.right;
            else
                DireccionActual = Vector3.forward;
        } else {
            if (DireccionActual == Vector3.back)
                DireccionActual = Vector3.left;
            else
                DireccionActual = Vector3.back;
        }
    }

    void CrearSueloInicial() {
        for (int j = 0; j < 3; j++) {
            ValZ += 6.0f;
            Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            suelos++;
        }
    }

    void CrearSueloCambio(){
        if (drogado) {
            ValZ = gameObject.transform.position.z;
            ValX = gameObject.transform.position.x;
            for (int j = 0; j < 6; j++) {
                ValZ -= 6.0f;
                Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
                suelos++;
            }
            DireccionActual = Vector3.back;
        } else {
            ValZ = gameObject.transform.position.z;
            ValX = gameObject.transform.position.x;
            for (int j = 0; j < 6; j++) {
                ValZ += 6.0f;
                Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
                suelos++;
            } 
            DireccionActual = Vector3.forward;
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("EstrellaBasica")){
            //Debug.Log("he tocado una estrella");
            audioEst.PlayOneShot(Est);
            Destroy(other.gameObject);
            estrellas++;
            textoPuntosBack.text = " " + estrellas;
            textoPuntos.text = " " + estrellas;
        }else if(other.gameObject.CompareTag("EstrellaEspecial")){
            //Debug.Log("he tocado una estrella especial");
            audioEst.PlayOneShot(Est);
            Destroy(other.gameObject);
            estrellas+=5;
            textoPuntosBack.text = " " + estrellas;
            textoPuntos.text = " " + estrellas;
        } else if(other.gameObject.CompareTag("LSD")) {
            drogado = !drogado;
            suelos = 0;
            CrearSueloCambio();
        }

        if(estrellas >= 30){
            jugadorBola1.Destruirya = true;        
            SceneManager.LoadScene("Nivel3", LoadSceneMode.Single);
        }
    }

    private void OnDisable() {
        foreach (GameObject obj in instancedObjects)
            if (obj != null) Destroy(obj);
    }
}