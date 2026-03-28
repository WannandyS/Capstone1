using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    private Player player;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        player = this.gameObject.GetComponent<Player>();
        healthSlider.maxValue = player.maxHealth;
        healthSlider.value = player.maxHealth;
    }

    private void Update()
    {
        healthSlider.value = player.maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Traps")
        {
            player.TakeDamage(1);
        }
    }
}
