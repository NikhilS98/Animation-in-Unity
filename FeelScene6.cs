using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeelScene6 : MonoBehaviour {

    public GameObject sphere;

    List<GameObject> clones;
    GameObject last;
    Rigidbody2D rb;
    float timer;
    int count;
    bool exploded, lastCreated;

	// Use this for initialization
	void Start () {
        clones = new List<GameObject>();
        makeF(-3);
        makeE(-1);
        makeE(1);
        makeL(3);

        timer = 5;
        count = 5;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timer -= Time.deltaTime;

        if(timer < 1)
        {
            if (!lastCreated) {
                last = Instantiate(sphere, new Vector3(0, 1.5f), transform.rotation);
                rb = last.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                lastCreated = true;
            }

            Vector3 scale = last.transform.localScale;
            if (scale.x < 0.3f)
            {
                scale.x += Time.deltaTime / 6;
                scale.y += Time.deltaTime / 6;
                last.transform.localScale = scale;
            }

            Vector3 pos = last.transform.position;
            if(pos.y <= 0)
            {
                if(count-- == 0)
                {
                    SceneManager.LoadScene("Scene7");
                }
                Vector3 vel = rb.velocity;
                vel.x = 0;
                vel.y = 5;
                rb.velocity = vel;
            }
        }

        else if(timer < 3)
        {
            if (!exploded)
            {
                foreach (GameObject obj in clones)
                {
                    Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                    Vector3 vel = rb.velocity;
                    vel.x = Random.Range(-0.5f, 0.5f);
                    vel.y = Random.Range(-0.5f, 0.5f);
                    rb.velocity = vel;
                    Destroy(obj, Random.Range(2, 8));
                }
                exploded = true;
            }
        }
	}

    void makeF(float x)
    {
        makeVerticalLine(x, 1, 90);
        makeHorizontalLine(x, 1, 50);
        makeHorizontalLine(x, 0, 50);
    }

    void makeE(float x)
    {
        makeVerticalLine(x, 1, 90);
        makeHorizontalLine(x, 1, 50);
        makeHorizontalLine(x, -0.15f, 50);
        makeHorizontalLine(x, -1.25f, 50);
    }

    void makeL(float x)
    {
        makeVerticalLine(x, 1, 90);
        makeHorizontalLine(x, -1.25f, 50);
    }

    void makeVerticalLine(float x, float y, int num)
    {
        for(int i = 0; i < num; i++)
        {
            clones.Add(Instantiate(sphere, new Vector3(x, y - i * 0.025f), transform.rotation));
        }
    }

    void makeHorizontalLine(float x, float y, int num)
    {
        for (int i = 0; i < num; i++)
        {
            clones.Add(Instantiate(sphere, new Vector3(x + i * 0.025f, y), transform.rotation));
        }
    }

}
