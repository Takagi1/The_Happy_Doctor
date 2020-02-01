using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    public enum LimbType{
        LEFTARM,
        LEFTLEG,
        RIGHTARM,
        RIGHTLEG
    }

    public enum InjuryClass
    {
        FINE = 0,
        INJURED,
        LOST
    }

    public InjuryClass state;
    public LimbType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
