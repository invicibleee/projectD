using UnityEngine;

public class MiracleMirage : MonoBehaviour
{
    [SerializeField] private GameObject miragePrefab; // Префаб миража
    [SerializeField] private float mirageDuration = 10f; // Длительность миража в секундах

    private GameObject mirageInstance; // Экземпляр миража

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
            // Создаем экземпляр миража
            mirageInstance = Instantiate(miragePrefab, transform.position, Quaternion.identity);
            // Устанавливаем время жизни миража
            Destroy(mirageInstance, mirageDuration);
        }
    }
}
