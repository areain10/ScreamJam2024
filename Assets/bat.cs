using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class bat : MonoBehaviour
{
    Vector2 direction;
    [SerializeField] float speed;
    Rigidbody2D rb;
    public float velocidadMax;
    public float shake;
    public float shakeDuration;

    public float xMax;
    public float zMax;
    public float xMin;
    public float zMin;

    private float x;
    private float z;
    private float tiempo;
    private float angulo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        x = Random.Range(-velocidadMax, velocidadMax);
        z = Random.Range(-velocidadMax, velocidadMax);
        angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
        transform.localRotation = Quaternion.Euler(0, angulo, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
        //rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Shake");
            collision.gameObject.GetComponent<playerHub>().shakeTray(shake,shakeDuration);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        var contact = collision.contacts[0];
        var v = rb.velocity;
        v.Normalize();
        float dotOfV = Vector2.Dot(v,direction * -1);
        Vector2 R = new Vector2();
        R += (direction * -1) * -2 * dotOfV + v;
        direction = R;
        */
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        
    }


    // Update is called once per frame
    void Move()
    {

        tiempo += Time.deltaTime;

        if (transform.localPosition.x > xMax)
        {
            x = Random.Range(-1f, 0.0f);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, 0, angulo);
            tiempo = 0.0f;
        }
        if (transform.localPosition.x < xMin)
        {
            x = Random.Range(0.0f, 1);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, 0, angulo);
            tiempo = 0.0f;
        }
        if (transform.localPosition.y > zMax)
        {
            z = Random.Range(-1, 0.0f);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, 0, angulo);
            tiempo = 0.0f;
        }
        if (transform.localPosition.y < zMin)
        {
            z = Random.Range(0.0f, 1);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, 0, angulo);
            tiempo = 0.0f;
        }


        if (tiempo > 1.0f)
        {
            x = Random.Range(-velocidadMax, velocidadMax);
            z = Random.Range(-velocidadMax, velocidadMax);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0,0,angulo);
            tiempo = 0.0f;
        }
        
        rb.velocity = new Vector2 (x, z) * speed;
        //transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + z, transform.localPosition.z );
        
    }
}

