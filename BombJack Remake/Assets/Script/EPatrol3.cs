using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPatrol3 : MonoBehaviour
{
    public float speed;
    public float slowValue;
    public float downDistance;
    public float frontDistance;
    public Component component;

    private bool movingRight = true;
    public Transform groundDetect;
    public Transform wallDetect;


    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, downDistance);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetect.position, Vector2.right, frontDistance, LayerMask.GetMask("Wall"));

        if (groundInfo.collider == false || wallInfo.collider == true)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void EnemySlow()
    {
        speed -= 1f;
        StartCoroutine(Slow());
    }

    private IEnumerator Slow()
    {
        yield return new WaitForSeconds(10f);
        speed += 1f;
    }
}
