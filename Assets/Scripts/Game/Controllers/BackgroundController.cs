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


        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            _playerCamera = player.GetComponentInChildren<Camera>();
            _backgrounds = this.gameObject.FindChildObjects("background");
            UpdateCameraBounds();
            UpdateBackgroundBounds();
            _spriteWidthHalf = _backgrounds.First().GetComponentInChildren<SpriteRenderer>().size.x / 2;
        }

        private void Update()
        {
            UpdateCameraBounds();
            Repeat();
        }

        private void Repeat()
        {
            if(_cameraXmin < _backgroundXmin +_playerCamera.orthographicSize)
            {
                var copyOf = _backgrounds.First();
                var newPos = copyOf.transform.position;
                newPos.x = _backgroundXmin - _spriteWidthHalf;
                AddBackground(newPos, copyOf);
            }
            if (_cameraXmax > _backgroundXmax - _playerCamera.orthographicSize)
            {
                var copyOf = _backgrounds.First();
                var newPos = copyOf.transform.position;
                newPos.x = _backgroundXmax + _spriteWidthHalf;
                AddBackground(newPos, copyOf);
            }

        }

        private void AddBackground(Vector3 pos, GameObject copyOf)
        {
            var newObj = Instantiate(copyOf, pos, new Quaternion(), this.gameObject.transform);
            _backgrounds.Add(newObj);
            UpdateBackgroundBounds();
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
            _backgrounds.ForEach(x =>
            {
                var xPos = x.transform.position.x;
                var sprite = x.GetComponentInChildren<SpriteRenderer>();
                var spriteWidthHalf = sprite.size.x/2;
                if (xPos - spriteWidthHalf < xMin) xMin = xPos - spriteWidthHalf;
                if (xPos + spriteWidthHalf > xMax) xMax = xPos + spriteWidthHalf;
            });

            _backgroundXmin = xMin;
            _backgroundXmax = xMax;
        }


    }
}
