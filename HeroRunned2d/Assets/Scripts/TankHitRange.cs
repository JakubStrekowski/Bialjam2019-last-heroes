using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHitRange : MonoBehaviour
{
    public Tank parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DestructibleWall")
        {
            parent.SetWall(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "DestructibleWall")
        {
            parent.SetWall(null);
        }
    }


}
