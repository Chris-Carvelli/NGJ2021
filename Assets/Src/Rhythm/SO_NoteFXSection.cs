using System;
using UnityEngine;

namespace Src.Rhythm {
	
	[Serializable]
	public struct SO_NoteFXSection {
		public Vector3 posOffset;
		public AnimationCurve ease;
		// public float rotFactor;
		// public AnimationCurve rotEase;
		public float scaleFactor;
		public Color targetColor;

		public Vector3 GetPos(Vector3 startPos, float t) =>
			Vector3.Lerp(startPos, startPos + posOffset, ease.Evaluate(t));
		
		// public Vector3 GetRot(Quaternion startPos, float t) =>
		// 	Quaternion.Lerp(startPos, startPos * posFactor, t);
		
		public Vector3 GetScale(Vector3 startScale, Vector3 aScale, float t) =>
			Vector3.Lerp(aScale, startScale * scaleFactor, ease.Evaluate(t));

		public Color GetColor(Color aColor, float t) =>
			Color.Lerp(aColor, targetColor, ease.Evaluate(t));
	}
}