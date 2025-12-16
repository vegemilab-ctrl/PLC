using UnityEngine;
using System.Collections; // IEnumerator를 사용하기 위해 추가해야 합니다.

public class ProductSpawner : MonoBehaviour
{
    // 생산할 제품 오브젝트 (Unity Inspector에서 할당)
    public GameObject product;
    // 생산 위치 (Unity Inspector에서 할당)
    public Transform[] spawnPositions;
    //첫 상품 제조 시간
    public float startSpawnTime = 0f;
    //상품 제조 시간 간격
    public float spawnInterval = 1f;
    //상품 제조 예정 시간
    private float _spawnTime = 0f;


    private void OnEnable()
    {
        //상품 제조기에 전원이 켜질때마다 첫 상품제조시간을 현재 시간에 더해
        //제조예정시간에 기록한다. OnEnable -> 활성화 비활성화 On/Off로 볼 때 첫상품만드는 시간 + 이후 상품제조시간 리셋됨.
        _spawnTime = Time.time + startSpawnTime;
    }

    //인풋에 연결된 Product액션에 호출됨.
    public void OnProduct()
    {
        //생산 위치 배열 수만큼 반복해서 생산한다.
        for (int i = 0; i < spawnPositions.Length; ++i)
        {
            Instantiate(product, spawnPositions[i].position, spawnPositions[i].rotation);
        }
    }

    private void Update()
    {
        //상품 제조 예정 시간이 아직 안되었으면 돌아가라.
        if (_spawnTime > Time.time)
            return;

        //다음 상품 제조 예정 시간을 갱신
        _spawnTime += spawnInterval;
        //상품 제조
        OnProduct();
    }
}