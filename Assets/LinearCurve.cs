using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearCurve : IPointCurve {
	public Point a;
	public Point b;

	public Point Evaluate (float t){
		return Point.Lerp (a, b, t);
	}
}
