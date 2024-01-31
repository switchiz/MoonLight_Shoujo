using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    [Header("보스 데이터")]
    /// <summary>
    /// 보스의 총알
    /// </summary>
    public PoolObjectType bullet = PoolObjectType.EnemyBossBullet;

    /// <summary>
    /// 보스의 미사일
    /// </summary>
    public PoolObjectType misslie = PoolObjectType.EnemyBossMisslie;

    /// <summary>
    /// 총알 발사 간격
    /// </summary>
    public float bulletInterval = 1.0f;

    /// <summary>
    /// 보스 활동영역(최소, 월드좌표)
    /// </summary>
    public Vector2 areaMin = new Vector2(2, -3);

    /// <summary>
    /// 보스 활동영역(최대, 월드좌표)
    /// </summary>
    public Vector2 areaMax = new Vector2(7, 3);

    /// <summary>
    /// 미사일 일제사격 회수
    /// </summary>
    public int barrageCount = 3;

    /// <summary>
    /// 총알 발사 위치1 
    /// </summary>
    Transform fire1;

    /// <summary>
    /// 총알 발사 위치2
    /// </summary>
    Transform fire2;

    /// <summary>
    /// 미사일 발사 위치
    /// </summary>
    Transform fire3;

    /// <summary>
    /// 보스의 이동 방향
    /// </summary>
    Vector3 moveDirection = Vector3.left;

    /// <summary>
    /// 보스 패턴
    /// </summary>
    bool bossP = true;

    /// <summary>
    /// 보스 위아래
    /// </summary>
    int updown = 1;

    private void Awake()
    {
        Transform fireTransforms = transform.GetChild(1);
        fire1 = fireTransforms.GetChild(0);
        fire2 = fireTransforms.GetChild(1);
        fire3 = fireTransforms.GetChild(2);
        

    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * moveSpeed * moveDirection);
        
        
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        StopAllCoroutines();

    }


    IEnumerator MovePatternProcess()
    {
        moveDirection = Vector3.left; // 처음엔 왼쪽

        float middelX = (areaMax.x - areaMin.x) * 0.5f + areaMin.x; // area의 가운데 

        while(transform.position.x > middelX) // 중간 지점에 도달할 때까지 계속 왼쪽으로 진행
        {
            yield return null;
        }

        StartCoroutine(FireBullet()); // 총알 발사 시작
        ChangeDirection(); // 아래로 움직임

        while (true)
        {
            if (transform.position.y > areaMax.y || transform.position.y < areaMin.y)
            {
                // 중간 지점에 도착
                ChangeDirection(); // 방향 전환
                StartCoroutine(FireMissile()); // 방향 전환 할 때 마다 미사일 발사
            }
            yield return null;
        }


    }

    /// <summary>
    /// 보스의 위 아래 방향을 변경하는 함수
    /// </summary>

    void ChangeDirection()
    {
        Vector3 target = new Vector3();
        target.x = Random.Range(areaMin.x , areaMax.x); // x 위치는 최소~최대 사이
        target.y = (moveDirection.y > 0) ? areaMin.y : areaMax.y; // y 위치는 올라가던 중이면 최소 , 내려가던 중이면 최대

        moveDirection = (target - transform.position).normalized; // 방향 수정

        
    }


    /// <summary>
    /// 보스 스폰이 완료된 후 마지막으로 실행되는 함수
    /// </summary>
    public void OnSpawn()
    {
        StartCoroutine(MovePatternProcess());
    }

    private void OnDrawGizmosSelected()
    {

        // 보스가 움직이는 영역
        Gizmos.color = Color.blue;

        Vector3 p0 = new(areaMin.x, areaMin.y);
        Vector3 p1 = new(areaMax.x, areaMin.y);
        Vector3 p2 = new(areaMax.x, areaMax.y);
        Vector3 p3 = new(areaMin.x, areaMax.y);

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }

    /// <summary>
    /// 총알 발사 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FireBullet()
    {
        while(true)
        {
            Factory.Instance.GetObject(PoolObjectType.EnemyBossBullet, fire1.position);
            Factory.Instance.GetObject(PoolObjectType.EnemyBossBullet, fire2.position);
            yield return new WaitForSeconds(bulletInterval);
        }
    }

    /// <summary>
    /// 미사일 발사 코루틴
    /// </summary>
    /// <returns></returns>

    IEnumerator FireMissile()
    {
        for (int i = 0; i < barrageCount;i++)
        {
            Factory.Instance.GetObject(PoolObjectType.EnemyBossBullet, fire3.position);
            yield return new WaitForSeconds(0.2f);
        }

    }
}

// 0. 스폰되면 정해진 영역의 가운데까지 전진
// 1. 특정 영역 안에서 위아래로 왕복한다.

// 2. 계속 주기적으로 총알을 발사한다.(1번 시작할 때 부터)

// 3. 이동 방향을 변경할 때 미사일을 3발 연속으로 발사한다.