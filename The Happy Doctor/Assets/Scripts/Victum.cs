using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victum : MonoBehaviour
{

    [SerializeField]
    public float health;
    Limb leftArm;
    public Limb leftLeg;
    public Limb rightArm;
    public Limb rightLeg;

    //Limbs
    private void Awake()
    {
        leftArm = GetComponentInChildren
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set health to max
        health = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
