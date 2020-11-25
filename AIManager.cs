using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoSingleton<AIManager>
{
    public Transform golds;
    public NPC[] npcs;

    private List<GameObject> wayPoints = new List<GameObject>();

    public int Level = 0;
    [HideInInspector]
    public List<GameObject> goldList = new List<GameObject>();

    protected override void OnStart()
    {

    }
    //Go to next level
    public void Next()
    {
        RandomGold();
        RandomNPC();
    }
    /// <summary>
    /// random generate gold in map
    /// </summary>
    public void RandomGold()
    {
        if (golds == null)
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < golds.childCount; i++)
        {
            GameObject g = golds.GetChild(i).gameObject;
            wayPoints.Add(g);
        }

        Random.InitState((int)System.DateTime.Now.Ticks);

        for (int i = 0; i < 5 + Level; i++)
        {
            int index = Random.Range(0, wayPoints.Count);
            while (true)
            {
                if (wayPoints[index].activeSelf)
                {
                    index = Random.Range(0, wayPoints.Count);
                }
                else
                {
                    GameObject g = Instantiate(wayPoints[index], golds);
                    g.SetActive(true);
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
