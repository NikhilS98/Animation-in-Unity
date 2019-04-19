using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandMotionScene2 : MonoBehaviour
{

    public GameObject sphere;

    GameObject[] clones;
    float timer, prevTimer;
    Vector3 point;
    Vector3[] dest;
    bool move;
    int count;

    // Use this for initialization
    void Start()
    {
        clones = new GameObject[8];
        dest = new Vector3[8];

        dest[0] = new Vector3(0, 2);
        dest[1] = new Vector3(2, 1);
        dest[2] = new Vector3(4, 0);
        dest[3] = new Vector3(2, -1);
        dest[4] = new Vector3(0, -2);
        dest[5] = new Vector3(-2, -1);
        dest[6] = new Vector3(-4, 0);
        dest[7] = new Vector3(-2, 1);

        timer = 15;
        prevTimer = timer;
        count = 0;
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (count < 8)
        {
            if(prevTimer - timer >= 0.2f) {
                clones[count] = Instantiate(sphere, dest[count], transform.rotation);
                clones[count].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                count++;
                prevTimer = timer;
            }
        }
        else
        {
            if(timer > 12)
            {
                for (int i = 0; i < clones.Length; i++)
                {
                    Vector3 pos = Vector3.zero;
                    pos.x = Random.Range(6f, 10f);
                    pos.y = Random.Range(2f, 4f);
                    Vector3 vel = new Vector3(pos.x - clones[i].transform.position.x, pos.y - clones[i].transform.position.y);
                    clones[i].GetComponent<Rigidbody2D>().velocity = vel;
                }
            }
            else if (timer > 10.2f)
            {
                for (int i = 0; i < clones.Length; i++)
                {
                    point = new Vector3(Random.value + 6, Random.value + 2);
                    clones[i].transform.RotateAround(point, Vector3.forward, -2);
                }
            }
            else if (timer > 7)
            {
                for (int i = 0; i < clones.Length; i++)
                {
                    Vector3 vel = new Vector3((dest[i].x - clones[i].transform.position.x), (dest[i].y - clones[i].transform.position.y));
                    clones[i].GetComponent<Rigidbody2D>().velocity = vel;
                }
            }
            else if (timer > 6)
            {
                if (count++ == 8)
                {
                    for (int i = 0; i < clones.Length; i++)
                    {
                        Vector3 vel = new Vector3((0 - clones[i].transform.position.x), (0 - clones[i].transform.position.y));
                        clones[i].GetComponent<Rigidbody2D>().velocity = vel;
                    }
                }
            }
            else if (timer > 5)
            {
                SceneManager.LoadScene("Scene3");
            }
        }
    }
}
