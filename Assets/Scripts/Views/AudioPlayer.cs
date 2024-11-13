using System;
using Events;
using Extensions.Unity.MonoHelper;
using Installers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Views
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : EventListenerMono
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _pitchedAudioSource;
        private Settings _settings;

        private void Awake()
        {
            _settings = GameInstaller.Ins.GameSettings.AudioPlayerSettings;
        }

        private void PlayAudio(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
        
        private void PlayAudio(AudioClip audioClip, float pitch)
        {
            _pitchedAudioSource.pitch = pitch;
            
            _pitchedAudioSource.PlayOneShot(audioClip);
        }

        protected override void RegisterEvents()
        {
            CardEvents.PreCardOpen += OnPreCardOpen;
            GridEvents.Match += OnMatch;
            GridEvents.GridComplete += OnGridComplete;
        }

        private void OnPreCardOpen()
        {
            PlayAudio(_settings.FlipSfx, Random.Range(0.9f, 1.1f));
        }

        private void OnMatch()
        {
            PlayAudio(_settings.MatchSfx);
        }

        private void OnGridComplete()
        {
            PlayAudio(_settings.LevelCompleteSfx);
        }

        protected override void UnRegisterEvents()
        {
            CardEvents.PreCardOpen -= OnPreCardOpen;
            GridEvents.Match -= OnMatch;
            GridEvents.GridComplete -= OnGridComplete;
        }

        [Serializable]
        public class Settings
        {
            public AudioClip MatchSfx => _matchSfx;
            public AudioClip FlipSfx => _flipSfx;
            public AudioClip LevelCompleteSfx => _levelCompleteSfx;
            [SerializeField] private AudioClip _matchSfx;
            [SerializeField] private AudioClip _flipSfx;
            [SerializeField] private AudioClip _levelCompleteSfx;
        }
    }
}