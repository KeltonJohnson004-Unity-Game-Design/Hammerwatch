using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    public GameObject groundObject;
    public bool isStart = false;
    public bool isEnd = false;

    public bool hasSpawned = false;
    public Transform locationToSpawn;
    public void setRoomStart()
    {
        Renderer rend = groundObject.GetComponent<Renderer>();
        rend.material.color = new Color(0f, 1f, 0f, 1f);
        isStart = true;
    }

    public void setRoomEnd()
    {
        Renderer rend = groundObject.GetComponent<Renderer>();
        rend.material.color = new Color(1f, 0f, 0f, 1f);
        isEnd = true;
    }

    public void setRoomTemp()
    {
        Renderer rend = groundObject.GetComponent<Renderer>();
        rend.material.color = new Color(0f, 0f, 1f, 1f);
        isEnd = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && isEnd)
        {
            other.GetComponent<thirdPersonMovementController>().canExit = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isEnd)
        {

            other.GetComponent<thirdPersonMovementController>().canExit = false;
        }
    }
}
