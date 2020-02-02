using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Animations;

public class NewPlayerz : MonoBehaviour
{
    //config
    [SerializeField] float runSpeed = 2f;
    bool bagFull = false;

    public bool facingRight = true;
    Limb.LimbType content;

    int boxTime;
    int resetBoxTime;

    public bool needMeds = false;

    //cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    //MENU INTERACT
    public bool menuInteract = false;
    Victim victim;
    private int menuLoc = 0;

    Vector3 sizeSave;

    public bool bagGrab = false;

    //messege then methods
    void Start()
    {
        menuInteract = false;
        victim = null;

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        resetBoxTime = 30;
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
                menuInteract = false;
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

        }
        if (Input.GetButtonDown("Interact") && menuInteract == false)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }

        if (GetComponent<BoxCollider2D>().enabled == true && boxTime <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            boxTime = resetBoxTime;
        }
        else if(GetComponent<BoxCollider2D>().enabled == true)
        {
            boxTime -= 1;
        }

        if (bagGrab)
        {
            myAnimator.SetBool("Grabbing stuff", false);
            print("Test");

            bagGrab = false;
        }
        if (needMeds)
        {
            myAnimator.SetBool("NeedsMeds", false);
            needMeds = false;
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
    }

    private void PillPopping() 
    {
            if(bagFull == true) 
        {
            myAnimator.SetBool("NeedsMeds", bagFull);
        }
    }

    //System: when this triggers set menu to true and go until the player does exits or performs a action
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject)
        {
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
            //TODO Peter: Player already has a limb in the bag
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



        menuInteract = false;
        CloseMenu();
        
        bagFull = true;
        content = type;
        victim.RemoveLimb(type);
        victim = null;
    }

    void GiveLimb()
    {
        if(!bagFull)
        {
            //NO Limb
            return;
        }
        else if (victim.HasLimb(content) == true)
        {
            //TODO Peter: Victim already has that limb
            return;
        }

        //TODO: Perfrom give limb animation
        bool playerIsInMenu = menuInteract;//activates limb grabber
        myAnimator.SetBool("Grabbing stuff", playerIsInMenu);
        bool playerIsNotInMenu = !menuInteract;//reactivate animations
        myAnimator.SetBool("idle", playerIsNotInMenu);
        
        bagFull = false;
        menuInteract = false;
        CloseMenu();
        victim.GiveLimb(content);
        victim = null;
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

    void CloseMenu()
    {
        if (menuLoc == 0) { victim.leftArmBG.color = Color.white; }
        else if (menuLoc == 1) { victim.leftLegBG.color = Color.white; }
        else if (menuLoc == 2) { victim.rightArmBG.color = Color.white; }
        else if (menuLoc == 3) { victim.rightLegBG.color = Color.white; }
        victim.TurnOffMenu();
        menuLoc = 0;
    }
}
