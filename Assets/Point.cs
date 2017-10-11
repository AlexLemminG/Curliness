using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point {
	public Vector3 position;
	public Quaternion rotation;
	public Vector3 scale;

	public Point (Vector3 position, Quaternion rotation, Vector3 scale)
	{
		this.position = position;
		this.rotation = rotation;
		this.scale = scale;
	}

	public Point Copy(){
		return new Point (position, rotation, scale);
	}

	public override string ToString ()
	{
		return string.Format ("[Point: position={0}, rotation={1}, scale={2}]", position, rotation, scale);
	}
	

	public static Point Lerp(Point a, Point b, float t){
		Vector3 position = Vector3.Lerp (a.position, b.position, t);
		Quaternion rotation = Quaternion.Lerp (a.rotation, b.rotation, t);
		Vector3 scale = Vector3.Lerp (a.scale, b.scale, t);
		return new Point (position, rotation, scale);
	}

	public void ToLocalTransform(Transform transform){
		transform.localPosition = position;
		transform.localRotation = rotation;
		transform.localScale = scale;
	}
	public void ToWorldTransform(Transform transform){
		transform.position = position;
		transform.rotation = rotation;
		transform.localScale = scale;
	}

	public static Point FromLocalTransform(Transform transform){
		return new Point (transform.localPosition, transform.localRotation, transform.localScale);
	}

	public static Point FromWorldTransform(Transform transform){
		return new Point (transform.position, transform.rotation, transform.lossyScale);
	}

	public static Point identity {
		get{
			return new Point (Vector3.zero, Quaternion.identity, Vector3.one);
		}
	}
}
