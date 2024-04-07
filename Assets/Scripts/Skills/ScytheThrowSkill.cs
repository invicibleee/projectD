using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheThrowSkill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject scythePrefab;
    [SerializeField] private Vector2 launchDirection;
    [SerializeField] private float scytheGravity;
}
