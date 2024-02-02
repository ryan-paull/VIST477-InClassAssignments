using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;

public class CapsuleMovement : MonoBehaviour

{
    public Rigidbody myBody;
    public TMP_Text coinText;
    public GameObject allCoins;
    private float speed;
    private bool onGround;
    private int coinCounter;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
 
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        speed = 3.0f;
        onGround = false;
        coinCounter = 0;
        actions.Add("finish", Finish);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += Speech;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left shift"))
        {
            speed = 5.0f;
        }
        else
        {
            speed = 2.0f;
        }
        if (Input.GetKeyDown("space"))// boolean whether spacebar was pressed or not
        {
            if (onGround == true)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(myBody.velocity.x, 3, myBody.velocity.z);
            }
        }
        if (Input.GetKey("up"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(myBody.velocity.x, myBody.velocity.y, speed);
        }
        if (Input.GetKey("down"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(myBody.velocity.x, myBody.velocity.y, -speed);
        }
        if (Input.GetKey("right"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(speed, myBody.velocity.y, myBody.velocity.z);
        }
        if (Input.GetKey("left"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-speed, myBody.velocity.y, myBody.velocity.z);
        }
        if(transform.position.y < -5)
        {
            transform.position = new Vector3(0.1f, 1.35f, -4.38f);
            myBody.velocity = new Vector3(0, 0, 0);
        }
        coinText.SetText("Coins Collected = " + coinCounter.ToString());
        /*if(coinCounter == 5)
        {
            coinText.SetText("Game Finished");
        }*/
    }
    private void Speech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log("Keyword: " + speech.text);
        actions[speech.text].Invoke();
    }
    private void Finish()
    {
        if (coinCounter == 5)
        {
            coinText.SetText("Game Finished");
        }
        else
        {
            coinText.SetText("Game NOT Finished!!");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
        if(collision.gameObject.tag == "Coin")
        {
            coinCounter += 1;
        }
        if(collision.gameObject.tag == "Obstacle")
        {
            transform.position = new Vector3(0.1f, 1.35f, -4.38f);
            myBody.velocity = new Vector3(0, 0, 0);
            coinCounter = 0;
            foreach (Transform child in allCoins.transform)
            {
                GameObject coin = child.gameObject;
                coin.GetComponent<MeshRenderer>().enabled = true;
                coin.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }
}
