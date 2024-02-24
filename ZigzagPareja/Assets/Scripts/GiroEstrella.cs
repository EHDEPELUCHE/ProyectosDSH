using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueBola : MonoBehaviour
{
    public GameObject particulas;
    public float speed = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World ); 
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        Vector3 posactual = new Vector3 (transform.position.x, 0.5f, transform.position.z);
        Instantiate(particulas, posactual, particulas.transform.rotation);
        //Debug.Log("Se destruye");
    }
}
