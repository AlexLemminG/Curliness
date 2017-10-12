using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPointCurve : IPointCurve{
	public List<Point> controlPoints = new List<Point>();

	List<Point> m_supportingList = new List<Point>();

	public BezierPointCurve (List<Point> controlPoints)
	{
		this.controlPoints = new List<Point>(controlPoints);
	}
	public BezierPointCurve ()
	{
	}
	

	public Point Evaluate(float t){
		while (m_supportingList.Count < controlPoints.Count) {
			m_supportingList.Add (Point.identity);
		}
		for (int i = 0; i < controlPoints.Count; i++) {
			m_supportingList [i] = controlPoints [i].Copy();
		}

		for (int i = controlPoints.Count-1; i >= 0; i--) {
			for (int j = 0; j <= i - 1; j++) {
				var pointA = m_supportingList [j];
				var pointB = m_supportingList [j+1];
				var newControlPoint = Point.Lerp (pointA, pointB, t);
				m_supportingList [j] = newControlPoint;
			}
		}
		return m_supportingList [0];
	}
}
