using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShapesGenerator : MonoBehaviour {

    public GameObject sphere;

    GameObject clone;
    Vector3 scale;
    float timer, size;
    bool square, diamond, circle;

	// Use this for initialization
	void Start () {
        sphere.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        timer = 8f;
        size = 4;
        scale = sphere.transform.localScale;
        scale.x = 0.2f;
        scale.y = 0.2f;
        sphere.transform.localScale = scale;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if(timer <= 1.2)
        {
            SceneManager.LoadScene("Scene2");
        }
        else if (timer <= 3)
        {
            if(!circle)
            {
                circle = true;
                makeCircle();
            }
            float vel = 4 * size / Mathf.PI;
            clone.transform.RotateAround(Vector3.zero, Vector3.forward, -vel);
        }
        else if (timer <= 4.5)
        {
            if(!diamond)
            {
                diamond = true;
                makeDiamond();
            }
        }
        else if (timer <= 6)
        {
            if(!square)
            {
                square = true;
                makeSquare();
            }
        }

	}

    void makeSquare()
    {
        float vel = 2 * size / 2;

        Vector3 pos = new Vector3(transform.position.x - size, transform.position.y + size);
        GameObject topL = Instantiate(sphere, pos, transform.rotation);
        topL.GetComponent<Rigidbody2D>().velocity = new Vector3(vel, 0);

        pos = new Vector3(transform.position.x - size, transform.position.y - size);
        GameObject bottomL = Instantiate(sphere, pos, transform.rotation);
        bottomL.GetComponent<Rigidbody2D>().velocity = new Vector3(0, vel);

        pos = new Vector3(transform.position.x + size, transform.position.y + size);
        GameObject topR = Instantiate(sphere, pos, transform.rotation);
        topR.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -vel);

        pos = new Vector3(transform.position.x + size, transform.position.y - size);
        GameObject bottomR = Instantiate(sphere, pos, transform.rotation);
        bottomR.GetComponent<Rigidbody2D>().velocity = new Vector3(-vel, 0);

        DelayedDestroy(topL);
        DelayedDestroy(topR);
        DelayedDestroy(bottomR);
        DelayedDestroy(bottomL);
    }

    void makeDiamond()
    {
        float vel = 2 * size / 2;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y + size);
        GameObject top = Instantiate(sphere, pos, transform.rotation);
        top.GetComponent<Rigidbody2D>().velocity = new Vector3(vel / 2, -vel / 2);

        pos = new Vector3(transform.position.x, transform.position.y - size);
        GameObject bottom = Instantiate(sphere, pos, transform.rotation);
        bottom.GetComponent<Rigidbody2D>().velocity = new Vector3(-vel / 2, vel / 2);

        pos = new Vector3(transform.position.x + size, transform.position.y);
        GameObject right = Instantiate(sphere, pos, transform.rotation);
        right.GetComponent<Rigidbody2D>().velocity = new Vector3(-vel / 2, -vel / 2);

        pos = new Vector3(transform.position.x - size, transform.position.y);
        GameObject left = Instantiate(sphere, pos, transform.rotation);
        left.GetComponent<Rigidbody2D>().velocity = new Vector3(vel / 2, vel / 2);

        DelayedDestroy(top);
        DelayedDestroy(bottom);
        DelayedDestroy(left);
        DelayedDestroy(right);
    }

    void makeCircle()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + size);
        clone = Instantiate(sphere, pos, transform.rotation);
        DelayedDestroy(clone);
    }

    void DelayedDestroy(GameObject obj)
    {
        Destroy(obj, 2);
    }
}
