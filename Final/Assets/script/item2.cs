using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class item2 : NetworkBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
                other.GetComponent<player>().item = 2;
                other.GetComponent<Health>().currentHealth += 30;
                Destroy(gameObject);
        }
    }

}
