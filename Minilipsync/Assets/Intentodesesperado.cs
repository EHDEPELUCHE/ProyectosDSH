using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intentodesesperado : MonoBehaviour
{
     public Animator animator;
    public AudioClip audioClip;
     private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
         audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
         animator.Play(audioClip.name.Substring(0, 10), 1, 0);
        Debug.Log("Animator haciendo algo");
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
