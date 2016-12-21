using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerBehavoir : MonoBehaviour {

	public float speed;
    public Sprite[] charSprite;
	bool canMove = true;
	public int playerLives;
	public int points;
    Vector3 startPosition;
	Animator anim;
	char facingDirection;
	GameObject projectile;
    // Use this for initialization
    GameObject lookingAt;
	public Text point;
	public Text life;
	void Awake () {
		playerLives = 3;
        projectile = GameObject.Find("PinWeapon");
        startPosition = transform.position;
        facingDirection = 'r';

    }
	
	// Update is called once per frame
	void Update () 
	{
		point.text = "Score: " + points;
		life.text = "Life left: " + playerLives;
		if( !canMove ) return;
		//check if player is alive
		
		Move ();
        RayCastInDirectionToGetLook();
		fireWeapon ();
	
	}
    void RayCastInDirectionToGetLook()
    {
        RaycastHit2D hitN = Physics2D.Raycast(transform.position, Vector2.up);
        RaycastHit2D hitS = Physics2D.Raycast(transform.position, -Vector2.up);
        RaycastHit2D hitW = Physics2D.Raycast(transform.position, -Vector2.right);
        RaycastHit2D hitE = Physics2D.Raycast(transform.position, Vector2.right);
        Debug.DrawLine(transform.position, hitN.point, Color.green);
        Debug.DrawLine(transform.position, hitS.point, Color.green);
        Debug.DrawLine(transform.position, hitW.point, Color.green);
        Debug.DrawLine(transform.position, hitE.point, Color.green);
        if(facingDirection == 'r')
        {
            lookingAt = hitE.collider.gameObject;
        }
        else if (facingDirection == 'l')
        {
            lookingAt = hitW.collider.gameObject;
        }
        else if (facingDirection == 'u')
        {
            lookingAt = hitN.collider.gameObject;
        }
        else if (facingDirection == 'd')
        {
            lookingAt = hitS.collider.gameObject;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "enemy" || collision.collider.tag == "rock" || collision.collider.tag == "fire")
        {
            playerLives--;
            transform.position = startPosition;
            GameObject isThere = GameObject.Find("PinWeapon(Clone)");
            if(isThere != null)
            {
                Destroy(isThere);
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision)
	{
        if (collision.collider.tag == "ground")
        {
            if (lookingAt == collision.collider.gameObject)
            {
                Destroy(collision.gameObject);
                StartCoroutine(BlockWait());
            }
        }
	}
	void Move()
	{
		float horizontalInput = 0.0f;
		float verticalInput = 0.0f;
		if(Input.GetAxis ("Horizontal")> 0)
		{
			horizontalInput = speed;
            facingDirection = 'r';
            GetComponent<SpriteRenderer>().sprite = charSprite[0];
		}
		else if(Input.GetAxis ("Horizontal")< 0)
		{
			horizontalInput = -speed;
            facingDirection = 'l';
            GetComponent<SpriteRenderer>().sprite = charSprite[1];
        }
		else if (horizontalInput == 0 && Input.GetAxis ("Vertical")>0) 
		{
			verticalInput = speed;
            facingDirection = 'u';
            GetComponent<SpriteRenderer>().sprite = charSprite[2];
        }
        else if (horizontalInput == 0 && Input.GetAxis ("Vertical") < 0) 
		{
			verticalInput = -speed;
            facingDirection = 'd';
            GetComponent<SpriteRenderer>().sprite = charSprite[3];
        }
        transform.Translate (horizontalInput, verticalInput, 0.0f);
        //rotation always zero
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
    }
    void fireWeapon()
    {
        GameObject isThere = GameObject.Find("PinWeapon(Clone)");
        if (Input.GetMouseButtonDown(0) && isThere == null)
        {
            Vector2 Located;
            if(facingDirection == 'r')
            {
                Located = new Vector2(transform.position.x + .15f, transform.position.y);
            }
            else if (facingDirection == 'l')
            {
                Located = new Vector2(transform.position.x - .15f, transform.position.y);
            }
            else if (facingDirection == 'u')
            {
                Located = new Vector2(transform.position.x, transform.position.y + .15f);
            }
            else if (facingDirection == 'd')
            {
                Located = new Vector2(transform.position.x, transform.position.y - .15f);
            }
            (Instantiate(projectile, transform.position, transform.rotation) as GameObject).GetComponent<WeaponBehaviour>().Direction(facingDirection);

        }
    }
	IEnumerator BlockWait()
	{
		canMove = false;
		yield return new WaitForSeconds(.5f);
		canMove = true;
		
	}  

}
