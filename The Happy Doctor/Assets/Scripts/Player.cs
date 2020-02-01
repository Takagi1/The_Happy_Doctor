using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    //Copy of the detection box
    [SerializeField]
    GameObject detection;

    public float speed;

    bool bagFull = false;

    public bool facingRight = true;
    Limb.LimbType content;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        detection = FindObjectOfType<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        if (input != 0)
        {
            transform.position += new Vector3(input * speed, 0, 0);
        }

        //Flipping Sprite
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            facingRight = true;
        }

        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            facingRight = false;
        }

        if (Input.GetButtonDown("Interact"))
        {
        }
    }

    void GrabLimb(Victim victim, Limb.LimbType type)
    {
        if (bagFull)
        {
            //TODO: Player already has a limb in the bag
            return;
        }
        victim.RemoveLimb(type);
        content = type;
    }

    void GiveLimb(Victim victim)
    {
        if (victim.HasLimb(content))
        {
            //Victim already has that limb
            return;
        }

        victim.GiveLimb(content);
    }
}
