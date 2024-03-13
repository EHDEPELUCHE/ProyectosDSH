using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Enemigos : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    public float vidaRestante;
    public UnityEngine.UI.Image barraVida;
    // Start is called before the first frame update
    void Start()
    {
        vidaRestante = 5.0f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        pathfinder.SetDestination(target.position);
    }

    public void heSidoTocado() {
        vidaRestante = GetComponent<gestionVida>().vida / GetComponent<gestionVida>().MaxVida;
        barraVida.transform.localScale = new Vector3(vidaRestante, 1, 1);
    }

    public void muerte() {
        Destroy(gameObject);
    }
}
