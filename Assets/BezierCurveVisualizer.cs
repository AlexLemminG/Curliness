using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveVisualizer : MonoBehaviour {
	public List<Transform> controlPoints;
	public int pointsCount = 100;
	BezierPointCurve m_curve;
	public IPointCurve curve;
	public bool normalize = false;
	void OnDrawGizmos(){
		if (m_curve == null) {
			var bezierCurveControlPoints = new List<Point> ();
			m_curve = new BezierPointCurve ();
			m_curve.controlPoints = bezierCurveControlPoints;
		}

		m_curve.controlPoints.Clear ();
		foreach (var controlPoint in controlPoints) {
			m_curve.controlPoints.Add (Point.FromWorldTransform (controlPoint));
		}
		curve = m_curve;
		if (normalize) {
			curve = new NormalizedPointCurve (curve);
		}

		float pointIndexToT = 1f / pointsCount;
		for (int i = 0; i < pointsCount; i++) {
			float tA = i * pointIndexToT;
			float tB = (i+1) * pointIndexToT;

			var pointA = curve.Evaluate (tA);
			var pointB = curve.Evaluate (tB);
			Gizmos.DrawLine (pointA.position, pointB.position);
			Gizmos.DrawWireSphere (pointA.position, 0.01f);
		}
	}
}
