using System;
using System.Linq;
using UnityEngine;

namespace Src.Rhythm {
	public class MinigameController : MonoBehaviour {
		private RhythmSystem _rhythmSystem;

		public string[] inputNames = new[] {
			"MinigameLeft",
			"MinigameRight",
			"MinigameUp",
			"MinigameDown",
		};
		
		public bool inputSnap;
		public float tolerance = .2f;

		public float[] lastInputPresses;
		public bool[] pressedThisFrame;
		
		private void Start() {
			_rhythmSystem = RhythmSystem.Instance;

			lastInputPresses = inputNames.Select(_ => 999f).ToArray();
			pressedThisFrame = new bool[inputNames.Length];
		}

		private void Update() {
			for (var i = 0; i < inputNames.Length; i++) {
				var input = inputNames[i];
				pressedThisFrame[i] = false;
				if (Input.GetButtonDown(input)) {
					pressedThisFrame[i] = true;
					var rawPress = _rhythmSystem.Time;
					var snappedTime = Mathf.Round(rawPress);
					var dSnap = Mathf.Abs(rawPress - snappedTime);


					lastInputPresses[i] = dSnap < tolerance ? snappedTime : rawPress;
				}
			}
		}
	}
}