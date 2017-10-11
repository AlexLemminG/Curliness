using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatSpline : IFloatCurve{
	public List<IFloatCurve> curves = new List<IFloatCurve>();

	public float Evaluate (float t)
	{
		int curveIndex = Mathf.Clamp ((int)(t * curves.Count), 0, curves.Count - 1);
		var targetCurve = curves [curveIndex];
		float targetCurveT = t * curves.Count - curveIndex;
		return targetCurve.Evaluate (targetCurveT);
	}
}
