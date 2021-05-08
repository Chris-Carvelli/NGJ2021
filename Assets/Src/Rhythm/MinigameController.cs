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
		
		private void Start() {
			_rhythmSystem = RhythmSystem.Instance;

			lastInputPresses = inputNames.Select(_ => 999f).ToArray();
		}

		private void Update() {
			for (var i = 0; i < inputNames.Length; i++) {
				var input = inputNames[i];
				if (Input.GetButtonDown(input)) {
					var rawPress = _rhythmSystem.Time;
					var snappedTime = Mathf.Round(rawPress);
					var dSnap = Mathf.Abs(rawPress - snappedTime);


					lastInputPresses[i] = dSnap < tolerance ? snappedTime : rawPress;
				}
			}
		}
	}
}