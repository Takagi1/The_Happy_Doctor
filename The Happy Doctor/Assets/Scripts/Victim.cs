using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Victim : MonoBehaviour
{

    [SerializeField]
    private float health;

    public Limb leftArm;
    public Limb leftLeg;
    public Limb rightArm;
    public Limb rightLeg;

    
    private void Awake()
    {
        //Set health to max
        health = 1000;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (leftArm.state == Limb.InjuryClass.INJURED){ health -= 1; }
        else if (leftArm.state == Limb.InjuryClass.LOST) { health -= 3; }

        if (leftLeg.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (leftLeg.state == Limb.InjuryClass.LOST) { health -= 3; }

        if (rightArm.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (rightArm.state == Limb.InjuryClass.LOST) { health -= 3; }

        if (rightLeg.state == Limb.InjuryClass.INJURED) { health -= 1; }
        else if (rightLeg.state == Limb.InjuryClass.LOST) { health -= 3; }

        if(health <= 0)
        {
            //TODO: Kill victim here
        }
    }
}
