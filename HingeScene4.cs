using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HingeScene4 : MonoBehaviour {

    public GameObject sphere;
    public float originalTimer, zRot;
    public int count;

    GameObject clone, staticSurface, blockerR;
    float timer;

	// Use this for initialization
	void Start () {
        sphere.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        staticSurface = GameObject.FindGameObjectWithTag("Finish");
        blockerR = transform.GetChild(0).gameObject;
        timer = originalTimer;
        zRot = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (count > 0 && timer <= 0) {
            float x = Random.Range(-2f, 2f);
            float size = Random.Range(0.3f, 0.8f);

            Vector3 pos = new Vector3(x, 10);
            clone = Instantiate(sphere, pos, transform.rotation);

            Vector3 scale = clone.transform.localScale;
            scale.x = size;
            scale.y = size;
            clone.transform.localScale = scale;

            timer = originalTimer;
            count--;
        }
        if(count <= 0)
        {
            if (count == -15)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                blockerR.GetComponent<BoxCollider2D>().enabled = false;
            }
            count--;
        }
        if(count < -15)
        {
            Vector3 pos = staticSurface.transform.position;
            pos.y += Time.deltaTime/5;
            staticSurface.transform.position = pos;
            zRot -= 0.15f;
            staticSurface.transform.rotation = Quaternion.Euler(0, 0, zRot);
            originalTimer -= Time.deltaTime;
        }
        if(originalTimer < -5)
        {
            SceneManager.LoadScene("Scene5");
        }
    }
}
