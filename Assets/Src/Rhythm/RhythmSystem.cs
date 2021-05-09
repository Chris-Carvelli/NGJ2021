using System;
using Unity.Collections;
using UnityEngine;

namespace Src.Rhythm {
	
	public class RhythmSystem : MonoBehaviour {
		// singleton
		public static RhythmSystem Instance { get; private set; }
		
		// references
		public AudioSource musicSource;
		
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
			new [] { 4f, 10 },
			new [] { 5f, 11 },
			new [] { 6f, 12 },
			new [] { 4f, 20 },
		};

		public SO_Choreography choreography;

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
			musicSource = GetComponent<AudioSource>();
			musicSource.loop = looping;
			musicSource.clip = choreography.music;
			songBpm = choreography.bpm;
			_channels = choreography.GetChannels();
			
			//Calculate the number of seconds in each beat
			secPerBeat = 60f / songBpm;

			_channelIdxs = new int[_channels.Length];
			_channelLoops = new int[_channels.Length];

			//Start the music
			dsptimesong = (float)AudioSettings.dspTime;
			musicSource.Play();

			_playing = true;
		}

		public void StartSong(SO_Choreography pChoreography, NoteVisuals[] visuals) {
			choreography = pChoreography;
			notes = visuals;
			Init();
		}

		public void StopSong() {
			_playing = false;
			musicSource.Pause();
		}

		private void Update() {
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

				if (controller.pressedThisFrame[i])
					notes[i].score += notes[i].successful ? choreography.successScore : choreography.failScore;
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