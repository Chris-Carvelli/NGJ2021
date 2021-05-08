﻿using UnityEngine;

namespace Src.Rhythm {
	public enum State {
		Off,
		Attack,
		Sustain,
		Release
	}
	
	public class NoteVisuals : MonoBehaviour {
		public State state;

		public SO_NoteFXSection attackFX;
		public SO_NoteFXSection sustainFX;
		public SO_NoteFXSection releaseFX;
		private Vector3 _startPos;
		private Quaternion _startRot;
		private Vector3 _startScale;
		private Color _startColor;
		
		public float attackDuration;
		public float sustainDuration;
		public float releaseDuration;
		
		[Header("Debug")]
		public float attackTime;
		public float sustainTime;
		public float releaseTime;
		
		public float _attackT;
		public float _sustainT;
		public float _releaseT;
		public float sysTime;

		private RhythmSystem _rhythmSystem;
		private SpriteRenderer _renderer;
		
		private void Start() {
			_rhythmSystem = RhythmSystem.Instance;
			_renderer = GetComponent<SpriteRenderer>();

			_startPos = transform.localPosition;
			_startRot = transform.localRotation;
			_startScale = transform.localScale;
			
			_startColor = _renderer.color;
		}

		private void Update() {
			attackTime = (attackDuration - (_attackT - _rhythmSystem.Time)) / attackDuration;
			sustainTime = (sustainDuration - (_sustainT - _rhythmSystem.Time)) / sustainDuration;
			releaseTime = (releaseDuration - (_releaseT - _rhythmSystem.Time)) / releaseDuration;

			float t;
			SO_NoteFXSection fx = attackFX;
			Vector3 aPos = _startPos;
			Vector3 aScale = _startScale;
			Quaternion aRot;
			Color aColor;
			if (attackTime < 1) {
				t = attackTime;
				state = State.Attack;
				fx = attackFX;
				aPos = _startPos;
				aRot = _startRot;
				aScale = _startScale;
				aColor = _startColor;
			}
			else if (sustainTime < 1) {
				t = sustainTime;
				state = State.Sustain;
				fx = sustainFX;
				aScale = _startScale * attackFX.scaleFactor;
				aColor = attackFX.targetColor;
			}
			else if (releaseTime < 1) {
				t = releaseTime;
				state = State.Release;
				fx = releaseFX;
				aScale = _startScale * sustainFX.scaleFactor;
				aColor = sustainFX.targetColor;
			}
			else {
				t = 1;
				state = State.Off;
				fx = releaseFX;
				aScale = _startScale;
				aColor = _startColor;
			}

			transform.localScale = fx.GetScale(_startScale, aScale, t);
			_renderer.color = fx.GetColor(aColor, t);
		}

		public void Spawn() { }
		
		public void Despawn() { }

		public void Impulse(float pT) {
			print($"impulse {name}");
			_attackT = pT + attackDuration;
			_sustainT = pT + attackDuration + sustainDuration;
			_releaseT = pT + attackDuration + sustainDuration + releaseDuration;
		}
	}
}