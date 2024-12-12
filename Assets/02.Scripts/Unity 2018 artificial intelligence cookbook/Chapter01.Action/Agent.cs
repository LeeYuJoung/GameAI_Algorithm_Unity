using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 지능적인 움직임을 만들기 위해 행위들을 활용 (파일 및 뼈대)
public class Agent : MonoBehaviour
{
    private Rigidbody agentRigidbldy;

    protected Steering steering;
    public float maxSpeed;    // 최대속도
    public float maxAccel;    // 최대가속
    public float orientation; // 방향값
    public float rotation;    // 회전값
    public Vector3 velocity;  // 속도

    private void Start()
    {
        agentRigidbldy = GetComponent<Rigidbody>();

        velocity = Vector3.zero;
        steering = new Steering();
    }

    public virtual void Update()
    {
        if (agentRigidbldy == null)
            return;

        // 나중 위치의 값에서 처음 위치의 값을 뺀 벡터량 (변화량)
        Vector3 displacement = velocity * Time.deltaTime;
        orientation = orientation * Time.deltaTime;

        // 회전 값들의 범위를 0~360 사이로 제한
        if(orientation < 0.0f)
        {
            orientation += 360.0f;
        }
        else if(orientation > 360.0f)
        {
            orientation -= 360.0f;
        }

        transform.Translate(displacement, Space.World);
        transform.rotation = new Quaternion();
        transform.Rotate(Vector3.up, orientation);
    }

    public virtual void LateUpdate()
    {
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;

        // 벡터의 길이를 반환했을 때 maxSpeed보다 크다면
        if(velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;
        }
        if(steering.angular == 0.0f)
        {
            rotation = 0.0f;
        }
        if(steering.linear.sqrMagnitude == 0.0f)
        {
            velocity = Vector3.zero;
        }

        steering = new Steering();
    }

    public virtual void FixedUpdate()
    {
        if (agentRigidbldy == null)
            return;

        Vector3 displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;

        if(orientation < 0.0f)
        {
            orientation += 360.0f;
        }
        else if(orientation > 360.0f)
        {
            orientation -= 360.0f;
        }

        // 무엇을 하고 싶은지에 따라 ForceMode값을 설정
        agentRigidbldy.AddForce(displacement, ForceMode.VelocityChange);
        Vector3 orientationVector = OriToVec(orientation);
        agentRigidbldy.rotation = Quaternion.LookRotation(orientationVector, Vector3.up);
    }

    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }

    // Orientation Value (방향값)을 벡터로 변환하는 함수
    public Vector3 OriToVec(float orientation)
    {
        Vector3 vector = Vector3.zero;
        vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
        vector.z = Mathf.Cos(orientation *  Mathf.Deg2Rad) * 1.0f;

        return vector;
    }
}