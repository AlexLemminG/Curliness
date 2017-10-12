using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatSplineMath {
	const int c_iterationsCount = 10;
	public static float FindClosest(FloatSpline sameDerivativeSignSpline, float targetValue){
		float stepSize = 0.5f;
		float result = 0.5f;
		for (int i = 0; i < c_iterationsCount; i++) {
			float left = result - stepSize;
			float middle = result;
			float right = result + stepSize;

			float leftValue = sameDerivativeSignSpline.Evaluate (left);
			float middleValue = sameDerivativeSignSpline.Evaluate (middle);
			float rightValue = sameDerivativeSignSpline.Evaluate (right);

			float leftError = Mathf.Abs (targetValue - leftValue);
			float middleError = Mathf.Abs (targetValue - middleValue);
			float rightError = Mathf.Abs (targetValue - rightValue);

			if (middleError <= leftError && middleError <= rightError) {
				result = middle;
			} else {
				if (leftError <= rightError) {
					result = left;
				} else {
					result = right;
				}
			}
			stepSize *= 0.5f;
		}
		result = Mathf.Clamp01 (result);
		return result;
	}
}
