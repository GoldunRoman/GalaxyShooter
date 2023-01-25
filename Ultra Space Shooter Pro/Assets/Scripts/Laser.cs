using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    private float _borderY = 7.2f;

    void Update()
    {         
        LaserMovementUp();        
    }

    private void LaserMovementUp()
    {
        transform.Translate(0f, _speed * Time.deltaTime, 0f);

        if(transform.position.y > _borderY)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    
}
