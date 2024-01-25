using UnityEngine;

[CreateAssetMenu(fileName = "NewTalismanUpgrade", menuName = "Custom/Talisman Upgrade")]
public class TalismanUpgrade : ScriptableObject
{
    public int level;
    public float talismanDmg;
    public float talismanCooldown;
}