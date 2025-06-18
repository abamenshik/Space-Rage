using UnityEngine;
using UnityEngine.Events;

namespace Components.Animation
{
    [System.Serializable]
    public class NamedAnimation
    {
        [SerializeField] private string name;
        [SerializeField] private bool loop;
        [SerializeField] private bool allowNext;
        [SerializeField] private Sprite[] animation;
        [SerializeField] private UnityEvent animComlete;

        public string Name => name;
        public bool Loop => loop;
        public bool AllowNext => allowNext;
        public Sprite[] Animation => animation;
        public UnityEvent AnimComlete => animComlete;
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField][Range(0, 60)] private int _frameRate;
        [SerializeField] private NamedAnimation[] _allAnim;

        private float _nextFrameTime;
        private float _secondsPerFrame;
        private int _currentSpriteIndex;
        private int _currentAnimationIndex;
        private SpriteRenderer _spr;
        private bool _isPlaying;

        private void Start()
        {
            _spr = GetComponent<SpriteRenderer>();
            _secondsPerFrame = 1f / _frameRate;
            StartAnimation();
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }
        private void OnBecameInvisible()
        {
            enabled = false;
        }
        public void SetClip(string name)
        {
            for (int i = 0; i < _allAnim.Length; i++)
            {
                if (_allAnim[i].Name.Equals(name))
                {
                    _currentAnimationIndex = i;
                    StartAnimation();
                    return;
                }
            }
        }
        private void StartAnimation()
        {
            _nextFrameTime = Time.time;
            enabled = _isPlaying = true;
            _currentSpriteIndex = 0;
        }
        private void OnEnable()
        {
            _nextFrameTime = Time.time;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;
            NamedAnimation clip = _allAnim[_currentAnimationIndex];
            if (_currentSpriteIndex >= clip.Animation.Length)
            {
                if (clip.Loop)
                    _currentSpriteIndex = 0;
                else
                {
                    enabled = _isPlaying = clip.AllowNext;
                    clip.AnimComlete?.Invoke();
                    if (clip.AllowNext)
                    {
                        _currentSpriteIndex = 0;
                        _currentAnimationIndex = (int)Mathf.Repeat(_currentAnimationIndex + 1, _allAnim.Length);
                    }
                }
                return;
            }
            _spr.sprite = clip.Animation[_currentSpriteIndex];
            _nextFrameTime += _secondsPerFrame;
            _currentSpriteIndex++;
        }
    }
}