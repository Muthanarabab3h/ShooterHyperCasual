using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralPlayer : MonoBehaviour
{
    public bool notAssigned = false;
    public GameObject player;

    public int health = 1;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {

        if(GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Gamemanager>().doubleCharacter == false)
        {
        
            if(other.gameObject.tag == "Main Player")
            {
                
                if(!notAssigned)
                {
                    GameObject playerInitiated = Instantiate(player, transform.position, transform.rotation);
                    
                    GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated.GetComponent<Unit>());
                    GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
                    // gameObject.tag = "Player";
                    // gameObject.GetComponent<BoxCollider>().isTrigger = false;
                    // gameObject.GetComponent<BoxCollider>().size = new Vector3(2f, 1f, 2f);
                    // gameObject.GetComponent<Player>().enabled = true;
                    // gameObject.GetComponent<NeutralPlayer>().enabled = false;
                    // gameObject.GetComponent<PlayerSupport>().enabled = true;
                    // notAssigned = true;
                    Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(gameObject);

                    notAssigned = true;
                }
                
            }

        }
        else
        {
            if(!notAssigned)
                {
                    GameObject playerInitiated1 = Instantiate(player, transform.position, transform.rotation);
                    GameObject playerInitiated2 = Instantiate(player, transform.position, transform.rotation);
                    Debug.Log("Instantitate");
                    
                    GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated1.GetComponent<Unit>());
                    GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated2.GetComponent<Unit>());
                    GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
                    GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
                    // gameObject.tag = "Player";
                    // gameObject.GetComponent<BoxCollider>().isTrigger = false;
                    // gameObject.GetComponent<BoxCollider>().size = new Vector3(2f, 1f, 2f);
                    // gameObject.GetComponent<Player>().enabled = true;
                    // gameObject.GetComponent<NeutralPlayer>().enabled = false;
                    // gameObject.GetComponent<PlayerSupport>().enabled = true;
                    // notAssigned = true;
                    Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(gameObject);

                    notAssigned = true;
                }
                
            
        }
    }

    
}
