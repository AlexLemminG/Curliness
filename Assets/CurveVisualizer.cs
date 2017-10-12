using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveVisualizer : MonoBehaviour {
	public int pointsCount;
	public bool normalize;

	List<Transform> m_controlPoints;
	IPointCurve m_curve;

	IPointCurve CreateCurve(List<Point> points){
		return new BezierPointCurve (points);
	}

	void OnDrawGizmos(){
		var conrolPoints = new List<Point> ();
		m_controlPoints = new List<Transform> ();
		for (int i = 0; i < transform.childCount; i++) {
			m_controlPoints.Add (transform.GetChild (i));
		}
		foreach (var controlPoint in m_controlPoints) {
			var point = Point.FromWorldTransform (controlPoint);
			conrolPoints.Add (point);
		}
		m_curve = CreateCurve (conrolPoints);
		if (normalize) {
			m_curve = new NormalizedPointCurve (m_curve);
		}

		float indexToT = 1f / (pointsCount - 1);
		List<Point> points = new List<Point>();
		for (int i = 0; i < pointsCount; i++) {
			points.Add(m_curve.Evaluate(i * indexToT));
		}

		Gizmos.color = Color.white;
		for (int i = 0; i < pointsCount-1; i++) {
			var pointA = points [i];
			var pointB = points[i+1];
			Gizmos.DrawLine (pointA.position, pointB.position);
			Gizmos.DrawWireSphere (pointA.position, 0.01f);
		}

		Gizmos.color = Color.gray;
		foreach (var point in conrolPoints) {
			Gizmos.DrawWireSphere (point.position, 0.02f);
		}
	}
}
