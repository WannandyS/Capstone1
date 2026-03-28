using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float time = 2f;
    void Start()
    {
        MoveToPoint1();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveToPoint1()
    {
        LeanTween.move(this.gameObject, point1.position, 2f).setEaseOutExpo().setOnComplete(MoveToPoint2);
    }

    void MoveToPoint2()
    {
        LeanTween.move(this.gameObject, point2.position, 2f).setEaseOutExpo().setOnComplete(MoveToPoint1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>() != null)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(1);
            }
        }
    }
}
