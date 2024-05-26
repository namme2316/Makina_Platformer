using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabula : MonoBehaviour
{
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
  
    public void EnableCollider()
    {
        col.enabled = true;
    }

    public void DisableCollider() 
    {
        col.enabled = false;
    }
}
