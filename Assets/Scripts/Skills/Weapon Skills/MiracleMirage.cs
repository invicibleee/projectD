using UnityEngine;

public class MiracleMirage : MonoBehaviour
{
    [SerializeField] private GameObject miragePrefab; // Префаб миража
    [SerializeField] private float mirageDuration = 10f; // Длительность миража в секундах

    [SerializeField] private PlayerStats playerStats;
    private GameObject mirageInstance; // Экземпляр миража

    public void ActivateMirage()
    {
        playerStats.DecreaseUlt(playerStats.maxUlt.GetValue());
        if (mirageInstance == null)
        {
            // Создаем экземпляр миража
            mirageInstance = Instantiate(miragePrefab, transform.position, Quaternion.identity);
            // Устанавливаем время жизни миража
            Destroy(mirageInstance, mirageDuration);
        }
    }
}
