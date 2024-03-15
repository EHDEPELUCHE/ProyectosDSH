using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Jugador : MonoBehaviour
{
    public float vidaRestante;
    public UnityEngine.UI.Image barraVida;
    public Text tvida;
    // Start is called before the first frame update
    void Start()
    {
        vidaRestante = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GolpeoJug() {
        vidaRestante = GetComponent<gestionVidaJugador>().vida / GetComponent<gestionVidaJugador>().MaxVida;
        barraVida.transform.localScale = new Vector3(vidaRestante, 1, 1);
        tvida.text = "Vida " + 10 * vidaRestante + "/10";
    }

    public void FinPartida() {
        Destroy(gameObject);
        SceneManager.LoadScene("Derrota");
    }
}
