using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutLineWall : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }
    }
/*    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
*/
}
