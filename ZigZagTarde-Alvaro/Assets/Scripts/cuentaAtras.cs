using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cuentaAtras : MonoBehaviour
{
    //Públicas
    private Button boton;
    public Image imagen;
    public Sprite[] numeros;
    // Start is called before the first frame update
    void Start()
    {
        boton = GameObject.FindWithTag("botonEmpezar").GetComponent<Button>();
        boton.onClick.AddListener(Empezar);
    }

    void Empezar() {
        imagen.gameObject.SetActive(true);
        boton.gameObject.SetActive(false);
        StartCoroutine(CuentaAtras());
    }

    IEnumerator CuentaAtras() {
        for (int i = numeros.Length - 1; i > -1; i--) {
            imagen.sprite = numeros[i];
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("nivelUno");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
