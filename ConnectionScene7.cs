using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionScene7 : MonoBehaviour {

    public GameObject blackSphere, whiteSphere, connector;

    GameObject[] clones;
    Rigidbody2D[] rbs;
    Transform[] transforms;
    GameObject whiteClone;
    Vector3 whiteScale;
    float timer;
    bool stopVel, repositioned;

	// Use this for initialization
	void Start () {
        clones = new GameObject[2];
        rbs = new Rigidbody2D[2];
        transforms = new Transform[2];

        whiteClone = Instantiate(whiteSphere, Vector3.zero, transform.rotation);
        whiteScale = whiteClone.transform.localScale;
        whiteScale.x = 0;
        whiteClone.transform.localScale = whiteScale;

        connector = Instantiate(connector, new Vector3(0, 100), transform.rotation);


        for (int i = 0; i < 2; i++)
        {
            clones[i] = Instantiate(blackSphere, Vector3.zero, transform.rotation);
            rbs[i] = clones[i].GetComponent<Rigidbody2D>();
            transforms[i] = clones[i].transform;
            Vector3 scale = transforms[i].localScale;
            scale.x = 0.3f;
            scale.y = 0.3f;
            transforms[i].localScale = scale;
            rbs[i].bodyType = RigidbodyType2D.Kinematic;
            Vector3 vel = rbs[i].velocity;
            vel.y = 0.5f;
            rbs[i].velocity = vel;
        }

        timer = 11;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if(timer < 5)
        {
            connector.transform.Rotate(new Vector3(0, 0, -2));
            transforms[0].RotateAround(connector.transform.position, Vector3.forward, -2);
            transforms[1].RotateAround(connector.transform.position, Vector3.forward, -2);

            if(timer < 2.5f)
            {
                Vector3 scale = connector.transform.localScale;
                if (timer > -3)
                {
                    rbs[0].velocity = connector.transform.position - transforms[0].position;
                    rbs[1].velocity = connector.transform.position - transforms[1].position;

                    if (scale.x > 0)
                    {
                        scale.x -= 2 * Time.deltaTime;
                        connector.transform.localScale = scale;
                    }
                }
                else
                {
                    SceneManager.LoadScene("Scene8");
                }
            }
        }

        else if(timer < 7)
        {
            if (whiteScale.x >= 0)
            {
                whiteScale.x -= 2 * Time.deltaTime;
                whiteScale.y -= 2 * Time.deltaTime;
                whiteClone.transform.localScale = whiteScale;
            }
            else
            {
                if (whiteClone != null)
                {
                    connector.transform.position = whiteClone.transform.position;
                    Destroy(whiteClone);
                }

                Vector3 scale = connector.transform.localScale;
                scale.x += 2 * Time.deltaTime;
                if(scale.y > 0.05)
                    scale.y -= Time.deltaTime / 2;
                connector.transform.localScale = scale;

                Vector3 pos0 = transforms[0].position;
                pos0.x -= Time.deltaTime;
                transforms[0].position = pos0;

                Vector3 pos1 = transforms[1].position;
                pos1.x += Time.deltaTime;
                transforms[1].position = pos1;
            }
            
        }

        else if(timer < 8)
        {

        }

        else if(timer < 8.25f)
        {
            if (!repositioned)
            {
                whiteClone.transform.position = transforms[0].position;
                Vector3 pos = whiteClone.transform.position;
                pos.z = -1;
                whiteClone.transform.position = pos;
                repositioned = true;
            }

            Vector3 pos0 = transforms[0].position;
            pos0.x -= Time.deltaTime;
            transforms[0].position = pos0;

            Vector3 pos1 = transforms[1].position;
            pos1.x += Time.deltaTime;
            transforms[1].position = pos1;

            whiteScale.x += Time.deltaTime;
            whiteScale.y += 1.5f * Time.deltaTime;
            whiteClone.transform.localScale = whiteScale;

        }
		else if(timer < 9)
        {
            if (!stopVel)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector3 vel = rbs[i].velocity;
                    vel.y = 0;
                    rbs[i].velocity = vel;
                }
                stopVel = true;
            }
            for (int i = 0; i < 2; i++)
            {
                Vector3 scale = transforms[i].localScale;
                if (scale.x < 0.8f)
                {
                    scale.x += Time.deltaTime;
                    scale.y += Time.deltaTime;
                    transforms[i].localScale = scale;
                }
                
            }
        }
	}
}
