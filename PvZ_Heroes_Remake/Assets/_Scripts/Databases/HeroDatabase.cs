using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroDatabase", menuName = "Database/HeroDatabase")]
public class HeroDatabase : ScriptableObject
{
    public List<HeroInfo> heroList;
}
