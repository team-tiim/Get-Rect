using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Core.Utils
{
    public class AISight
    {
        private float _distance;
        private float _midDistance;
        private float _range;
        private CapsuleCollider2D _capsule;
        private RaycastHit2D _upDownRay;
        private RaycastHit2D _downUpRay;
        private RaycastHit2D _midDownRay;
        private RaycastHit2D _midUpRay;
        private RaycastHit2D _midDownMidRay;
        private Vector2 _lastPos;
        private float _edgeDistance;

        public AISight(CapsuleCollider2D capsule, float range)
        {
            _capsule = capsule;
            _range = range;
            _distance = (float)Math.Sqrt(Math.Pow(_range, 2) + Math.Pow(_capsule.size.y / 2, 2));
            _midDistance = (float)Math.Sqrt(Math.Pow(_range/2, 2) + Math.Pow(_capsule.size.y / 2, 2));
        }

        public void UpdateRays(int direction)
        {
            var rayheight = _capsule.size.y / 2;
            var position = new Vector2(_capsule.transform.position.x, _capsule.transform.position.y) + _capsule.offset;
            var upPos = new Vector2(position.x, position.y + rayheight);
            var downPos = new Vector2(position.x, position.y - rayheight);
            var angleMidDown = new Vector2(_range * direction, -rayheight);
            var angleMidDownMid = new Vector2(_range * direction/2, -rayheight);
            var angleMidUp = new Vector2(_range * direction, rayheight);
            var angleUpDown = new Vector2(_range * direction, -rayheight);
            var angleDownUp = new Vector2(_range * direction, rayheight);
            _upDownRay = Physics2D.Raycast(upPos, angleUpDown, _distance);
            _downUpRay = Physics2D.Raycast(downPos, angleDownUp, _distance);
            _midDownRay = Physics2D.Raycast(position, angleMidDown, _distance+1);
            _midUpRay = Physics2D.Raycast(position, angleMidUp, _distance);
            _midDownMidRay = Physics2D.Raycast(position, angleMidDownMid, _midDistance+1);

            Debug.DrawRay(position, angleMidDown, _midDownRay ? Color.red : Color.green);
            Debug.DrawRay(position, angleMidUp, _midUpRay ? Color.red : Color.green);
            Debug.DrawRay(upPos, angleUpDown, _upDownRay ? Color.red : Color.green);
            Debug.DrawRay(downPos, angleDownUp,_downUpRay  ? Color.red : Color.green);
            Debug.DrawRay(position, angleMidDownMid, _midDownMidRay ? Color.red : Color.green);
        }


    }
}
