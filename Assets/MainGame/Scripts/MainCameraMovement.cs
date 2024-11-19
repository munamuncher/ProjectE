using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{
    private Transform player;
    private Vector3 camPos;

    [Header("camer MaxPostion")]
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float maxY;
    [SerializeField]
    private float minX;
    [SerializeField]
    private float minY;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        if(player == null)
        {
            Debug.LogWarning($"{this.name} - Awake() - Player 위치 참조 실패");
        }
    }

    private void LateUpdate()
    {
        camPos = new Vector3(player.position.x, player.position.y, -10f);
        camPos.x = Mathf.Clamp(camPos.x, minX ,maxX);
        camPos.y = Mathf.Clamp(camPos.y, minY, maxY);
        transform.position = camPos;
    }
}
