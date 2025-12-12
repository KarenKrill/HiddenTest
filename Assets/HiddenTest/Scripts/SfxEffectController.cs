using UnityEngine;
using DG.Tweening;
using Zenject;
using HiddenTest.Gameplay.Abstractions;
using KarenKrill.Audio.Abstractions;
using System.Collections.Generic;

namespace HiddenTest
{
    public class SfxEffectController : MonoBehaviour
    {
        [Inject]
        public void Initialize(IGameTaskController<IHiddenObject> taskController,
            IAudioController audioController)
        {
            _taskController = taskController;
            _audioController = audioController;
        }

        [SerializeField]
        private List<AudioClip> _objectFoundAudioClips = new();

        private IGameTaskController<IHiddenObject> _taskController;
        private IAudioController _audioController;

        private void OnEnable()
        {
            _taskController.TaskRemoved += OnTaskRemoved;
        }

        private void OnDisable()
        {
            _taskController.TaskRemoved -= OnTaskRemoved;
        }

        private void OnTaskRemoved(IGameTask<IHiddenObject> gameTask)
        {
            if (_objectFoundAudioClips.Count > 0)
            {
                _audioController.PlaySfx(_objectFoundAudioClips[Random.Range(0, _objectFoundAudioClips.Count)]);
            }
        }
    }
}
