using System;
using Unity.Collections;
using UnityEngine;

namespace Src.Rhythm {
	
	public class RhythmSystem : MonoBehaviour {
		// singleton
		public static RhythmSystem Instance { get; private set; }
		[Header("Song Config")]
		//Song beats per minute
		//This is determined by the song you're trying to sync up to
		public float songBpm;

		public bool looping;

		public float beatsShownInAdvance;

		public NoteVisuals[] notes;
		public MinigameController controller;

		//keep all the position-in-beats of notes in the song
		private float[][] _channels = new [] {
			new [] { 0f, 8 },
			new [] { 2f, 14 },
			new [] { 2f, 10 },
			new [] { 4f, 20 },
		};

		public int[] _channelIdxs;
		public int[] _channelLoops;
		
		[Header("Debug")]
		//the current position of the song (in seconds)
		public float songPosition;

		//the current position of the song (in beats)
		public float songPosInBeats;

		//the duration of a beat
		public float secPerBeat;

		//how much time (in seconds) has passed since the song started
		public float dsptimesong;
		
		// references
		private AudioSource _musicSource;

		private bool _playing;

		private void Awake() {
			DontDestroyOnLoad(gameObject);
			
			if (Instance is null)
				Instance = this;
			else
				Destroy(gameObject);
		}

		private void Init() {
			//Load the AudioSource attached to the Conductor GameObject
			_musicSource = GetComponent<AudioSource>();

			//Calculate the number of seconds in each beat
			secPerBeat = 60f / songBpm;

			_channelIdxs = new int[_channels.Length];
			_channelLoops = new int[_channels.Length];

			//Start the music
			dsptimesong = (float)AudioSettings.dspTime;
			_musicSource.Play();

			_playing = true;
		}

		private void Update() {
			if (Input.GetButton("Jump"))
				Init();
			
			if (_playing) {
				HandleInputs();
				UpdateSongState();
				Spawn();
			}
		}

		private void HandleInputs() {
			for (var i = 0; i < controller.lastInputPresses.Length; i++) {
				var press = controller.lastInputPresses[i];
				var channel = _channels[i];
				var nextIndex = _channelIdxs[i];
				var channelOffset = (channel[1] - channel[0]) * _channelLoops[i];

				notes[i].successful = press >= notes[i]._attackT && press <= notes[i]._sustainT;
			}
		}

		private void UpdateSongState() {
			//calculate the position in seconds
			songPosition = (float) (AudioSettings.dspTime - dsptimesong);

			//calculate the position in beats
			songPosInBeats = songPosition / secPerBeat;
		}

		private void Spawn() {
			for (var i = 0; i < _channels.Length; i++) {
				var channel = _channels[i];
				var nextIndex = _channelIdxs[i];
				var channelOffset = (channel[1] - channel[0]) * _channelLoops[i];
				var pos = channel[nextIndex] + channelOffset;
				
				if (pos < Time + beatsShownInAdvance)
				{
					notes[i].Impulse(pos);
					
					_channelIdxs[i] = _channelIdxs[i]++ % channel.Length;
					_channelLoops[i]++;
				}
			}
		}


		public float Time => songPosInBeats;
	}
}