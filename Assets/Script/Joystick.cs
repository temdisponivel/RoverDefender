using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour
{
    private Quaternion initialRotation = default(Quaternion);
    private float sumAngleHorizontal = 0;
    private float sumAngleVertical = 0;
    private float lastAngleHorizontal = 0;
    private float lastAngleVertical = 0;

    void Start()
    {
        initialRotation = this.transform.rotation;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if (horizontal != 0 && sumAngleHorizontal + horizontal < 30 && sumAngleHorizontal + horizontal > -30)
        {
            sumAngleHorizontal += horizontal;
            this.transform.Rotate(Vector3.forward, horizontal * 10 * Time.deltaTime, Space.Self);
        }

        if (vertical != 0 && sumAngleVertical + vertical < 30 && sumAngleVertical + vertical > -30)
        {
            sumAngleVertical += vertical;
            this.transform.Rotate(Vector3.right, vertical * 10 * Time.deltaTime, Space.Self);
        }

        if (horizontal == 0)
        {
            float angulo = 0;
            if (sumAngleHorizontal != 0)
            {
                angulo = Time.deltaTime * -sumAngleHorizontal * 10;
                this.transform.Rotate(Vector3.forward, angulo, Space.Self);
                sumAngleHorizontal = 0;
            }
        }
        if (vertical == 0)
        {
            float angulo = 0;
            if (sumAngleVertical != 0)
            {
                angulo = Time.deltaTime * -sumAngleVertical * 10;
                this.transform.Rotate(Vector3.right, angulo, Space.Self);
                sumAngleVertical = 0;
            }
        }
    }
}
