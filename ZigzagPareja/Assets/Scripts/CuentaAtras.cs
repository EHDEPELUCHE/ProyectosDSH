using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CuentaAtras : MonoBehaviour
{
    //Publica
    private Button botonEmpezar;
    private Button botonSalir;
    private Button botonReiniciar;
    public Image imagen;
    public Sprite[] numeros;

    // Start is called before the first frame update
    void Start()
    {
        botonEmpezar = GameObject.FindWithTag("EmpezarBoton").GetComponent<Button>();
        botonSalir = GameObject.FindWithTag("Salir").GetComponent<Button>();
       
        botonEmpezar.onClick.AddListener(Empezar);
        botonSalir.onClick.AddListener(Terminar);
       
    }

    void Empezar(){
        imagen.gameObject.SetActive(true);
        botonEmpezar.gameObject.SetActive(false);
        botonSalir.gameObject.SetActive(false);
        StartCoroutine(cuentaAtras());
    }

    void Terminar(){
        Debug.Log("Cerrando");
        Application.Quit();
    }

    public void Reinicio(){
        SceneManager.LoadScene("MenuInicial");
    }
    IEnumerator cuentaAtras(){
        for(int i = 2;i >= 0; i--){
            imagen.sprite = numeros[i];
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Nivel1");
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
