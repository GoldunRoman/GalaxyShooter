using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 13f;

    private void Update()
    {
        LaserMovementDown();
    }
    private void LaserMovementDown()
    {  
        transform.Translate((Vector3.down * _speed) * Time.deltaTime);
    }

}
