using UnityEngine;
using System.Collections;

public class PBehaviour : MonoBehaviour {

	// Use this for initialization
	public float speedH;
    public float speedV;
    public float speedMoveTowards;
    public float min;
    public float max;
	public Transform target;
    Vector3 StartPosition;
    bool isGhosting;
    bool isStaying;
    public bool restarted;
    public float timer;
    float radius;
    Rigidbody2D rb;
	void Start()
	{
        rb = GetComponent<Rigidbody2D>();
        isGhosting = false;
        radius = .5f;
        StartPosition = transform.position;
	}
	// Update is called once per frame
	void Update () 
	{
        if (GameObject.Find("Main Camera").GetComponent<GameManager>().state == "restart")
        {
            transform.position = StartPosition;
            GameObject.Find("Main Camera").GetComponent<GameManager>().count++;
            if(GameObject.Find("Main Camera").GetComponent<GameManager>().count == 4)
            {
                GameObject.Find("Main Camera").GetComponent<GameManager>().state = "done";
                GameObject.Find("Main Camera").GetComponent<GameManager>().count = 0;
            }
        }
        isStaying = false;
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            float randNum = Random.Range(min, max);
            if(randNum >= max-2 && !isGhosting)
            {
                isGhosting = true;
            }
        }
        if (!isGhosting)
        {
            CastingRay();
        }
        else
        {
            GhostingMove();
            StartCoroutine(BlockWait());

        }

        //rotation always zero
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
    }

	void OnCollisionStay2D(Collision2D col)
	{
        Debug.Log("init");
        if (!isGhosting)
        {
            StartCoroutine(WaitForSecondCollision());
        }
	}
    void CastingRay()
    {
        RaycastHit2D hitN = Physics2D.Raycast(transform.position, Vector2.up);
        RaycastHit2D hitS = Physics2D.Raycast(transform.position, -Vector2.up);
        RaycastHit2D hitW = Physics2D.Raycast(transform.position, -Vector2.right);
        RaycastHit2D hitE = Physics2D.Raycast(transform.position, Vector2.right);
        Debug.DrawLine(transform.position, hitN.point, Color.green);
        Debug.DrawLine(transform.position, hitS.point, Color.green);
        Debug.DrawLine(transform.position, hitW.point, Color.green);
        Debug.DrawLine(transform.position, hitE.point, Color.green);
        if (hitE.collider != null || hitN.collider != null || hitS.collider != null || hitW.collider != null)
        {
            if (Mathf.Abs(hitN.distance) + Mathf.Abs(hitS.distance) > Mathf.Abs(hitW.distance) + Mathf.Abs(hitE.distance))
            {

                if (Mathf.Abs(hitN.distance) <= .1f && hitN.collider.tag != "Player")
                {
                    speedV *= -1;
                }
                if (Mathf.Abs(hitS.distance) <= .1f && hitN.collider.tag != "Player")
                {
                    speedV *= -1;
                }
                transform.Translate(new Vector2(0.0f, speedV * Time.deltaTime));
            }
            else
            {

                if (Mathf.Abs(hitW.distance) <= .1f && hitN.collider.tag != "Player")
                {
                    speedH *= -1;
                }
                if (Mathf.Abs(hitE.distance) <= .1f && hitN.collider.tag != "Player")
                {
                    speedH *= -1;
                }
                transform.Translate(new Vector2(speedH * Time.deltaTime, 0.0f));
            }
        }
    }
    void GhostingMove()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        Vector2 moveto = new Vector2(Random.Range(target.position.x - radius, target.position.x + radius), Random.Range(target.position.y - radius, target.position.y + radius));
        float distance = Vector3.Distance(transform.position, moveto);
        transform.position = Vector2.Lerp(transform.position, moveto, (Time.deltaTime * speedMoveTowards) / distance);
    }
    IEnumerator BlockWait()
    {
        yield return new WaitForSeconds(2.0f);
        RaycastHit2D hitN = Physics2D.Raycast(transform.position, Vector2.up);
        RaycastHit2D hitS = Physics2D.Raycast(transform.position, -Vector2.up);
        RaycastHit2D hitW = Physics2D.Raycast(transform.position, -Vector2.right);
        RaycastHit2D hitE = Physics2D.Raycast(transform.position, Vector2.right);
        Debug.DrawLine(transform.position, hitN.point, Color.green);
        Debug.DrawLine(transform.position, hitS.point, Color.green);
        Debug.DrawLine(transform.position, hitW.point, Color.green);
        Debug.DrawLine(transform.position, hitE.point, Color.green);
        if (hitN.distance + hitS.distance + hitW.distance + hitE.distance != 0.0f)
        {
            if(hitN.distance >= 0.06f && hitS.distance >= 0.06f && hitW.distance >= 0.06f && hitE.distance >= 0.06f)
            {
                isGhosting = false;
                GetComponent<BoxCollider2D>().isTrigger = false;
                timer = 100.0f;
                max = 100000.0f;
            }
            
        }

    }
    IEnumerator WaitForSecondCollision()
    {
        yield return new WaitForSeconds(0.8f);
        speedH *= -1;
        speedV *= -1;
    }
}
