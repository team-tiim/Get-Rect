using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game.Controllers
{
    public class BackgroundController : MonoBehaviour
    {
        public float speed = 1;
        

        private Camera _playerCamera;
        private List<GameObject> _backgrounds;
        private float _cameraXmin;
        private float _cameraXmax;
        private float _backgroundXmin;
        private float _backgroundXmax;
        private float _spriteWidthHalf;
        private Vector2 _lastPos;
        private string _originalName;


        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            _playerCamera = player.GetComponentInChildren<Camera>();
            _backgrounds = this.gameObject.FindChildObjects("background");
            _originalName = _backgrounds.First().name;
            UpdateCameraBounds();
            UpdateBackgroundBounds();
            _spriteWidthHalf = _backgrounds.First().GetComponentInChildren<SpriteRenderer>().size.x / 2;
            _lastPos = _playerCamera.transform.position;
        }

        private void Update()
        {
            MoveLayer();
            UpdateCameraBounds();
            Repeat();
            _lastPos = _playerCamera.transform.position;

        }

        private void Repeat()
        {
            if(_cameraXmin < _backgroundXmin +_playerCamera.orthographicSize)
            {
                var original = _backgrounds.First();
                var newPos = original.transform.position;
                newPos.x = _backgroundXmin - _spriteWidthHalf;
                AddBackground(newPos, original, false);
                _backgroundXmin -= _spriteWidthHalf * 2;

            }
            if (_cameraXmax > _backgroundXmax - _playerCamera.orthographicSize)
            {
                var original = _backgrounds.First();
                var newPos = original.transform.position;
                newPos.x = _backgroundXmax + _spriteWidthHalf;
                AddBackground(newPos, original, true);
                _backgroundXmax += _spriteWidthHalf * 2;
            }

        }

        private void AddBackground(Vector3 pos, GameObject copyOf,bool last)
        {
            var maxDelta = _playerCamera.orthographicSize * 2;
            var newObj = Instantiate(copyOf, pos, new Quaternion(), this.gameObject.transform);
            newObj.name = _originalName;
            if (last)
            {
                _backgrounds.Add(newObj);
                if(_backgroundXmin+(_spriteWidthHalf*2)< _cameraXmin - maxDelta)
                {
                    RemoveObj(_backgrounds.First());
                    _backgroundXmin += _spriteWidthHalf * 2;
                }

            }
            else
            {
                _backgrounds.Insert(0, newObj);
                if (_backgroundXmax - (_spriteWidthHalf * 2) > _cameraXmax + maxDelta)
                {
                    RemoveObj(_backgrounds.Last());
                    _backgroundXmax -= _spriteWidthHalf * 2;
                }
            }
            
        }

        private void RemoveObj(GameObject remove)
        {
            _backgrounds.Remove(remove);
            Destroy(remove);
        }

        private void UpdateCameraBounds()
        {
            var height = _playerCamera.orthographicSize;
            var width = height * _playerCamera.aspect;
            _cameraXmin = _playerCamera.transform.position.x - width;
            _cameraXmax = _playerCamera.transform.position.x + width;
        }

        private void UpdateBackgroundBounds()
        {
            var xMin = float.MaxValue;
            var xMax = float.MinValue;
            var newlist = new List<GameObject>();
            _backgrounds.ForEach(x =>
            {
                var xPos = x.transform.position.x;
                var sprite = x.GetComponentInChildren<SpriteRenderer>();
                var spriteWidthHalf = sprite.size.x/2;
                if (xPos - spriteWidthHalf < xMin)
                {
                    xMin = xPos - spriteWidthHalf;
                    newlist.Insert(0, x);
                }
                if (xPos + spriteWidthHalf > xMax)
                {
                    xMax = xPos + spriteWidthHalf;
                    newlist.Add(x);
                }
            });

            _backgroundXmin = xMin;
            _backgroundXmax = xMax;
        }

        

        public void MoveLayer()
        {
            if (_lastPos.x != _playerCamera.transform.position.x && (1 - speed) != 0)
            {
                var newPos = this.transform.position;
                var deltaX = (_playerCamera.transform.position.x - _lastPos.x) * (1 - speed);
                var deltaY = (_playerCamera.transform.position.y - _lastPos.y) * (1 - speed);
                newPos.x += deltaX;
                newPos.y += deltaY;
                this.transform.position = newPos;
                _backgroundXmin += deltaX;
                _backgroundXmax += deltaX;
            }
        }
    }
}
