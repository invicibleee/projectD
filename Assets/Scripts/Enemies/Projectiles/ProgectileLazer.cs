using UnityEngine;

public class ProjectileLazer : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float speed;
    private float startTime;
    private Transform projectileRotation;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float maxAccelerationTime = 2f; // ��� ��� ���������� ������������� �����������
    [SerializeField]
    private float damageRadius;
    [SerializeField]
    private float destroyDelay = 2f; // �������� ����� ��������� ��'����

    public Transform startPoint; // ��������� ����� ������
    public float maxDistance = 10f; // ����������� ��������� ������
    public LayerMask layerMask; // ���� ��� ��������� ���������
    public LineRenderer lineRenderer; // ��������� ��� ����������� �� ������

    private EnemyEye enemyEye;

    private void Start()
    {
        // ���������� ������� ��'����
        transform.rotation = Quaternion.Euler(0, 0, -45);
        startTime = Time.time;
        enemyEye = GetComponent<EnemyEye>();
    }

    private void Update()
    {
        if (startPoint != null && lineRenderer != null)
        {
            // �������� ��������� ������� �������� � ���������� �� ����� "Player" ��� "Ground"
            RaycastHit2D hit = Physics2D.Raycast(startPoint.position, Quaternion.Euler(0, projectileRotation.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * Vector2.right, maxDistance, layerMask);
            Vector2 endPosition; // ������� ���� ������

            if (hit.collider != null)
            {
                // ���� �������� ��������, ������������ ������ ����� ������ �� ���� ��������
                endPosition = hit.point;

                // ����������, �� �������� �������� � ��'����� �� �������� ���
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    // ��������� ����� Damage �� ��'���, � ���� �������� �����
                    hit.collider.gameObject.SendMessage("Damage", attackDetails);
                }
            }
            else
            {
                // ������������ ������ ����� ������ �� ����������� ���������, �������� �� ����� ������� ��������� �����
                endPosition = startPoint.position + Quaternion.Euler(0, projectileRotation.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * Vector2.right * maxDistance;

            }

            // ��������� ������ ����� ������ � �������� ���� ��������
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPosition);
        }

        // ��������� �������� ���, �� ������� � ������� ������� ������� 䳿
        float timeElapsed = Time.time - startTime;

        // ����������� ������� �������� ��������� ������� �� ����
        float currentRotationSpeed = Mathf.Lerp(0f, rotationSpeed * 10f, Mathf.Clamp01(timeElapsed / maxAccelerationTime));

        // �������� ��'���
        transform.Rotate(Vector3.forward * currentRotationSpeed * Time.deltaTime);

        // ����������, �� ��� �������� ��'���� ��� ������
        if (Time.time >= startTime + destroyDelay)
        {
            // ������� ��'���
            Debug.Log("����� �������");
            Destroy(gameObject);
        }
    }

    public void FireProjectile(float damage, Transform transform)
    {
        attackDetails.damageAmount = damage;
        projectileRotation = transform;
    }
}
