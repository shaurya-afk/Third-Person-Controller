using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float timeSmoothTime = 0.1f;
    float turnSmoothVel;

    private void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(hor, 0f, ver).normalized; //normalized -> normalize if both keys are pressed together

        if (dir.magnitude >= 0.1f)
        {
            float target_angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turnSmoothVel, timeSmoothTime);
            transform.rotation=Quaternion.Euler(0f,angle, 0f);

            Vector3 move_dir = Quaternion.Euler(0f, target_angle, 0f) * Vector3.forward;

            controller.Move(move_dir.normalized * speed * Time.deltaTime);
        }
    }
}
