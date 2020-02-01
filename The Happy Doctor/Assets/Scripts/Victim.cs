using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VicState {
    LIVING,
    DEAD,
    SAVED
}

public class Victim : MonoBehaviour
{
    [SerializeField]
    private float health;

    public Limb leftArm;
    public Limb leftLeg;
    public Limb rightArm;
    public Limb rightLeg;

    public VicState state;

    private void Awake()
    {
        //Set health to max
        health = 1000;
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        int check = 0;
        if (leftArm.state == Limb.InjuryClass.FINE) { check += 1; }
        else if (leftArm.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (leftArm.state == Limb.InjuryClass.LOST) { health -= 3; }

        if (leftLeg.state == Limb.InjuryClass.FINE) { check += 1; }
        else if (leftLeg.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (leftLeg.state == Limb.InjuryClass.LOST) { health -= 3; }

        if (rightArm.state == Limb.InjuryClass.FINE) { check += 1; }
        else if (rightArm.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (rightArm.state == Limb.InjuryClass.LOST) { health -= 3; }

        if (rightLeg.state == Limb.InjuryClass.FINE) { check += 1; }
        else if (rightLeg.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (rightLeg.state == Limb.InjuryClass.LOST) { health -= 3; }

        //Check for save
        if (check == 4) { state = VicState.SAVED; }

        //Check for death
        if (health <= 0) { state = VicState.DEAD; }
    }

    public void RemoveLimb(Limb.LimbType type)
    {
        //Damage for taking it
        health -= 10;


        if (type == Limb.LimbType.LEFTARM)
        {
            leftArm.state = Limb.InjuryClass.LOST;
        }
        else if (type == Limb.LimbType.LEFTLEG)
        {
            leftLeg.state = Limb.InjuryClass.LOST;
        }
        else if (type == Limb.LimbType.RIGHTARM)
        {
            rightArm.state = Limb.InjuryClass.LOST;
        }
        else if (type == Limb.LimbType.RIGHTLEG)
        {
            rightLeg.state = Limb.InjuryClass.LOST;
        }

        //If all limbs removed kill victim
        if (leftArm.state == Limb.InjuryClass.LOST &&
            leftLeg.state == Limb.InjuryClass.LOST &&
            rightArm.state == Limb.InjuryClass.LOST &&
            rightLeg.state == Limb.InjuryClass.LOST)
        {
            state = VicState.DEAD;
        } 
        

    }

    public void GiveLimb(Limb.LimbType type)
    {
        if (type == Limb.LimbType.LEFTARM)
        {
            leftArm.state = Limb.InjuryClass.FINE;
        }
        else if (type == Limb.LimbType.LEFTLEG)
        {
            leftLeg.state = Limb.InjuryClass.FINE;
        }
        else if (type == Limb.LimbType.RIGHTARM)
        {
            rightArm.state = Limb.InjuryClass.FINE;
        }
        else if (type == Limb.LimbType.RIGHTLEG)
        {
            rightLeg.state = Limb.InjuryClass.FINE;
        }

        //If all limbs removed kill victim
        if (leftArm.state == Limb.InjuryClass.FINE &&
            leftLeg.state == Limb.InjuryClass.FINE &&
            rightArm.state == Limb.InjuryClass.FINE &&
            rightLeg.state == Limb.InjuryClass.FINE)
        {
            state = VicState.SAVED;
        }
    }
    public bool HasLimb(Limb.LimbType type)
    {
        if(type == Limb.LimbType.LEFTARM)
        {
            if(leftArm.state != Limb.InjuryClass.LOST)
            {
                return true;
            }
        }
        else if (type == Limb.LimbType.LEFTLEG)
        {
            if (leftLeg.state != Limb.InjuryClass.LOST)
            {
                return true;
            }
        }
        else if (type == Limb.LimbType.RIGHTARM)
        {
            if (rightArm.state != Limb.InjuryClass.LOST)
            {
                return true;
            }
        }
        else if (type == Limb.LimbType.RIGHTLEG)
        {
            if (rightLeg.state != Limb.InjuryClass.LOST)
            {
                return true;
            }
        }
        return true;
    }
    
}
