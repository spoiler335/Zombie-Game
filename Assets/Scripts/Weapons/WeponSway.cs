using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponSway : MonoBehaviour
{
    public float amount = 0.1f;
    public float maxAmont = 0.3f;
    public float smoothAmount = 6f;

    private Vector3 initPos;

    void Start()
    {
        initPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = -Input.GetAxis("Mouse X") * amount;
        float moveY = -Input.GetAxis("Mouse Y") * amount;
        moveX = Mathf.Clamp(moveX, -maxAmont, maxAmont);
        moveY = Mathf.Clamp(moveY, -maxAmont, maxAmont);

        Vector3 finalPosToMove = new Vector3(moveX,moveY,0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosToMove + initPos, Time.deltaTime*smoothAmount);


    }
}
