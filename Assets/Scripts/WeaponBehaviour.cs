using UnityEngine;
using System.Collections;

public class WeaponBehaviour : MonoBehaviour {

    // Use this for initialization
    public float speedH;
    public float speedV;
	Rigidbody2D rb;
	float speed;
    float timeHitEnemy;
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		speed = 0.7f;
        timeHitEnemy = .2f;
    }
	
	// Update is called once per frame
	void Update ()
    {

        transform.Translate(new Vector2(speedH* Time.deltaTime, speedV * Time.deltaTime));
	
	}
    public void Direction(char d)
    {
        speedV = 0;
        speedH *= -1;
        if (d == 'r')
        {
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        }
        else if (d == 'l')
        {            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
        }
        else if (d == 'u')
        {
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
        }
        else if (d == 'd')
        {
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 270.0f));
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="enemy")
        {
            
            speedH = 0;
            speedV = 0;
            other.transform.position = transform.position;
            if(other.name == "Fygars")
            {
                other.GetComponent<FBehaviour>().speedH = 0;
                other.GetComponent<FBehaviour>().speedV = 0;
            }
            else
            {
                other.GetComponent<PBehaviour>().speedH = 0;
                other.GetComponent<PBehaviour>().speedV = 0;
            }
        }
        else if (!(other.tag == "Player" || other.tag == "Untagged"))
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            timeHitEnemy -= Time.deltaTime;
            if (timeHitEnemy <= 0)
            {
                if (other.name == "Fygars")
                {
                    GameObject.Find("digdug_various_sheet_4").GetComponent<PlayerBehavoir>().points += 500;
                }
                else
                {
                    GameObject.Find("digdug_various_sheet_4").GetComponent<PlayerBehavoir>().points += 250;
                }
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
