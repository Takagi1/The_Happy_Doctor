using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class NewPlayerz : MonoBehaviour
{
    //config
    [SerializeField] float runSpeed = 2f;
    bool bagFull = false;

    public bool facingRight = true;
    Limb.LimbType content;

    int boxTime;
    int resetBoxTime;
    //state
    bool isAlive = true;


    //cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    //MENU INTERACT
    public bool menuInteract;
    Victim victim;
    private int menuLoc = 0;

    Vector3 sizeSave;

    //messege then methods
    void Start()
    {
        menuInteract = false;
        victim = null;

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        resetBoxTime = 2;
        boxTime = resetBoxTime;
        sizeSave = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (menuInteract)
        {

            if (Input.GetAxis("Horizontal") != 0)
            {
                CloseMenu();
                victim = null;
            }
            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
            {
                MoveMenu(-1);
            }
            else if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
            {
                MoveMenu(1);
            }
            if (Input.GetButtonDown("Interact"))
            {     
                if (menuLoc == 0)
                {
                    if(victim.leftArm.state == Limb.InjuryClass.FINE)
                    {
                        GrabLimb();
                    }
                    else if(victim.leftArm.state == Limb.InjuryClass.INJURED)
                    {
                        victim.leftArm.state = Limb.InjuryClass.FINE;
                    }
                    else if(victim.leftArm.state == Limb.InjuryClass.LOST)
                    {
                        GiveLimb();
                    }
                }
                else if (menuLoc == 1)
                {
                    if (victim.leftLeg.state == Limb.InjuryClass.FINE)
                    {
                        GrabLimb();
                    }
                    else if (victim.leftLeg.state == Limb.InjuryClass.INJURED)
                    {
                        victim.leftLeg.state = Limb.InjuryClass.FINE;
                    }
                    else if (victim.leftLeg.state == Limb.InjuryClass.LOST)
                    {
                        GiveLimb();
                    }
                }
                else if (menuLoc == 2)
                {
                    if (victim.rightArm.state == Limb.InjuryClass.FINE)
                    {
                        GrabLimb();
                    }
                    else if (victim.rightArm.state == Limb.InjuryClass.INJURED)
                    {
                        victim.rightArm.state = Limb.InjuryClass.FINE;
                    }
                    else if (victim.rightArm.state == Limb.InjuryClass.LOST)
                    {
                        GiveLimb();
                    }
                }
                else if (menuLoc == 3)
                {
                    if (victim.rightLeg.state == Limb.InjuryClass.FINE)
                    {
                        GrabLimb();
                    }
                    else if (victim.rightLeg.state == Limb.InjuryClass.INJURED)
                    {
                        victim.rightLeg.state = Limb.InjuryClass.FINE;
                    }
                    else if (victim.rightLeg.state == Limb.InjuryClass.LOST)
                    {
                        GiveLimb();
                    }
                }
            }

            //Action exit

        }
        else
        {
            Run();
            if (Input.GetButtonDown("Interact"))
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        if (GetComponent<BoxCollider2D>().isActiveAndEnabled && boxTime == 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            boxTime = resetBoxTime;
        }
        else
        {
            boxTime -= 1;
        }

    }
    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");// value is between -1 and +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        if(controlThrow > 0)
        {
            transform.localScale = new Vector3(sizeSave.x, sizeSave.y, sizeSave.z);
            facingRight = true;
        }
        else if(controlThrow < 0)
        {
            transform.localScale = new Vector3(-sizeSave.x, sizeSave.y, sizeSave.z);
            facingRight = false;
        }

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("idle", playerHasHorizontalSpeed);//we will change this to running if we have time or want to expand on the speed (like leveling or something)
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
    void GrabLimb()
    {
        Limb.LimbType type = Limb.LimbType.LEFTARM;
        if (menuLoc == 1) { type = Limb.LimbType.LEFTLEG; }
        else if (menuLoc == 2) { type = Limb.LimbType.RIGHTARM; }
        else if (menuLoc == 3) { type = Limb.LimbType.RIGHTLEG; }
        if (bagFull)
        {
            //TODO: Player already has a limb in the bag
            return;
        }
        else if (menuLoc == 0 && victim.leftArm.state == Limb.InjuryClass.LOST)
        {
            //No Limb
        }
        else if (menuLoc == 1 && victim.leftLeg.state == Limb.InjuryClass.LOST)
        {
            //No Limb
        }
        else if (menuLoc == 2 && victim.rightArm.state == Limb.InjuryClass.LOST)
        {
            //No Limb
        }
        else if (menuLoc == 3 && victim.rightLeg.state == Limb.InjuryClass.LOST)
        {
            //No Limb
        }

        //TODO: Perfrom grab limb animation
        victim.RemoveLimb(type);
        content = type;
    }

    void GiveLimb()
    {
        if(!bagFull)
        {
            //NO Limb
            return;
        }
        else if (victim.HasLimb(content))
        {
            //TODO: Victim already has that limb
            return;
        }

        //TODO: Perfrom give limb animation
        victim.GiveLimb(content);
    }


    void StartMenu()
    {
        victim.leftArmBG.color = Color.blue;
    }

    void MoveMenu(int val)
    {

        if (menuLoc == 0) { victim.leftArmBG.color = Color.white; }
        else if (menuLoc == 1) { victim.leftLegBG.color = Color.white; }
        else if (menuLoc == 2) { victim.rightArmBG.color = Color.white; }
        else if (menuLoc == 3) { victim.rightLegBG.color = Color.white; }

        menuLoc += val;
        if (menuLoc < 0) { menuLoc = 0; }
        if (menuLoc > 3) { menuLoc = 3; }

        if (menuLoc == 0) { victim.leftArmBG.color = Color.blue; }
        else if (menuLoc == 1) { victim.leftLegBG.color = Color.blue; }
        else if (menuLoc == 2) { victim.rightArmBG.color = Color.blue; }
        else if (menuLoc == 3) { victim.rightLegBG.color = Color.blue; }
    }

    //TODO: Close Menu
    void CloseMenu()
    {
        //Remove colour here
        victim.TurnOffMenu();
        menuLoc = 0;
    }
}
