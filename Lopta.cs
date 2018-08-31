using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Lopta : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource source;
    private Transform ball;
    private Transform player;
    public Transform referenceObject;
    public float deltaForce = 15f;
    public float ballSpeed = 10f;
    private bool hasCollided = false;
    private Vector3 originalPosition;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("PlayerCharacter").transform;
        ball = GameObject.Find("PlayingBall").transform;
        originalPosition = transform.position;
        rb.angularDrag = 0;
        rb.AddForce(0, 0, -1000f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasCollided)
        {
            hasCollided = false;
            Vector3 objVelocity = rb.velocity;
            objVelocity.x /= 2.0f;
            objVelocity.y /= 2.0f;
            rb.velocity = objVelocity;
        }

        if (Mathf.Abs(rb.velocity.z) < 5.0f && rb.velocity.z != 0f)
        {
            Vector3 newSpeed = rb.velocity;
            newSpeed.z = rb.velocity.z * deltaForce;
            rb.AddForce(newSpeed);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        source.Play();
        hasCollided = true;
        if (col.gameObject.name.Equals("LeftHand") || col.gameObject.name.Equals("RightHand"))
        {
            rb.velocity = Vector3.zero;
            Vector3 newDir = col.contacts[0].point - transform.position;
            newDir = -newDir.normalized;
            newDir.z = ballSpeed;
            newDir.y /= 2;
            newDir.y *= ballSpeed;
            newDir.x /= 2;
            newDir.x *= ballSpeed;
            //rb.AddForce(newDir * 300f);
            rb.velocity = newDir;
        }
        //if(Mathf.Abs(rb.velocity.z) <= 5.0f)
        //{
        //    //Vector3 dir = col.contacts[0].point - transform.position;
        //    //dir = -dir.normalized;
        //    Vector3 dir = new Vector3(0, 0, 1);
        //    rb.AddForce(dir * 300f);
        //}

        if (rb.velocity.z < 0f)
        {
            rb.velocity = Vector3.zero;
            Vector3 shootTowards = referenceObject.position - transform.position;
            shootTowards.x += Random.Range(-1.5f, 1.5f);
            shootTowards.y += Random.Range(-1f, 1f);
            rb.AddForce(shootTowards * deltaForce);
        }
    }

    private void ResetBall()
    {
        transform.position = originalPosition;
        rb.velocity = Vector3.zero;
        rb.AddForce(0, 0, -1000f);
    }

}
