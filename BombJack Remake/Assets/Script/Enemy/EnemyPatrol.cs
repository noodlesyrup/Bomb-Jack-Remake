using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public float downDistance;
    public float frontDistance;

    private bool isFacingRight = true;

    public Transform groundDetect;
    public Transform wallDetect;

    private Animator anim;

    private void Start()
    {
     
    }

    void Update()
    {
        int layer_mask = LayerMask.GetMask("Wall");
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, downDistance);

        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetect.position, Vector2.right, frontDistance, layer_mask);


        if (groundInfo.collider == false || wallInfo.collider == true)
        {
            if(isFacingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isFacingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isFacingRight = true;
            }
        }

    }
}
