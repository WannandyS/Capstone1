using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;
    void Start()
    {
        int randomNumber = Random.Range(1, 101);
        textMesh.text = randomNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
