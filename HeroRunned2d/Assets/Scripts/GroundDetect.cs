using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    public Ninja parent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit terrain");
        parent.SetOnGround(true);
    }






}
