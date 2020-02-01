﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum VicState {
    LIVING,
    DEAD,
    SAVED
}

public class Victim : MonoBehaviour
{

    private float healthMax;

    [SerializeField]
    private float health;

    public Limb leftArm;
    public Limb leftLeg;
    public Limb rightArm;
    public Limb rightLeg;

    public VicState state;

    public int children;
    public bool isMurderer;

    public Image healthBar;
    public Text leftArmTxt;
    public Text leftLegTxt;
    public Text rightArmTxt;
    public Text rightLegTxt;

    public bool menuVisible;

    private void Awake()
    {

        healthMax = 1000;
        //Set health to max
        health = healthMax;
        children = Random.Range(0, 3);
        if(Random.Range(0,1) == 1) { isMurderer = true; }
        else { isMurderer = false; }
        leftArmTxt.text = "Left Arm " + leftArm.GetClass();
        leftLegTxt.text = "Left Leg " + leftLeg.GetClass();
        rightArmTxt.text = "Right Arm " + rightArm.GetClass();
        rightLegTxt.text = "Right Leg " + rightLeg.GetClass();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        //TEMP
        leftArmTxt.text = "Left Arm " + leftArm.GetClass();
        leftLegTxt.text = "Left Leg " + leftLeg.GetClass();
        rightArmTxt.text = "Right Arm " + rightArm.GetClass();
        rightLegTxt.text = "Right Leg " + rightLeg.GetClass();

        if (leftArm.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (leftArm.state == Limb.InjuryClass.LOST) { health -= 3; }

        if (leftLeg.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (leftLeg.state == Limb.InjuryClass.LOST) { health -= 3; }

        if (rightArm.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (rightArm.state == Limb.InjuryClass.LOST) { health -= 3; }

        if (rightLeg.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (rightLeg.state == Limb.InjuryClass.LOST) { health -= 3; }

        healthBar.fillAmount = health / healthMax;

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

    public void TurnOnMenu()
    {
        leftArmTxt.enabled = true;
        leftLegTxt.enabled = true;
        rightArmTxt.enabled = true;
        rightLegTxt.enabled = true;
    }

    public void TurnOffMenu()
    {
        leftArmTxt.enabled = false;
        leftLegTxt.enabled = false;
        rightArmTxt.enabled = false;
        rightLegTxt.enabled = false;
    }
    
}
