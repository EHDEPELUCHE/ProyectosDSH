using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    AudioSource audioSource;
    Animation animacion;
    public GameObject balaPrefab;
    public  Transform salida;
    public float nextshot = 0.0f;
    public float fireRate = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
        animacion = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextshot && Input.GetMouseButton(0)) {
            nextshot = Time.time + fireRate;
            audioSource.Play();
            animacion.wrapMode = WrapMode.Once;
            animacion.Play();
            GameObject bala = Instantiate(balaPrefab, salida.position, salida.rotation);
            Destroy(bala, 5);
        }
    }
}
