using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatScene5 : MonoBehaviour {

    public GameObject sphere;
    public int numOfSpheres;

    GameObject[] clones;
    float timer;
    int extra;

	// Use this for initialization
	void Start () {
        extra = 8;
        clones = new GameObject[numOfSpheres + extra];
		for(int i = 0; i < numOfSpheres; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-11f, 11f), Random.Range(-3.5f, 3.5f));
            clones[i] = Instantiate(sphere, pos, transform.rotation);

            Vector3 scale = clones[i].transform.localScale;
            float inc = Random.Range(0f, 0.6f);
            scale.x += inc;
            scale.y += inc;
            clones[i].transform.localScale = scale;
        }
        for(int i = numOfSpheres; i < numOfSpheres + extra; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-2f, 8f), i - 20);
            clones[i] = Instantiate(sphere, pos, transform.rotation);

            Vector3 scale = clones[i].transform.localScale;
            float inc = Random.Range(0f, 0.6f);
            scale.x += inc;
            scale.y += inc;
            clones[i].transform.localScale = scale;
            clones[i].GetComponent<Rigidbody2D>().mass += Random.Range(0f, 0.5f);
            clones[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        timer = 20;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer -= Time.deltaTime;
        Vector3 pos = transform.position;
		if (pos.y < 0)
        {
            transform.Rotate(new Vector3(0, 0, -15 * Time.deltaTime));
            pos.y += 20 * Time.deltaTime;
            transform.position = pos;
        }
        if(timer < 19)
        {
            for(int i = 0; i < numOfSpheres; i++)
            {
                Rigidbody2D rb = clones[i].GetComponent<Rigidbody2D>();
                Vector3 vel = rb.velocity;
                vel.x = Random.Range(-0.5f, 0.5f);
                vel.y = Random.Range(-1f, 1f);
                rb.velocity = vel;
            }
            for(int i = numOfSpheres; i < numOfSpheres + extra; i++)
            {
                clones[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                Vector3 vel = clones[i].GetComponent<Rigidbody2D>().velocity;
                if (vel.y < -1) {
                    vel.y += 1f;
                    clones[i].GetComponent<Rigidbody2D>().velocity = vel;
                }
            }
        }
        if(timer < 17.5)
        {
            for (int i = 0; i < numOfSpheres + extra; i++)
            {
                Rigidbody2D rb = clones[i].GetComponent<Rigidbody2D>();
                Vector3 vel = rb.velocity;
                vel.x = Random.Range(-0.5f, 0.5f);
                vel.y = Random.Range(-1f, 1f);
                rb.velocity = vel;
            }
        }
        if(timer < 14)
        {
            for (int i = 0; i < numOfSpheres + extra; i++)
            {
                Vector3 scale = clones[i].transform.localScale;
                if (scale.x > 0) {
                    scale.x -= Time.deltaTime;
                    scale.y -= Time.deltaTime;
                    clones[i].transform.localScale = scale;
                }

                Rigidbody2D rb = clones[i].GetComponent<Rigidbody2D>();
                Vector3 vel = rb.velocity;
                if (clones[i].transform.position.x != 0 && clones[i].transform.position.y != 0)
                {
                    vel.x = 0 - clones[i].transform.position.x * 5;
                    vel.y = 0 - clones[i].transform.position.y * 5;
                    rb.velocity = vel;
                }
            }
        }
        if(timer < 12.5)
        {
            SceneManager.LoadScene("Scene6");
        }
	}
}
