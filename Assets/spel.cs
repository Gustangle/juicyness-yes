﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spel : MonoBehaviour
{
    public GameObject[] mulls;
    public Transform[] spawns;
    public bool[] full;
    public float timer;
    public GameObject nymull;
    public GameObject helst;
    public float range;
<<<<<<< HEAD
    public Camera Kamera;
    public int score;
=======
    public Camera camera;
>>>>>>> e13c43c03d89ae9eb734d87091ba3c89658517a8
    public int liv;
    public GameObject loseScreen;
    public float poängtime = 5;
    public bool gepoäng;
    public Text skortext;
    public AudioSource musik;
    public AudioSource ljud;

    public int score = 0;
    public int highScore = 0;
    string highScoreKey = "HighScore";

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);

        ljud = GetComponent<AudioSource>();
        helst = new GameObject();
        loseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        helst.transform.position += new Vector3(1, 1, 0) * Time.deltaTime;
        skortext.text = (string)score.ToString();
        timer += Time.deltaTime;
        if (timer > Random.Range(5, 50)) 
        {
            print("skapa mullvad");
            timer = 0;
            int nummer = Random.Range(0, spawns.Length);;
            nymull = Instantiate(mulls[Random.Range(0, mulls.Length)], spawns[nummer].position, Quaternion.identity);
            nymull.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            full[nummer] = true;
            gepoäng = true;
        }

       
        if (helst.transform.position.y > 10)
        {
            helst.transform.position = new Vector3(transform.position.x, 0, 0);
        }
        if (liv <= 0)
        {
            musik.Stop();
            ljud.PlayOneShot(ljud.clip);
            Time.timeScale = 0;
            print("haha du förlorade!?!?!?!?!?!?!?");
            loseScreen.SetActive(true);
        }
       
        helst.transform.position += new Vector3(1, 1, 0) * Time.deltaTime;
        if (Input.GetMouseButtonDown(0)) //Du klickar en knapp
        {
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            print(Vector3.Distance(nymull.transform.position, v3));
            for (int i = 0; i < spawns.Length; i++)
            {
                if (Vector3.Distance(nymull.transform.position, v3) < range)
                {
                    ljud.PlayOneShot(ljud.clip);
                    Destroy(nymull);
                    full[i] = false;
                    if (gepoäng == true)
                    {
                        if (poängtime > 3)
                        {
                            score += 5;
                            gepoäng = false;
                            poängtime = 5;
                        }
                        if (poängtime > 1)
                        {
                            score += 3;
                            gepoäng = false;
                            poängtime = 5;
                        } else
                        {
                            score += 1;
                            gepoäng = false;
                            poängtime = 5;
                        }
                    }
                }
                else
                {
                    liv--;
                    print("miss");
                }
            }
        }

        if (gepoäng)
        {
            poängtime -= Time.deltaTime;
        }
    }
    private void OnDisable()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save();
        }
    }
}