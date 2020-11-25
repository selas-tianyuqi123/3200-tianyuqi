using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The speed of main object
    public float speed = 1f;
    //the model in the map, like the door and gravity
    private Rigidbody rb;

    private void Awake()
    {
        AIManager.Instance.Next();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //The object move
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, transform.position.y, v) * speed / 10f;
        //make the word platform target to the setting range vector
        Vector4 target = this.transform.localToWorldMatrix * new Vector4(move.x, move.y, move.z, 1);
        target.y = transform.position.y;

        //Debug.Log((Vector3)target - transform.position);

        if (Mathf.Abs(h) >= 0.2 || Mathf.Abs(v) >= 0.2)
        {
            //rotation of objects
            Quaternion targetQ = Quaternion.LookRotation((Vector3)target - transform.position);
            transform.GetChild(0).rotation = Quaternion.RotateTowards(transform.GetChild(0).rotation, targetQ, 10f);
        }
        //through the gravity to move obejcts
        rb.MovePosition(target);
        //letting the force velocity be 0
        rb.velocity = Vector3.zero;

    }
    
    public void OnTriggerEnter(Collider other)
    {
        //Trigger the detection of gold 
        if (other.CompareTag("Gold"))
        {
            //play the audio 
            AudioManager.Instance.Play(AudioManager.Instance.sound_gold);
            //adding gold in the top
            UIManager.Instance.AddGold(1);
            //Delete the gold from AImanager
            AIManager.Instance.goldList.Remove(other.gameObject);
            //Delete the gold
            Destroy(other.gameObject);
            //If the gold return to the 0, the game is over
            if (AIManager.Instance.goldList.Count == 0)
            {
                UIManager.Instance.ShowGameOver("YOU WIN");
            }
        }
        //The system detect playr near "npc"
        if (other.CompareTag("Npc"))
        {
            UIManager.Instance.ShowMsg(UIManager.MsgType.NPC);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //The player away the "npc" in a setting range.
        if (other.CompareTag("Npc"))
        {
            UIManager.Instance.HideMsg();
        }
    }


}
