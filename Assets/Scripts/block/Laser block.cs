using UnityEngine;
using System;

public class Laserblock : MonoBehaviour
{
    public bool shouldRotate = false;
    public GameObject g;

    public float speed = 5.0f;  // 이동 속도
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // A와 D 키, 좌우 화살표에 반응
        float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;   // W와 S 키, 상하 화살표에 반응
        transform.Translate(moveX, moveZ, 0);

        float rayLength = 5.0f;  // 레이캐스트의 길이
        int layerMask = LayerMask.GetMask("sss");  // 레이캐스트가 반응할 레이어 마스크

        // 레이캐스트를 쏘는 방향 배열
        Vector3[] directions = {
            Vector3.down,  // 아래
            Vector3.up,    // 위
            Vector3.left,  // 왼쪽
            Vector3.right  // 오른쪽
        };

        if (Input.GetKeyDown(KeyCode.Space))
        {

            foreach (Vector3 dir in directions)
            {
                Vector3 checkDirection = shouldRotate ? Quaternion.Euler(0, 0, 45) * dir : dir;  // 조건에 따라 방향을 조절
                RaycastHit2D hit = Physics2D.Raycast(transform.position, checkDirection, rayLength, layerMask);
                if (hit.collider != null)
                {
                    Debug.DrawLine(transform.position, transform.position + checkDirection * rayLength, new Color(1, 0, 0), 0.1f);
                    Debug.Log("Hit " + hit.collider.name + " at " + checkDirection);

                    if (hit.collider.name == "TargetBlock" && !shouldRotate)
                    {
                        shouldRotate = true;  // 첫 번째 블록에 충돌 시 회전 실시
                    }
                    else if (hit.collider.name == "TragetBlock2" && shouldRotate)
                    {
                        Debug.Log("Found TragetBlock2, resetting rotation.");
                        shouldRotate = false;  // 두 번째 블록에 충돌 시 회전 리셋
                    }
                    else if (hit.collider.name == "Power")
                    {
                        Collider2D collider = g.GetComponent<Collider2D>();
                        if (collider != null)
                        {
                            collider.isTrigger = !collider.isTrigger;
                        }
                    }
                }
            }

        }

    }
}

