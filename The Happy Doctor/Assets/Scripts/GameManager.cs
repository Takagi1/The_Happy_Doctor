using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int saved = 0;
    int living = 0;
    int dead = 0;

    public Text livingTxt;
    public Text savedTxt;
    public Text deadTxt;

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
        
        livingTxt = GameObject.FindGameObjectWithTag("Living").GetComponent<Text>();
        savedTxt = GameObject.FindGameObjectWithTag("Saved").GetComponent<Text>();
        deadTxt = GameObject.FindGameObjectWithTag("Dead").GetComponent<Text>();

        livingTxt.text = "Living " + living;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(living == 0)
        {
            //TODO: End Game here / add end sequence        
            GameSceneManager.managerInstance.LoadNextScene();
            return;
        }
        if (living > 0)
        {
            foreach (GameObject obj in list)
            {
                GameObject game = obj;
                Victim vic = obj.GetComponent<Victim>();
                switch (vic.state)
                {

                    case VicState.DEAD:
                        orphens += vic.children;
                        list.Remove(obj);
                        Destroy(game);
                        living -= 1;
                        dead += 1;
                        deadTxt.text = "Dead " + dead;
                        livingTxt.text = "Living " + living;
                        break;

                    case VicState.SAVED:
                        list.Remove(obj);
                        Destroy(game);
                        living -= 1;
                        saved += 1;
                        savedTxt.text = "Saved " + saved;
                        livingTxt.text = "Living " + living;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
