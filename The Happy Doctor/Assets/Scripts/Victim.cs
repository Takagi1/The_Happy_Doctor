using System.Collections;
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

    public bool menuVisible;

    public VicState state;

    //Make const?
    public int children;
    public bool isMurderer;

    public Image healthBar;

    [Header("Limbs")]
    public Limb leftArm;
    public Limb leftLeg;
    public Limb rightArm;
    public Limb rightLeg;

    [Header("Images Settings")]
    public Image leftArmBG;
    public Image leftLegBG;
    public Image rightArmBG;
    public Image rightLegBG;


    [Header("Text Settings")]
    public Text leftArmTxt;
    public Text leftLegTxt;
    public Text rightArmTxt;
    public Text rightLegTxt;

    [SerializeField] float lostDamage = 0.3f;
    [SerializeField] float injuredDamage = 0.1f;

    private void Awake()
    {
        //Set health to max
        healthMax = 1000;
        health = healthMax;
        children = Random.Range(0, 3);
        if (Random.Range(0, 1) == 1) { isMurderer = true; }
        else { isMurderer = false; }
        leftArmTxt.text = "Left Arm " + leftArm.GetClass();
        leftLegTxt.text = "Left Leg " + leftLeg.GetClass();
        rightArmTxt.text = "Right Arm " + rightArm.GetClass();
        rightLegTxt.text = "Right Leg " + rightLeg.GetClass();

        leftArmTxt.transform.parent.gameObject.SetActive(false);
        leftLegTxt.transform.parent.gameObject.SetActive(false);
        rightArmTxt.transform.parent.gameObject.SetActive(false);
        rightLegTxt.transform.parent.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
      
        //TEMP
        leftArmTxt.text = "Left Arm " + leftArm.GetClass();
        if(leftArm.state == Limb.InjuryClass.FINE) { leftArmTxt.color = Color.green; }
        else if (leftArm.state == Limb.InjuryClass.INJURED) { leftArmTxt.color = Color.yellow; }
        else if (leftArm.state == Limb.InjuryClass.LOST) { leftArmTxt.color = Color.red; }

        leftLegTxt.text = "Left Leg " + leftLeg.GetClass();
        if (leftLeg.state == Limb.InjuryClass.FINE) { leftLegTxt.color = Color.green; }
        else if (leftLeg.state == Limb.InjuryClass.INJURED) { leftLegTxt.color = Color.yellow; }
        else if (leftLeg.state == Limb.InjuryClass.LOST) { leftLegTxt.color = Color.red; }

        rightArmTxt.text = "Right Arm " + rightArm.GetClass();
        if (rightArm.state == Limb.InjuryClass.FINE) { rightArmTxt.color = Color.green; }
        else if (rightArm.state == Limb.InjuryClass.INJURED) { rightArmTxt.color = Color.yellow; }
        else if (rightArm.state == Limb.InjuryClass.LOST) { rightArmTxt.color = Color.red; }

        rightLegTxt.text = "Right Leg " + rightLeg.GetClass();
        if (rightLeg.state == Limb.InjuryClass.FINE) { rightLegTxt.color = Color.green; }
        else if (rightLeg.state == Limb.InjuryClass.INJURED) { rightLegTxt.color = Color.yellow; }
        else if (rightLeg.state == Limb.InjuryClass.LOST) { rightLegTxt.color = Color.red; }


        if (leftArm.state == Limb.InjuryClass.INJURED) { health -= injuredDamage; }
        else if (leftArm.state == Limb.InjuryClass.LOST) { health -= lostDamage; }

        if (leftLeg.state == Limb.InjuryClass.INJURED) { health -= injuredDamage; }
        else if (leftLeg.state == Limb.InjuryClass.LOST) { health -= lostDamage; }

        if (rightArm.state == Limb.InjuryClass.INJURED) { health -= injuredDamage; }
        else if (rightArm.state == Limb.InjuryClass.LOST) { health -= lostDamage; }

        if (rightLeg.state == Limb.InjuryClass.INJURED) { health -= injuredDamage; }
        else if (rightLeg.state == Limb.InjuryClass.LOST) { health -= lostDamage; }

        healthBar.fillAmount = health / healthMax;

        //Check for death
        if (health <= 0) { state = VicState.DEAD; }

        //Fix for heal issue
        if (leftArm.state == Limb.InjuryClass.FINE &&
            leftLeg.state == Limb.InjuryClass.FINE&&
            rightArm.state == Limb.InjuryClass.FINE &&
            rightLeg.state == Limb.InjuryClass.FINE)
        {
            state = VicState.SAVED;// redundancy issue between saved and living was stopping people from being saved.
        }

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
            leftArm.hasChanged = true;
        }
        else if (type == Limb.LimbType.LEFTLEG)
        {
            leftLeg.state = Limb.InjuryClass.FINE;
            leftLeg.hasChanged = true;
        }
        else if (type == Limb.LimbType.RIGHTARM)
        {
            rightArm.state = Limb.InjuryClass.FINE;
            rightArm.hasChanged = true;
        }
        else if (type == Limb.LimbType.RIGHTLEG)
        {
            rightLeg.state = Limb.InjuryClass.FINE;
            rightLeg.hasChanged = true;
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
            if(leftArm.state == Limb.InjuryClass.LOST)
            {
                return false;
            }
            else { return true; }
        }
        else if (type == Limb.LimbType.LEFTLEG)
        {
            if (leftLeg.state == Limb.InjuryClass.LOST)
            {
                return false;
            }
            else { return true; }
        }
        else if (type == Limb.LimbType.RIGHTARM)
        {
            if (rightArm.state == Limb.InjuryClass.LOST)
            {
                return false;
            } else { return true; }
        }
        else if (type == Limb.LimbType.RIGHTLEG)
        {
            if (rightLeg.state == Limb.InjuryClass.LOST)
            {
                return false;
            } else { return true; }
        }
        return true;
    }

    public void TurnOnMenu()
    {
        leftArmBG.gameObject.SetActive(true);
        leftLegBG.gameObject.SetActive(true);
        rightArmBG.gameObject.SetActive(true);
        rightLegBG.gameObject.SetActive(true);
    }

    public void TurnOffMenu()
    {
        leftArmBG.gameObject.SetActive(false);
        leftLegBG.gameObject.SetActive(false);
        rightArmBG.gameObject.SetActive(false);
        rightLegBG.gameObject.SetActive(false); ;
    }

}
