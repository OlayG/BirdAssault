using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	public Transform bird;

    void LateUpdate()
    {
        transform.position = new Vector3(bird.position.x,
                                         transform.position.y,
                                         transform.position.z);
    }
}
