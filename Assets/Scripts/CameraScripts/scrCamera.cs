using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCamera : MonoBehaviour
{
    public Transform Player;
    public float posicaoX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        posicaoX = Player.transform.position.x;
        this.transform.position = new Vector3(posicaoX, transform.position.y, transform.position.z);
    }
}
