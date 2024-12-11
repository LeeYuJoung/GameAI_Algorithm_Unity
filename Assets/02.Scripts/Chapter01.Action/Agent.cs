using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� �������� ����� ���� �������� Ȱ�� (���� �� ����)
public class Agent : MonoBehaviour
{
    private Rigidbody agentRigidbldy;

    protected Steering steering;
    public float maxSpeed;    // �ִ�ӵ�
    public float maxAccel;    // �ִ밡��
    public float orientation; // ���Ⱚ
    public float rotation;    // ȸ����
    public Vector3 velocity;  // �ӵ�

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

        // ���� ��ġ�� ������ ó�� ��ġ�� ���� �� ���ͷ� (��ȭ��)
        Vector3 displacement = velocity * Time.deltaTime;
        orientation = orientation * Time.deltaTime;

        // ȸ�� ������ ������ 0~360 ���̷� ����
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

        // ������ ���̸� ��ȯ���� �� maxSpeed���� ũ�ٸ�
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

        // ������ �ϰ� �������� ���� ForceMode���� ����
        agentRigidbldy.AddForce(displacement, ForceMode.VelocityChange);
        Vector3 orientationVector = OriToVec(orientation);
        agentRigidbldy.rotation = Quaternion.LookRotation(orientationVector, Vector3.up);
    }

    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }

    // Orientation Value (���Ⱚ)�� ���ͷ� ��ȯ�ϴ� �Լ�
    public Vector3 OriToVec(float orientation)
    {
        Vector3 vector = Vector3.zero;
        vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
        vector.z = Mathf.Cos(orientation *  Mathf.Deg2Rad) * 1.0f;

        return vector;
    }
}