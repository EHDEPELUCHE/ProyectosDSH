using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Enemigos : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    GameObject player;
    GameObject controladorRondas;
    public float vidaRestante;
    public UnityEngine.UI.Image barraVida;
    float myCollisionradius;
    float targetCollisionradius;
    float DistanciadeAtaque = 2f;
    float NextAttackTime =0;
    float TimeBetweenattack = 0.5f;
    bool atacando = false;
    // Start is called before the first frame update
    void Start()
    {
        vidaRestante = 5.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        controladorRondas = GameObject.FindGameObjectWithTag("Rondas");
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //gestionVidaJugador = player.GetComponent<gestionVidaJugador>();
        myCollisionradius = GetComponent<CapsuleCollider>().radius;
        targetCollisionradius = target.GetComponent<CapsuleCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        if(!atacando){
           // Debug.Log("Actualiza");
           Vector3 dirtoTarget = (target.position - transform.position).normalized;
            Vector3 TargetPosition = target.position - dirtoTarget * (myCollisionradius + targetCollisionradius + DistanciadeAtaque);
            pathfinder.SetDestination(TargetPosition);
            if(Time.time > NextAttackTime){
                //Debug.Log("Tiempo pasa");
                NextAttackTime = TimeBetweenattack + Time.time;
                float sqrDsttoTarget = (target.position - transform.position).sqrMagnitude;
                if (sqrDsttoTarget <= Mathf.Pow(myCollisionradius + targetCollisionradius + DistanciadeAtaque, 2)){
                    Debug.Log("Estoy al lado");
                    StartCoroutine(Attack());
                } 
            } 
        }
        
    }

    IEnumerator Attack(){
        pathfinder.enabled = false;
        atacando = true;
        float percent =0;
        float attackspeed = 1;
        float damage =1;
        bool hasAppliedDamage = false;

        while (percent <= 1){
            if(percent >= 0.5f && !hasAppliedDamage){
                Debug.Log("LE HE PEGADO");
                player.SendMessage("herido", damage, SendMessageOptions.DontRequireReceiver);
                hasAppliedDamage = true;
            }
                percent += Time.deltaTime * attackspeed;
                yield return null;
        }
        atacando = false;
        pathfinder.enabled = true;
    }

    public void heSidoTocado() {
        vidaRestante = GetComponent<gestionVida>().vida / GetComponent<gestionVida>().MaxVida;
        barraVida.transform.localScale = new Vector3(vidaRestante, 1, 1);
    }

    public void muerte() {
        Destroy(gameObject);
        controladorRondas.SendMessage("EnemigoMuerto", SendMessageOptions.DontRequireReceiver);
    }
}
