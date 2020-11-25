using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour {

    //The unique home camera in map 
    public GameObject Home_Camera;

    private void Start()
    {
        Home_Camera.SetActive(false);
    }
    //when player enter the room
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Home alert will show on the screen link with library
            UIManager.Instance.ShowMsg(UIManager.MsgType.HOME);
            Home_Camera.SetActive(true);
        }
    }
    //when player exit the room and the camera stop.
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.HideMsg();
            Home_Camera.SetActive(false);
        }
    }
}
