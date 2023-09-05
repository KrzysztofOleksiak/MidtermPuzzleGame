using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Adjust the speed as needed
    [SerializeField] private float maxHeight = 5f; // Maximum height the object can reach
    [SerializeField] private float minHeight = 0f; // Minimum height the object can go
    //[SerializeField] private Animator animator; // Reference to the Animator component

    [SerializeField] private bool isUp; // By default, start moving up
    //private bool idle;

    private void Start()
    {
        //idle = true;
    }

    private void Update()
    {
        // Check the bool flag and move accordingly
        if (isUp)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
        //animator.SetBool("Idle", idle);
        //if(idle)animator.SetTrigger("Unstall");
    }
    public void Up()
    {
        isUp = true;
    }
    public void Down()
    {
        isUp = false;
    }
    private void MoveUp()
    {
        if (transform.position.y < maxHeight)
        {
            // Move the object up
            transform.Translate(transform.up * moveSpeed * Time.deltaTime);
            //idle = false;
        }
        else
        {
            if(transform.position.y != maxHeight) transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
            //idle = true;
        }
    }

    private void MoveDown()
    {
        if (transform.position.y > minHeight)
        {
            // Move the object down
            transform.Translate(-transform.up * (moveSpeed/2) * Time.deltaTime);
            //idle = false;
        }
        else
        {
            if(transform.position.y != minHeight) transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
            //idle = true;
        }
    }
}