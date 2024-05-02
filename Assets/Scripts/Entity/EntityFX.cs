using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("FlashFX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMaterial;
    private Material originalMaterial;

    [Header("AIlment colors")]
    [SerializeField] private Color[] chillColor;//ok
    [SerializeField] private Color[] igniteColor;//blood
    [SerializeField] private Color[] shockColor;//poison
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMaterial;
        Color currentColor = sr.color;

        sr.color = Color.white;
        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMaterial;
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    private void CancelColorChhange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
    public void ShockFXFor(float _seconds)
    {
        InvokeRepeating("ShockColorFX", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }
    public void ChillFXFor(float _seconds)
    {
        InvokeRepeating("ChillColorFX", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }
    public void IgniteFXFor(float _seconds)
    {
        InvokeRepeating("IgniteColorFX", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }
    private void ShockColorFX()
    {
        if (sr.color != shockColor[0])
            sr.color = shockColor[0];
        else
            sr.color = shockColor[1];
    }
    private void ChillColorFX()
    {
        if (sr.color != chillColor[0])
            sr.color = chillColor[0];
        else
            sr.color = chillColor[1];
    }
    private void IgniteColorFX()
    {
        if (sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
            sr.color = igniteColor[1];
    }
   
   
}
