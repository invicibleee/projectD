using UnityEngine;

public class MiracleMirage : MonoBehaviour
{
    [SerializeField] private GameObject miragePrefab; // ������ ������
    [SerializeField] private float mirageDuration = 10f; // ������������ ������ � ��������

    private GameObject mirageInstance; // ��������� ������

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            ActivateMirage();
        }
    }
    public void ActivateMirage()
    {
        if (mirageInstance == null)
        {
            // ������� ��������� ������
            mirageInstance = Instantiate(miragePrefab, transform.position, Quaternion.identity);
            // ������������� ����� ����� ������
            Destroy(mirageInstance, mirageDuration);
        }
    }
}
