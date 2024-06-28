using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Скріпт управління камерою
public class CameraScript : MonoBehaviour
{
    private GameObject ball; // посилання на об'єкт на сцені (персонаж)
    private Vector3 offset;  // зміщення камери відносно персонажу
    private Vector3 mAngels; // кути, накопичкні рухом міші
    private float sensitivityH = 2f;
    private float sensitivityV = 1f;


    void Start()
    {
        ball = GameObject.Find("Ball"); // отримання референс
        offset = this.transform.position - ball.transform.position;
        mAngels = this.transform.eulerAngles;
    }

    private void Update()
    {
        mAngels.y += Input.GetAxis("Mouse X") * sensitivityH;
        mAngels.x -= Input.GetAxis("Mouse Y") * sensitivityV;

        if (mAngels.x > 78f) mAngels.x = 75f;
        if (mAngels.x < 35f) mAngels.x = 35f;
        
        if (mAngels.y > 360) mAngels.y -= 360;
        if (mAngels.y < 0) mAngels.y += 360;

    }

    // Update is called once per frame
    void LateUpdate() // вплив на камеру краще робити в LateUpdate
    {
        this.transform.position = ball.transform.position + 
            Quaternion.Euler(0, mAngels.y, 0) * offset;
        this.transform.eulerAngles = mAngels;
    }
}
