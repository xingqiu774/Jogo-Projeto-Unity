using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bg1 : MonoBehaviour
{
    
	public Camera cam;
	public Transform followTarget;
	Vector2 startingPosition;
	float startingZ;
	
	float ZdistanceFromTarget => transform.position.z - followTarget.transform.position.z;
	float paralaxFactor => Mathf.Abs(ZdistanceFromTarget) / clippingPlane;
	
	float clippingPlane => (cam.transform.position.z + (ZdistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
	
	Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;
	
	// Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
		startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * paralaxFactor;
		transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
	
}
