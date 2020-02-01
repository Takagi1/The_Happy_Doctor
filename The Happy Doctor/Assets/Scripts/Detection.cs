using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    BoxCollider2D box;
    Victim victim;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        victim = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        victim = collision.collider.GetComponent<Victim>();
    }

    Victim GetVictim()
    { 
        return victim;
    }

}
