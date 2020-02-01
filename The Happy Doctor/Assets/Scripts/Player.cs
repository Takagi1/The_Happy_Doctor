using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    public float speed;

    bool bagFull = false;

    public bool facingRight = true;
    Limb.LimbType content;
    Rigidbody2D body;
    Victim victim;

    // Start is called before the first frame update
    void Start()
    {
        victim = null;
        body = GetComponent<Rigidbody2D>();

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
            GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine(EnableBox(2));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print("Test");
        if (col.gameObject)
        {
            victim = col.gameObject.GetComponent<Victim>();
        }
    }
    IEnumerator EnableBox(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        victim = null;
        GetComponent<BoxCollider2D>().enabled = false;
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
