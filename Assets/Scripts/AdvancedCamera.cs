using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 target;
    [SerializeField] private float speed = 1.5f;

// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.fixedDeltaTime);
    }
}
