using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update


    public float power = 50;

    public Trajectory trajectory;

    private UnityEngine.Camera mainCamera;

    private bool isFly = false;
    private int jumpsCount = 0;
    private bool isTouchWall = false;
    private bool isOnGround = true;
    private bool startJump = false;
    private bool firstJump = false;
    private int jumpAllow = 0; // если 0 - можно куда угодно, если 1 - можно только направо, если 2 - можно только налево
    private bool leftJump = false;
    

    private Rigidbody2D rigidbody2D;


    Vector3 lastVelocity;


    void Start()
    {
        mainCamera = UnityEngine.Camera.main;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float enter;

        ////Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Луч от камеры до координат нажатия мышки
        //new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);

        //Vector3 tap = new Vector3(-ray.GetPoint(enter).x, -ray.GetPoint(enter).y);

        Vector3 speedRight = new Vector3((float)1.7, (float)2.25) * power;

        Vector3 speedLeft = new Vector3((float)-1.7, (float)2.25) * power;

        Vector3 zeroSpeed = new Vector3(0, 0);
        //transform.position * power
        //transform.rotation = Quaternion.LookRotation(speed);

        


        //if (Input.GetMouseButtonDown(0) && isTouchWall && rigidbody2D.velocity.y < 0.01 && rigidbody2D.velocity.x < 0.01)
        //{
        //    isOnGround = true;
        //}
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
            isOnGround = false;
            isTouchWall = false;
            jumpsCount++;
        }
        //else if (Input.GetKeyDown(KeyCode.Space) && jumpsCount < 2)
        //{
            
        //        jumpAllow = 0;
        //        if (jumpsCount == 0)
        //        {
        //            startJump = true;
        //        }
        //        firstJump = true;
        //        rigidbody2D.constraints = RigidbodyConstraints2D.None;
        //        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        //        rigidbody2D.velocity = Vector3.zero;
        //        rigidbody2D.AddForce(speedLeft, ForceMode2D.Impulse);
        //        isOnGround = false;
        //        isTouchWall = false;
        //        jumpsCount++;
            
        //}

        if (firstJump == false)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        isTouchWall = true;
        startJump = false;
        jumpsCount = 0;

        //if (collision.gameObject.tag == "GameController")
        //{
        //    Vector3 dir = (collision.gameObject.transform.position - gameObject.transform.position).normalized;
        //    if (dir.y > 0)
        //    {
        //        print("Сверху");
        //        jumpAllow = 0;
        //    }
        //    else if (dir.y < 0)
        //    {
        //        print("Снизу");
        //        jumpAllow = 0;
        //    }
        //    else if ((this.transform.position.x - collision.collider.transform.position.x) < 0)
        //    {
        //        print("Правая сторона колизии");
        //        jumpAllow = 1;
        //    }
        //    else if ((this.transform.position.x - collision.collider.transform.position.x) > 0)
        //    {
        //        print("Левая сторона колизии");
        //        jumpAllow = 2;
        //    }
        //}
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (startJump == false)
        {
            rigidbody2D.velocity = Vector3.zero;
            Vector3 down = Vector3.down;
            float timeSinceLastFrame = Time.deltaTime;
            Vector3 translation = down * (timeSinceLastFrame/2);
            transform.Translate(translation);
        }
    }
}
