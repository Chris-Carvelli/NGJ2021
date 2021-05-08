using System;
using UnityEngine;

namespace Src.Rhythm {
	
	[Serializable]
	public struct SO_NoteFXSection {
		public AnimationCurve ease;
		public Vector3 posOffset;
		public Quaternion rotationOffset;
		public float scaleFactor;
		public Color targetColor;

		public Vector3 GetPos(Vector3 startPos, float t) =>
			Vector3.Lerp(startPos, startPos + posOffset, ease.Evaluate(t));
		
		public Quaternion GetRot(Quaternion startPos, Quaternion aRot, float t) =>
			Quaternion.Lerp(startPos, aRot * rotationOffset, t);
		
		public Vector3 GetScale(Vector3 startScale, Vector3 aScale, float t) =>
			Vector3.Lerp(aScale, startScale * scaleFactor, ease.Evaluate(t));

		public Color GetColor(Color aColor, float t) =>
			Color.Lerp(aColor, targetColor, ease.Evaluate(t));
	}
}