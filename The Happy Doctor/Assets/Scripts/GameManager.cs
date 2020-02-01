using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int saved = 0;
    int living = 0;
    int dead = 0;

    [SerializeField]
    public List<Victim> list;

    // Start is called before the first frame update
    void Start()
    {
        list = new List<Victim>();
        list.AddRange(FindObjectsOfType<Victim>());
        living = list.Count;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Victim vic in list)
        {
            switch (vic.state) {
                case VicState.DEAD:
                    list.Remove(vic);
                    living -= 1;
                    dead += 1;
                    break;

                case VicState.SAVED:
                    list.Remove(vic);
                    living -= 1;
                    saved += 1;
                    break;

                default:
                    break;
            }
        }
    }
}
