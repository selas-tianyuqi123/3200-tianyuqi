using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour {

    //set a title
    public void SetTitle(string title)
    {
        transform.Find("Title").GetComponent<Text>().text = title;
    }
    //quit button set
	public void Quit()
    {
        UIManager.Instance.Quit();
        Application.Quit();
    }
    //Go Next button
    public void Next()
    {
        AIManager.Instance.Level++;
        UIManager.Instance.SaveData();
        SceneManager.LoadScene(0);
    }
}
