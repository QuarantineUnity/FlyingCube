using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update


    public float power = 50;
    public Trajectory trajectory;
    private UnityEngine.Camera mainCamera;
    private int jumpsCount = 0;
    private bool startJump = false;
    private bool firstJump = false;
    private bool leftJump = false;
    private Rigidbody2D rigidbody2D;

    void Start()
    {
        mainCamera = UnityEngine.Camera.main;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public int target = 90;
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;

        Vector3 speedRight = new Vector3((float)1.7, (float)2.25) * power;

        Vector3 speedLeft = new Vector3((float)-1.7, (float)2.25) * power;

        if (Input.GetKeyDown(KeyCode.Space) && jumpsCount < 2)
        {
            if (jumpsCount == 0)
            {
                startJump = true;
            }
            firstJump = true;
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidbody2D.velocity = Vector3.zero;
            if (leftJump == false)
            {
                rigidbody2D.AddForce(speedRight, ForceMode2D.Impulse);
                leftJump = true;
            }
            else
            {
                rigidbody2D.AddForce(speedLeft, ForceMode2D.Impulse);
                leftJump = false;
            }
            jumpsCount++;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rigidbody2D.velocity = rigidbody2D.velocity * (float)0.75;
        }

        if (firstJump == false)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        startJump = false;
        jumpsCount = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (startJump == false)
        {
            //if (rigidbody2D.velocity.Equals(Vector3.zero))
            if (collision.gameObject.tag != "TilemapHorizontal")
            {
                rigidbody2D.velocity = Vector3.zero;
                Vector3 down = Vector3.down;
                float timeSinceLastFrame = Time.deltaTime;
                Vector3 translation = down * (timeSinceLastFrame / 2);
                transform.Translate(translation);
            }    
        }
    }
}
