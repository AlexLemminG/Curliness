using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour {
	[Range(0f, 1f)]
	public float t;
	public BezierCurveVisualizer bezierCurve;
	void Update () {
		bezierCurve.curve.Evaluate (t).ToWorldTransform(transform);
	}
}
