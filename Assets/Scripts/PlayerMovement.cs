using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    [SerializeField] private float speed = 3f;

    public void Move() //todo follow nearest monster
    {
        float posX = Input.GetAxis("Horizontal");
        float posY = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(posX, posY ,0 );

        transform.Translate(direction * speed * Time.deltaTime);


    }
}
