using UnityEngine;
using System.Collections;

public class rock : MonoBehaviour
{

    // Use this for initialization
    bool didjiggle;
    bool once;
    void Start()
    {
        didjiggle = false;
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, -Vector2.up);
        Debug.DrawLine(transform.position, hit.point, Color.blue);
        
        if (hit.distance - .066f > 0 || hit.collider.tag != "ground")
        {
            StartCoroutine(BlockJiggel());
            StartCoroutine(BlockFall(hit));
            
        }
        else
        {
            didjiggle = false;
            once = true;
        } 
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "enemy")
        {
            Destroy(col.gameObject);
        }
    }
    IEnumerator BlockJiggel()
    {
        if (!didjiggle)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 10.0f);
            yield return new WaitForSeconds(Random.Range(.01f, .4f));
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -10.0f);
            yield return new WaitForSeconds(Random.Range(.01f, .4f));
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 10.0f);
            yield return new WaitForSeconds(Random.Range(.01f, .4f));
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -10.0f);
            yield return new WaitForSeconds(Random.Range(.01f, .4f));
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            didjiggle = true;
        }
    }
    IEnumerator BlockFall(RaycastHit2D hit)
    {
        if (once)
        {
            yield return new WaitForSeconds(2.5f);
            once = false;
            Vector2 moveto = new Vector2(transform.position.x, hit.transform.position.y);
            transform.position = Vector2.Lerp(transform.position, moveto, (Time.deltaTime * .5f) / hit.distance);
        }
        else
        {
            Vector2 moveto = new Vector2(transform.position.x, hit.transform.position.y);
            transform.position = Vector2.Lerp(transform.position, moveto, (Time.deltaTime * .5f) / hit.distance);
        }
        
    }
}