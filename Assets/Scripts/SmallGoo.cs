using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGoo : MonoBehaviour
{

    private GameObject Player;

    private GameObject og_pos;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int maxDistance;

    private Rigidbody rb;

    [SerializeField]
    private int points;

    public PlayerController PlayerController { get; private set; }

    private Vector3 xPlosionOffset;

    private GameObject sfxManager;

    private SFXManager SFXManager;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();

        og_pos = gameObject.transform.parent.Find("Og_Pos").gameObject;

        xPlosionOffset = new Vector3(-0.5f, 0.5f, 0);

        sfxManager = GameObject.Find("SFXManager");
        SFXManager = sfxManager.GetComponent<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Hit();
    }

    private RaycastHit Hit()
    {

        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;



        Vector3 endPos = transform.position + (maxDistance * transform.forward);
        if (Physics.Raycast(ray, out hit, maxDistance))
        {

            endPos = hit.point;

            Debug.DrawLine(transform.position, endPos, Color.green);

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                endPos = hit.point;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Player")
                    {
                        StartCoroutine(Rush());
                    }
                }
            }


        }

        return hit;
    }


    private IEnumerator Rush()
    {
        rb.AddForce(-speed, 0, 0);
        yield return new WaitForSeconds(5f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = og_pos.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            PlayerController.AddScore(points);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = og_pos.transform.position;

            GameObject xPlosion = ObjectPool.SharedInstance.GetXplosion();
            xPlosion.transform.position = transform.position + xPlosionOffset;
            xPlosion.gameObject.SetActive(true);
            xPlosion.GetComponent<Xplosion>().StartXPlosion();

            SFXManager.PlaySFX(0);
            SFXManager.PlaySFX(1);

            this.gameObject.SetActive(false);
        }
    }

}