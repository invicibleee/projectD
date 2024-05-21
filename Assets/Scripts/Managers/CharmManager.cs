using System.Collections;
using System.Collections.Generic;
//using UnityEditor.TerrainTools;
using UnityEngine;

public class CharmManager : MonoBehaviour
{
    public static CharmManager instance;

    public EssenceAbsorberCharm essenceAbsorber { get; set; }
    public ArcaneResonanceOrbCharm arcaneResonanceOrb { get; set; }
    public BerserkersRageCharm berserkersRage { get; set; }
    public BlinkstrikeAmuletCharm blinkstrikeAmulet { get; set; }
    public GlassCannonCharm glassCannon { get; set; }
    public ManaflowMedallionCharm manaflowMedallion { get; set; }
    public MirrorshadeVeilCharm mirrorshadeVeil { get; set; }
    public MulticastCharm multicast { get; set; }
    public NecrophageHeartCharm necrophageHeart { get; set; }
    public ReapersGraspCharm reapersGrasp { get; set; }
    public ShadowstepAmuletCharm shadowstepAmulet { get; set; }
    public SwiftParryCharm swiftParry { get; set; }
    public VampiricEmbraceCharm vampiricEmbrace { get; set; }
    public AegisAmuletCharm aegisAmulet { get; set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        essenceAbsorber = GetComponent<EssenceAbsorberCharm>();
        arcaneResonanceOrb = GetComponent<ArcaneResonanceOrbCharm>();
        berserkersRage = GetComponent<BerserkersRageCharm>();
        blinkstrikeAmulet = GetComponent<BlinkstrikeAmuletCharm>();
        glassCannon = GetComponent<GlassCannonCharm>();
        manaflowMedallion = GetComponent<ManaflowMedallionCharm>();
        mirrorshadeVeil = GetComponent<MirrorshadeVeilCharm>();
        multicast = GetComponent<MulticastCharm>();
        necrophageHeart = GetComponent<NecrophageHeartCharm>();
        reapersGrasp = GetComponent<ReapersGraspCharm>();
        shadowstepAmulet = GetComponent<ShadowstepAmuletCharm>();
        swiftParry = GetComponent<SwiftParryCharm>();
        vampiricEmbrace = GetComponent<VampiricEmbraceCharm>();
        aegisAmulet = GetComponent<AegisAmuletCharm>();

    }

}
