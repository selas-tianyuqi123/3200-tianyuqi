using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //Game over --Ult
    public UIGameOver GameOver;
    //Gold number--UI
    public Text UIGoldCount;
    public Transform UIHp;
    //Alert about covid-19
    public Text UIMsg;
    //different situation about text（different alert about covid-19）
    public enum MsgType
    {
        NPC,
        HOME
    }
    //Current gold number--in the top of screen
    private int _goldCount;
    //Lesson text and alert text，which is information about covid-19 and it‘s using |" +  to separate each message 
    private string msg =
        "keep 1.5 metres away from others wherever possible|" +
        "avoid physical greetings such as handshaking, hugs and kisses|" +
        "avoid crowds if you see a crowded space do not enter|" +
        "avoid large public gatherings|" +
        //4
        "stop shaking hands to greet others|" +
        "avoid non-essential meetings.If needed, hold meetings via video conferencing or phone call|" +
        "put off large meetings to a later date|" +
        "hold essential meetings outside in the open air if possible|" +
        "promote good hand, sneeze and cough hygiene|" +
        "provide alcohol-based hand rub for all staff|" +
        "eat lunch at your desk or outside rather than in the lunch room|" +
        "regularly clean and disinfect surfaces that many people touch|" +
        "open windows or adjust air conditioning for more ventilation|" +
        "limit food handling and sharing of food in the workplace|" +
        "avoid non-essential travel|" +
        "promote strict hygiene among food preparation (canteen) staff and their close contacts|" +
        //16
        "care for the sick person in a single room, if possible|" +
        "keep the number of carers to a minimum|" +
        "keep the door to the sick person’s room closed.If possible, keep a window open|" +
        "wear a surgical mask when you are in the same room as the sick person.The sick person should also wear a mask when other people are in the same room";
    //To library the message 
    public string[] msgs;
    //The singleton pattern ensures that there is only one instance of a class, and it instantiates itself and provides that instance to the entire system
    //A static variable_instance
    private static UIManager _instance;
    //Public static variable, return _instance value when Get
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    
	void Awake () {
        //using '|' to separate all message
        msgs = msg.Split('|');
        //Point _instance to itself, and when other classes call it, it will point to that class
        _instance = this;
        HideMsg();
        Refresh();
    }

    public void Refresh()
    {
        //Get the number of gold form playerprefs
        _goldCount = PlayerPrefs.GetInt("gold", 0);
        AddGold(0);
        AddHp(0);
    }

    //The way of counting gold
    public void AddGold(int i)
    {
        _goldCount += i;
        UIGoldCount.text = _goldCount.ToString();
    }

    /// <summary>
    /// refreshing HP
    /// </summary>
    /// <param name="hp"></param>
    public void AddHp(int hp)
    {
        AIManager.Instance.hpCount += hp;
        //when HP<0, game is over
        if (AIManager.Instance.hpCount <= 0)
        {
            ShowGameOver("GAME OVER");
        }
        //when updating the ui what ever value, five hp hearts will re
        for (int i = 0; i < UIHp.childCount; i++)
        {
            //if i < HP， so it‘s active if not， hide this hearts.
            UIHp.GetChild(i).gameObject.SetActive(i < AIManager.Instance.hpCount ? true : false);
        }
    }

    //The screen will show the message, and base on the situation gives one of message from the library.
    public void ShowMsg(MsgType s)
    {
        switch (s)
        {
            case MsgType.HOME:
                int index = Random.Range(16,msgs.Length);
                UIMsg.text = msgs[index];
                break;
            case MsgType.NPC:
                int index2 = Random.Range(0, 16);
                UIMsg.text = msgs[index2];
                break;
            default:
                break;
        }
        UIMsg.gameObject.SetActive(true);
    }
    //Hide message 
    public void HideMsg()
    {
        UIMsg.gameObject.SetActive(false);
    }
    //Game over, the title will show, in the screen.
    public void ShowGameOver(string title)
    {
        //The game speed is set to 0 ， and it's main to shutdown the game
        Time.timeScale = 0;
        GameOver.SetTitle(title);
        GameOver.gameObject.SetActive(true);
    }
    //Save the Data of gold.
    public void SaveData()
    {
        PlayerPrefs.SetInt("gold", _goldCount);
    }
    //Reset the numebr of gold.
    public void Quit()
    {
        PlayerPrefs.SetInt("gold", 0);
    }

}
