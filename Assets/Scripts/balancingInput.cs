using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class balancingInput : MonoBehaviour
{
    [SerializeField] private float sens;
    [SerializeField] private float minMax;
    [SerializeField] private float maxAngularVelocity;
    Rigidbody2D rb;
    private float startX;
    private float mousedis;
    private float mouseup;
    float startTimer;
    float angle;
    bool moveable;
    // Start is called before the first frame update
    void Start()
    {
        mousedis = 0;
        rb = GetComponent<Rigidbody2D>();
        startTimer = 2;
        freeze(1);
        StartCoroutine(startingDelay());
    }
    IEnumerator startingDelay()
    {
        moveable = false;
        
        yield return new WaitForSeconds(startTimer);
        moveable = true;

    }
    // Update is called once per frame
    void Update()
    {
        
        if (moveable)
        {
            freeze(0);
            manageRotation();
        }
        else
        {
            freeze(1);
        }
            
            
      
        //gameObject.transform.rotation = Quaternion.Euler(0, 0, Math.Clamp(gameObject.transform.rotation.z,-13,13));
    }
    public void freeze(int i)
    {
        if(i == 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    void manageRotation()
    {
        mousedis += sens * Input.GetAxis("Mouse X");
        mousedis = Math.Clamp(mousedis, -minMax, minMax);

        rb.AddTorque(-mousedis);
        if (rb.angularVelocity > maxAngularVelocity)
        {
            rb.angularVelocity = maxAngularVelocity;
        }
        else if (rb.angularVelocity < -maxAngularVelocity)
        {
            rb.angularVelocity = -maxAngularVelocity;
        }

        angle = gameObject.transform.eulerAngles.z;
        
        
    }
    public IEnumerator shake(float power,float duration, float delay)
    {
        float elapsedTim = 0f;
        float timeBetween = 0f;
        while(elapsedTim < duration)
        {
            if (timeBetween > delay)
            {
                float zOffset = UnityEngine.Random.Range(-0.5f, 0.5f) * power;

                //rb.AddTorque(UnityEngine.Random.Range(-power, power));
                //transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + zOffset);
                float pow = UnityEngine.Random.Range(power / 2, power);
                
                //rb.angularVelocity = UnityEngine.Random.Range(-power, power);
                rb.AddTorque(UnityEngine.Random.Range(-power, power));
                timeBetween = 0f;
            }
            
            
            elapsedTim += Time.deltaTime;
            timeBetween += Time.deltaTime;
            yield return null;
            
        }
        
    }
    public void resetRot(bool move)
    {
        moveable = move;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public IEnumerator reverseIn(float duration)
    {
        if(sens > 0)
        {
            sens *= -1;
            yield return new WaitForSeconds(duration);
            sens *= -1;
        }
        

    }
}
