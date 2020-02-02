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

    private SpriteRenderer renderer;

    [Header("Sprites")]
    public Sprite fine;
    public Sprite hurt;
    public Sprite lost;
    public Sprite fix;

    public bool hasChanged = false;

    public InjuryClass state;
    public LimbType type;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(state == InjuryClass.FINE)
        {
            renderer.sprite = fine;
        }
        else if(state == InjuryClass.INJURED)
        {
            renderer.sprite = hurt;
        }
        else if (state == InjuryClass.LOST)
        {
            renderer.sprite = lost;
        }
        else if (state == InjuryClass.FINE && hasChanged)
        {
            renderer.sprite = fix;
        }

    }

    public string GetClass()
    {
        if (state == InjuryClass.FINE) { return "FINE"; }
        else if (state == InjuryClass.INJURED) { return "INJURED"; }
        else if (state == InjuryClass.LOST) { return "LOST"; }
        else { return null; }
    }
}
