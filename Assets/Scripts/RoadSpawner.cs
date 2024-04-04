using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{


    private Transform Player;

    private float speed;

    [SerializeField]
    private GameObject TestRoad;

    private Vector3 startPoint;

    private Vector3 ToAdd;

    private bool isSpawning;

    private int rand;

    private Vector3 offset;

    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = Player.GetComponent<PlayerController>().playerSpeed;
        startPoint = this.gameObject.transform.position;
        ToAdd = new Vector3(20, 0, 0);
        isSpawning = false;
        offset = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);

        transform.position = new Vector3(Player.position.x + 30, transform.position.y, transform.position.z);

        if(Vector3.Distance(startPoint, transform.position) > 20 && isSpawning == false)
        {
            SpawnRoad();
        }


    }


    private void SpawnRoad()
    {

        /*rand = Random.Range(1, 4);

        if(rand == 1)
        {
            isSpawning = true;
            GameObject Road = ObjectPool.SharedInstance.GetEasyRoad1();
            Road.transform.position = transform.position + ToAdd;
            Road.SetActive(true);
            startPoint = startPoint + ToAdd;
            isSpawning = false;
        }

        else if (rand == 2)
        {
            isSpawning = true;
            GameObject Road = ObjectPool.SharedInstance.GetEasyRoad2();
            Road.transform.position = transform.position + ToAdd;
            Road.SetActive(true);
            startPoint = startPoint + ToAdd;
            isSpawning = false;
        }

        else
        {
            isSpawning = true;
            GameObject Road = ObjectPool.SharedInstance.GetEasyRoad3();
            Road.transform.position = transform.position + ToAdd;
            Road.SetActive(true);
            startPoint = startPoint + ToAdd;
            isSpawning = false;
        }*/


        isSpawning = true;
        GameObject Road = ObjectPool.SharedInstance.GetRoad();
        Road.transform.position = startPoint + ToAdd;
        Road.SetActive(true);
        startPoint = startPoint + ToAdd;
        isSpawning = false;


    }
}
