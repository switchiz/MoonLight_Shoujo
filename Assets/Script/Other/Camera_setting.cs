using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera_setting : MonoBehaviour
{
    Transform target;

    public float smoothSpeed = 3;
    public Vector2 offset;
    public float limitMinX, limitMaxX;
    public float fixedY;
    float cameraHalfWidth, cameraHalfHeight;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        fixedY = transform.position.y;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),
            fixedY, // Y 위치를 고정된 값으로 설정
            -10); // Z                                                                                               // Z
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
}
