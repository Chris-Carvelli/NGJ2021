using UnityEngine;

namespace Src.Rhythm {
	[CreateAssetMenu(fileName = "new choreography", menuName = "Data/Choreography", order = 0)]
	public class SO_Choreography : ScriptableObject {
		public AudioClip music;
		
		public float bpm;
		
		public float[] channel0;
		public float[] channel1;
		public float[] channel2;
		public float[] channel3;

		public float successScore = 1;
		public float failScore = 1;
		public float targetScore;

		public float[][] GetChannels() {
			return new [] {
				channel0,
				channel1,
				channel2,
				channel3,
			};
		}
	}
}