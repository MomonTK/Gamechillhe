
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;


    //Folow player
    [SerializeField] private Transform player;


    private void Update()
    {
      
            //room camera
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        //follow player
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
