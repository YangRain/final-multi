using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class player : NetworkBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int playercolor;
    public int color;
    [SyncVar]
    public int item = 0;
    private GameObject thegamemanager;
    [SyncVar]
    public int score;
    private float floatscore;
    private float itemtime = 3;
    private bool colroset = false;
    private void Start()
	{    
        thegamemanager = GameObject.FindGameObjectWithTag("gamemanager");
    }
    // Update is called once per frame
    void Update()
    {

        if (thegamemanager.GetComponent<gamemanagerment>().m_gamestate != gamemanagerment.CTF_GameState.Ingame)
        {
            return;
        }
        if (isLocalPlayer == false)
        {
            return;
        }

        if (colroset == false)
        {
            resettheplayercolor();
            colroset = true;
        }


        if (GetComponent<Health>().flag != null && GetComponent<Health>().flag.transform.parent != null)
        {
            Cmdchangescore();
        }

        if (item != 0)
        {

            itemtime -= Time.deltaTime;
            if (itemtime <= 0)
            {
                item = 0;
                itemtime = 7;
                Cmdchangematerial();

            }
            if (item == 1)
            {
                this.GetComponent<Renderer>().material.color = Color.black;
                Cmdchangeimmuematerial();
            }
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 7.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);


        if (Input.GetKeyDown(KeyCode.Space))
        {

            CmdFire();
        }
    }

    void resettheplayercolor()
    {

        Cmdchangecolor();
    }

    [Command]
    void Cmdchangecolor()
    {

        Rpcchangecolor();
    }

    [ClientRpc]
    void Rpcchangecolor()
    {
        switch (playercolor)
        {
            case 1:
                this.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case 2:
                this.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case 3:
                this.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 4:
                this.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            default:
                break;

        }

    }
    [Command]
    void Cmdchangeimmuematerial()
    {

        Rpcchangeimmuematerial();

    }
    [ClientRpc]
    void Rpcchangeimmuematerial()
    {
        this.GetComponent<Renderer>().material.color = Color.black;
    }

    [Command]
    void Cmdchangematerial()
    {

        Rpcchangematerial();
    }

    [ClientRpc]
    void Rpcchangematerial()
    {
       resettheplayercolor();
        item = 0;
    }

    [Command]
    void Cmdchangescore()
    {
        floatscore += Time.deltaTime;
        score = (int)Mathf.Round(floatscore);
    }

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 7;

        NetworkServer.Spawn(bullet);
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
    }

    public override void OnStartLocalPlayer()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
    //[Command]
    public void Cmdimmuestatetrigger()
    {


        item = 1;
    }

}
