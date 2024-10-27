using UnityEngine;

public class Enemy_SideWays : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //gán chức năng 6 vào thành phần có tên "Player"
        if(collision.tag == "Player")
        {
            collision.GetComponent<Heal>().TakeDamage(damage);
        }
    }


    //mo dau bằng tạo huong di chuyển trai phai
    private void Awake()
    {
        //ben trai bang vi tri X - khoang cach va nguoc lai
        leftEdge = transform.position.x - movementDistance; 
        rightEdge = transform.position.x + movementDistance;

    }

    private void Update()
    {
        //kiem tra 
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);

                //Lat hoat anh
                transform.localScale = new Vector3(10, 10, -10);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);

                //lat hoat anh
                transform.localScale = new Vector3(-10, 10, 10);
            }
            else
                movingLeft = true;
        }
    }

}
