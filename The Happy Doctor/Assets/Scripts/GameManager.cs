using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int saved = 0;
    int living = 0;
    int dead = 0;

    [SerializeField]
    public List<GameObject> list;

    int orphens;

    // Start is called before the first frame update
    void Start()
    {
        orphens = 0;
        list = new List<GameObject>();
        list.AddRange(GameObject.FindGameObjectsWithTag("Victim"));
        living = list.Count;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in list)
        {
            Victim vic = obj.GetComponent<Victim>();
            switch (vic.state) {
                case VicState.DEAD:
                    orphens += vic.children;
                    list.Remove(obj);
                    living -= 1;
                    dead += 1;
                    break;

                case VicState.SAVED:
                    list.Remove(obj);
                    living -= 1;
                    saved += 1;
                    break;

                default:
                    break;
            }
        }
    }
}
