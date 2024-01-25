using UnityEngine;

[CreateAssetMenu(fileName = "NewTrapUpgrade", menuName = "Custom/Trap Upgrade")]
public class TrapUpgrade : ScriptableObject
{
    public int level;
    public float trapDmg;
    public float trapCooldown;
    public float trapStunDuration;
}
