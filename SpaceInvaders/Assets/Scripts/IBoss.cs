using UnityEngine;
using UnityEngine.UI;

public interface IBoss
{
    public void SetHealthBar(Slider healthbar);
    public void TakeDamage(int damage);
}
