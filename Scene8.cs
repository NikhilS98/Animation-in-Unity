using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene8 : MonoBehaviour {

    public GameObject sphere;

    GameObject[] clones;
    Rigidbody2D[] rbs;
    Text text;
    float prevTimer, timer;
    string[] words = { "DESIGNED", "BY", "NIKHIL", "IN", "KARACHI" };
    int count;

	// Use this for initialization
	void Start () {
        clones = new GameObject[12];
        rbs = new Rigidbody2D[clones.Length];

        text = GetComponent<Text>();
        text.text = "";
        prevTimer = 5;
        timer = 4.2f;
        count = 0;
        for(int i = 0; i < clones.Length; i++)
        {
            clones[i] = Instantiate(sphere, new Vector3(Random.Range(-20f, -10), Random.Range(-2f, 2f)), transform.rotation);
            rbs[i] = clones[i].GetComponent<Rigidbody2D>();
            rbs[i].bodyType = RigidbodyType2D.Kinematic;
        }
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        
        if(prevTimer - timer >= 1)
        {
            prevTimer = timer;
            for (int i = 0; i < clones.Length; i++)
            {
                Vector3 vel = rbs[i].velocity;
                vel.x = 3;
                vel.y = Random.Range(-2f, 2f);
                rbs[i].velocity = vel;
            }

            if (count < 5)
            {
                text.text += words[count++] + " ";
            }
        }
        
	}
}
