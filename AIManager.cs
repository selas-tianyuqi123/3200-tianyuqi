using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoSingleton<AIManager>
{
    public Transform golds;
    public NPC[] npcs;

    private List<GameObject> wayPoints = new List<GameObject>();
    //Current HP in game
    public int hpCount = 5;
    public int Level = 0;
    [HideInInspector]
    public List<GameObject> goldList = new List<GameObject>();

    protected override void OnStart()
    {

    }
    //Since AIManager does not delete the game after it starts, the Start function is only executed once, so every time the game is started, the data is cleaned up and reset, and the gold UI is updated
    public void Start()
    {
        UIManager.Instance.Quit();
        UIManager.Instance.Refresh();
    }

    //Go to next level
    public void Next()
    {
        //The speed of game is 1
        Time.timeScale = 1f;
        hpCount = 5;
        RandomGold();
        RandomNPC();
    }
    /// <summary>
    /// random generate gold in map
    /// </summary>
    public void RandomGold()
    {
        //Determine if golds is full or not, delete this script
        if (golds == null)
        {
            Destroy(gameObject);
            return;
        }
        //going through all the children under golds,and add to the waypoint list, This list will hold all path points and generates golds.
        for (int i = 0; i < golds.childCount; i++)
        {
            GameObject g = golds.GetChild(i).gameObject;
            wayPoints.Add(g);
        }
        //setting random coins
        Random.InitState((int)System.DateTime.Now.Ticks);
        //leve+ 5coins for successful
        for (int i = 0; i < 5 + Level; i++)
        {   
            //random number
            int index = Random.Range(0, wayPoints.Count);
            //infinite random
            while (true)
            {
                //Determine if this point is active or not. Activation means it has been used. Re-random a number.
                if (wayPoints[index].activeSelf)
                {
                    index = Random.Range(0, wayPoints.Count);
                }
                else
                {
                    //create golds.
                    GameObject g = Instantiate(wayPoints[index], golds);
                    g.SetActive(true);
                    //adding to golds list.
                    goldList.Add(g);
                    break;
                }
            }
        }
    }
    /// <summary>
    /// random get one coordinate in map 
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPoint()
    {
        int index = Random.Range(0, wayPoints.Count);
        return wayPoints[index].transform.position;
    }
    /// <summary>
    /// Random gernate npc in map
    /// </summary>
    public void RandomNPC()
    {
        //Stores random coordinate points to npc_Count while ensuring that all coordinate points are different
        Hashtable hashtable = new Hashtable();
        int[] npc_Count = new int[2 + Level / 2];
        for (int i = 0; i < 2 + Level / 2; i++)
        {
            int index = Random.Range(0, wayPoints.Count);
            while (!hashtable.ContainsKey(index))
            {
                hashtable.Add(index, index);
                npc_Count[i] = index;
            }
        }
        //Get coordinate from the npc_Count, and create one npc in map
        foreach (var item in npc_Count)
        {
            GameObject npc = Instantiate(npcs[Random.Range(0, 2)].gameObject);
            npc.transform.position = wayPoints[item].transform.position;
            npc.SetActive(true);
        }
    }
    //When AIManager is uninstalled, it means that the game has been shut down and the data is reset
    public void OnDestroy()
    {
        if (AIManager.Instance == this)
        {
            UIManager.Instance.Quit();
        }
    }

}
