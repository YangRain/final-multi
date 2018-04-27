using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class itemone : MonoBehaviour
{
    private GameObject thegamemanager;

    private void Start()
    {
        thegamemanager = GameObject.FindGameObjectWithTag("gamemanager");
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

                other.GetComponent<player>().Cmdimmuestatetrigger();
                Destroy(this.gameObject);
           
        }
    }

    void Rpcchangematerial()
    {
        thegamemanager.GetComponent<gamemanagerment>().respawnitem = gamemanagerment.hasitem.donthave;
    }

    void Cmdchangematerial()
    {
        thegamemanager.GetComponent<gamemanagerment>().respawnitem = gamemanagerment.hasitem.donthave;
        Rpcchangematerial();
    }
}
