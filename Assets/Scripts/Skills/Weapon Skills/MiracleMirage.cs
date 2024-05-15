using UnityEngine;

public class MiracleMirage : MonoBehaviour
{
    [SerializeField] private GameObject miragePrefab; // ������ ������
    [SerializeField] private float mirageDuration = 10f; // ������������ ������ � ��������

    [SerializeField] private PlayerStats playerStats;
    private GameObject mirageInstance; // ��������� ������

    public void ActivateMirage()
    {
        playerStats.DecreaseUlt(playerStats.maxUlt.GetValue());
        if (mirageInstance == null)
        {
            // ������� ��������� ������
            mirageInstance = Instantiate(miragePrefab, transform.position, Quaternion.identity);
            // ������������� ����� ����� ������
            Destroy(mirageInstance, mirageDuration);
        }
    }
}
