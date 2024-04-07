using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform startPoint; // ��������� ����� ������
    public float maxDistance = 10f; // ����������� ��������� ������
    public LayerMask layerMask; // ���� ��� ��������� ���������
    public LineRenderer lineRenderer; // ��������� ��� ����������� �� ������
    public float rotationSpeed = 1f; // �������� ��������� ������

    void Update()
    {
        if (startPoint != null && lineRenderer != null)
        {
            // �������� ��������� ������� �������� � ���������� �� ����� "Player" ��� "Ground"
            RaycastHit2D hit = Physics2D.Raycast(startPoint.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right, maxDistance, layerMask);

            Vector2 endPosition; // ������� ���� ������

            if (hit.collider != null)
            {
                // ���� �������� ��������, ������������ ������ ����� ������ �� ���� ��������
                endPosition = hit.point;
            }
            else
            {
                // ���� �������� �� ��������, ������������ ������ ����� ������ �� ����������� ���������
                endPosition = (Vector2)startPoint.position + ((Vector2)Vector3.right * maxDistance);
            }

            // ������������ ��������� �� ������ ����� ��
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPosition);

        }
    }
}
