using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    public float speed;

    bool bagFull = false;

    public bool facingRight = true;
    Limb.LimbType content;
    Rigidbody2D body;
    Victim victim;

    bool menuInteract = false;

    // Start is called before the first frame update
    void Start()
    {
        victim = null;
        body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<BoxCollider2D>().enabled)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if(menuInteract)
        {
            if (Input.GetAxis("Horizontal") != 0) {
                victim.TurnOffMenu();
                menuInteract = false;
                victim = null;
            }
            //Action exit
            
        }
        else
        {
            float input = Input.GetAxis("Horizontal");
            if (input != 0)
            {
                transform.position += new Vector3(input * speed, 0, 0);
            }

            //Flipping Sprite
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                facingRight = true;
            }

            else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                facingRight = false;
            }

            if (Input.GetButtonDown("Interact"))
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }


    //System: when this triggers set menu to true and go until the player does exits or performs a action
    void OnTriggerEnter2D(Collider2D col)
    {
        print("Test");
        if (col.gameObject)
        {
            print("Hit");
            victim = col.gameObject.GetComponent<Victim>();
            menuInteract = true;
        }
    }

    void GrabLimb(Victim victim, Limb.LimbType type)
    {
        if (bagFull)
        {
            //TODO: Player already has a limb in the bag
            return;
        }
        victim.RemoveLimb(type);
        content = type;
    }

    void GiveLimb(Victim victim)
    {
        if (victim.HasLimb(content))
        {
            //Victim already has that limb
            return;
        }

        victim.GiveLimb(content);
    }

    void PopMenu()
    {

    }
}
