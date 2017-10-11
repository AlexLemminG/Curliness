using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearFloatCurve : IFloatCurve{
	public float a;
	public float b;
	public LinearFloatCurve (float a, float b)
	{
		this.a = a;
		this.b = b;
	}
	
	public float Evaluate (float t){
		return Mathf.Lerp (a, b, t);
	}
}
