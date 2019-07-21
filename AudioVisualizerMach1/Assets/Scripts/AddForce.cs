using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000f);
        }
    }
}
