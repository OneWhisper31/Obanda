using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	
	public Camera tinelli;
	public Transform subject;
	
	Vector2 startPosition;
	float startZ;
	
	Vector2 travel => (Vector2)tinelli.transform.position - startPosition;
	
	float distanceFromSubject => transform.position.z - subject.position.z;
	float clippingPlane => (tinelli.transform.position.z + (distanceFromSubject > 0? tinelli.farClipPlane: tinelli.nearClipPlane));
	
	float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;
	
	public void start(){
		startPosition = transform.position;
		startZ = transform.position.z;
	}

	public void update(){
		Vector2 newPos = startPosition + travel * parallaxFactor;
		transform.position = new Vector3(newPos.x, newPos.y, startZ);
	}
}