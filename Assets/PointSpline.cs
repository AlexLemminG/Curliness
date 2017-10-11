using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpline : IPointCurve{
	public List<IPointCurve> curves;

	public Point Evaluate (float t)
	{
		int curveIndex = Mathf.Clamp ((int)(t * curves.Count), 0, curves.Count - 1);
		var targetCurve = curves [curveIndex];
		float targetCurveT = t * curves.Count - (int)(t * curves.Count);
		return targetCurve.Evaluate (targetCurveT);
	}
}
