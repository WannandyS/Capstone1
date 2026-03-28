using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{
    private int currentRed_Gem;
    private int currentGreen_Gem;

    [SerializeField] private Text redGemText;
    [SerializeField] private Text greenGemText;
    [SerializeField] private GameObject collectEffect_Prefab;
    private int maxHealthPlayer;

    private Player player;
    void Start()
    {
        currentRed_Gem = 0;
        currentGreen_Gem = 0;
        player = this.gameObject.GetComponent<Player>();
        maxHealthPlayer = player.maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Heart")
        {
            if (player.maxHealth < maxHealthPlayer)
            {
                player.maxHealth++;
                GameObject tempEffectPrefab = Instantiate(collectEffect_Prefab, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(tempEffectPrefab, 0.8f);
                Destroy(collision.gameObject);
            } else
            {
                Debug.Log("Already max limit");
            }
        }

        if (collision.gameObject.tag == "Red_Gem")
        {
            currentRed_Gem++;
            redGemText.text = currentRed_Gem.ToString();
            GameObject tempCollectEffect = Instantiate(collectEffect_Prefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(tempCollectEffect, 0.8f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Green_Gem")
        {
            currentGreen_Gem++;
            greenGemText.text = currentGreen_Gem.ToString();
            GameObject tempCollectEffect = Instantiate(collectEffect_Prefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(tempCollectEffect, 0.8f);
            Destroy(collision.gameObject);
        }
    }
}
