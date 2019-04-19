using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BounceScene3 : MonoBehaviour {

    public GameObject sphere;
    public int num;

    GameObject[] clones;
    float[] ground;
    float timer;
    bool exploded;

	// Use this for initialization
	void Start () {
        clones = new GameObject[num];
        ground = new float[num];

		for(int i = 0; i < num; i++)
        {
            clones[i] = Instantiate(sphere, Vector3.zero, transform.rotation);
            clones[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            clones[i].GetComponent<CircleCollider2D>().enabled = false;
            clones[i].GetComponent<Rigidbody2D>().mass = 1;
            ground[i] = Random.Range(-2f, 0);
        }
        timer = 2;
	}
	
	void FixedUpdate () {
        timer -= Time.deltaTime;

        if(timer < -5.2)
        {
            SceneManager.LoadScene("Scene4");
        }

        else if (timer < -4)
        {
            
        }

        else if (timer <= 0.5f)
        {
            if (!exploded)
            {
                Vector3 forceVector;

                for (int i = 0; i < num; i++)
                {
                    float mid = Mathf.PI / 2;
                    float angle = Random.Range(mid - 0.35f, mid + 0.35f);

                    float force = Random.Range(7.5f, 9f);
                    forceVector.x = force * Mathf.Cos(angle);
                    forceVector.y = force * Mathf.Sin(angle);
                    forceVector.z = 0;

                    clones[i].GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);
                    Vector3 scale = clones[i].transform.localScale;
                    float inc = Random.Range(0f, 0.5f);
                    scale.x += inc;
                    scale.y += inc;
                    clones[i].transform.localScale = scale;
                    clones[i].GetComponent<Rigidbody2D>().gravityScale = 1f;
                }
                exploded = true;
            }
            else
            {
                for (int i = 0; i < num; i++)
                {
                    Vector3 pos = clones[i].transform.position;
                    if (pos.y <= ground[i])
                    {
                        Vector3 vel = clones[i].GetComponent<Rigidbody2D>().velocity;
                        vel.x = 0;
                        vel.y = 0;
                        clones[i].GetComponent<Rigidbody2D>().velocity = vel;
                        clones[i].GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 5f, 0), ForceMode2D.Impulse);
                    }
                }
            }
        }

        
	}
}