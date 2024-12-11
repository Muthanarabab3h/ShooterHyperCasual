using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject mainPlayer;
    public float distance;
    public float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        // mainPlayer = GameObject.FindGameObjectWithTag("Main Player");
        // GameObject[] slots = mainPlayer.GetComponent<MainPlayer>().slots;
        // for(int i =0; i < slots.Length; i++)
        // {
        //     if(slots[i].transform.childCount == 0)
        //     {
        //         gameObject.transform.parent = slots[i].transform;
        //         break;
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0f, 0f, 0f), speed*Time.deltaTime);
    }
}
