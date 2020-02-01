using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InjuryClass
{
    FINE = 0,
    INJURED,
    LOST
}

public enum LimbType
{
    LEFTARM = 0,
    LEFTLEG,
    RIGHTARM,
    RIGHTLEG
}

public class Limb : MonoBehaviour
{
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
