using UnityEngine;

public class CylinderMovement : MonoBehaviour
{
    public float speed = 5f;

    void FixedUpdate()
    {
        // 1. 키 입력 감지 및 이동 방향 계산
        float moveInput = Input.GetAxis("Vertical");

        // 🚨 변경된 부분: 입력 값에 -1을 곱하여 방향 반전
        float invertedMoveInput = moveInput * -1f;

        // 2. 이동 벡터 계산: transform.right (로컬 X축) 사용
        // Up 키(moveInput = 1) -> invertedMoveInput = -1
        // Down 키(moveInput = -1) -> invertedMoveInput = 1
        Vector3 moveDirection = transform.right * invertedMoveInput * speed;

        // 3. 실제 이동 적용 (Rigidbody 사용)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = moveDirection;
        }
        else
        {
            transform.Translate(Vector3.right * invertedMoveInput * speed * Time.deltaTime);
        }
    }
}