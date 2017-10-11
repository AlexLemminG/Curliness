using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizedPointCurve : IPointCurve {
	private IPointCurve m_originalCurve;
	private IFloatCurve m_tNormalizer;
	private const int c_approximationPointsCount = 100;

	public NormalizedPointCurve (IPointCurve m_originalCurve)
	{
		this.m_originalCurve = m_originalCurve;

		float totalLength = 0f;
		float pointIndexToT = 1f / c_approximationPointsCount;

		float t = 0f;
		Point pointB = m_originalCurve.Evaluate (0);
		Point pointA;
//		Debug.Log ("begin");
		FloatSpline tNormalizerInv = new FloatSpline ();
		for (int i = 1; i <= c_approximationPointsCount; i++) {
			t = i * pointIndexToT;

			pointA = pointB;
			pointB = m_originalCurve.Evaluate (t);

			float distanceBetweenPoints = Vector3.Distance (pointA.position, pointB.position);

			float newTotalLength = totalLength + distanceBetweenPoints;
			tNormalizerInv.curves.Add (new LinearFloatCurve (totalLength, newTotalLength));
			totalLength = newTotalLength;
//			Debug.Log (totalLength);
		}
		float totalLengthInv = 1f / totalLength;
//		float lastB = 0f;
		foreach (LinearFloatCurve linearCurve in tNormalizerInv.curves) {
			linearCurve.a *= totalLengthInv;
			linearCurve.b *= totalLengthInv;
//			lastB = linearCurve.b;
		}
		var tNormalizerValues = new float[c_approximationPointsCount+1];
		for (int i = 0; i < tNormalizerValues.Length; i++) {
			tNormalizerValues [i] = FloatSplineMath.FindClosest (tNormalizerInv, i * pointIndexToT);
//			lastB = tNormalizerValues [i];
		}

		FloatSpline tNormalizer = new FloatSpline ();
		for (int i = 0; i < tNormalizerValues.Length - 1; i++) {
			float valA = tNormalizerValues [i];
			float valB = tNormalizerValues [i+1];
			var linearCurve = new LinearFloatCurve (valA, valB);
//			lastB = linearCurve.b;
			tNormalizer.curves.Add (linearCurve);
		}
//		Debug.Log (lastB);

		m_tNormalizer = tNormalizer;
	}
	

	public Point Evaluate (float t) {
//		return m_originalCurve.Evaluate (t);
		return m_originalCurve.Evaluate (m_tNormalizer.Evaluate (t));
	}
}
