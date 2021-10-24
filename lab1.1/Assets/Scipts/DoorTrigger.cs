using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Canvas canvas;
    public DoorController targetDoor;
    public bool playerstay = false;
    //public bool doorOpen = false;

    private void Update()
    {
        canvas.gameObject.SetActive(playerstay);
        if (playerstay && Input.GetKeyDown(KeyCode.E))
        {
            targetDoor.doorOpen = !targetDoor.doorOpen;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerstay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerstay = false;
        }
    }
}
