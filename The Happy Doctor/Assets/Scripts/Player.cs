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

    int boxTime;
    int resetBoxTime;

    //MENU INTERACT
    public bool menuInteract;
    Victim victim;
    private int menuLoc = 0;

    Vector3 sizeSave;

    // Start is called before the first frame update
    void Start()
    {
        menuInteract = false;
        victim = null;
        body = GetComponent<Rigidbody2D>();
        resetBoxTime = 2;
        boxTime = resetBoxTime;

        sizeSave = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        if(menuInteract)
        {
           
            if (Input.GetAxis("Horizontal") != 0) {
                victim.TurnOffMenu();
                menuInteract = false;
                victim = null;
            }
            float vert = 0;

            if (Input.GetButtonDown("Vertical") && Input.GetButtonDown("Vertical"))
            {
                MoveMenu(1);
            }
            else if (Input.GetButtonDown("down"))
            {
                MoveMenu(-1);
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
                transform.localScale = sizeSave;
                facingRight = true;
            }

            else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector3(-sizeSave.x, sizeSave.y, sizeSave.z);
                facingRight = false;
            }

            if (Input.GetButtonDown("Interact"))
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        if (GetComponent<BoxCollider2D>().enabled && boxTime == 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            boxTime = resetBoxTime;
        }
        else
        {
            boxTime -= 1;
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
            victim.TurnOnMenu();
            StartMenu();
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
            //TODO: Victim already has that limb
            return;
        }

        victim.GiveLimb(content);
    }


    void StartMenu()
    {
        victim.leftArmBG.color = Color.blue;
    }

    void MoveMenu(int val)
    {
        if(menuLoc < 0) { return; }
        if(menuLoc > 3) { return; }

        if (menuLoc == 0) { victim.leftArmBG.color = Color.white; }
        else if(menuLoc == 1){ victim.leftLegBG.color = Color.white; }
        else if(menuLoc == 2){ victim.rightArmBG.color = Color.white; }
        else if(menuLoc == 3){ victim.rightLegBG.color = Color.white; }

        menuLoc += val;

        if (menuLoc == 0){ victim.leftArmBG.color = Color.blue; }
        else if (menuLoc == 1){ victim.leftLegBG.color = Color.blue; }
        else if (menuLoc == 2){ victim.rightArmBG.color = Color.blue; }
        else if (menuLoc == 3){ victim.rightLegBG.color = Color.blue; }
    }

    //TODO: Close Menu
    void CloseMenu()
    {

        //Remove colour here

        menuLoc = 0;
    }
}
