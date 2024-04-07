using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public DashSkill dash { get; private set; }
    public CloneSkill clone { get; private set; }
    public ChronoSkill chrono { get; private set; }
    public ScytheThrowSkill scytheThrow { get; private set; }
    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        } else
        {
            instance = this;
        }
    }
    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
        chrono = GetComponent<ChronoSkill>();
        scytheThrow = GetComponent<ScytheThrowSkill>();
    }
}
