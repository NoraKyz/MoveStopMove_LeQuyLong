using _Framework.Pool.Scripts;
using _Game.Scripts.Utils;
using _Pattern;
using UnityEngine;

namespace _Game.Scripts.Weapon.Bullet
{
    public class Bullet : GameUnit
    {
        [SerializeField] private float moveSpeed;
        
        private Character.Character _owner;
        
        // movement
        private Vector3 _startPos;
        private Vector3 _targetPos;
        private Vector3 _moveDirection;
        private float _maxFlyDistance;
        
        private void Update()
        {
            Move();

            if (CanDespawn())
            {
                Despawn();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character.Character character = Cache<Character.Character>.GetComponent(other);
                
                if (character != _owner)
                {
                    character.OnHit();
                    Despawn();
                }
            }
            else
            {
                Despawn(); // trigger with platform
            }
        }
        
        public void OnInit(Character.Character owner, Vector3 targetPos)
        {
            _owner = owner;
            _startPos = TF.position;
            _targetPos = targetPos;
            _moveDirection = (_targetPos - _startPos).normalized;
            _maxFlyDistance = owner.AttackRange;
            
            TF.localScale = Vector3.one * owner.Size;
        }
        private void Despawn()
        {
            SimplePool.Despawn(this);
        }
        protected virtual void Move()
        {
            TF.position += _moveDirection * (moveSpeed * Time.deltaTime);
        }
        protected virtual bool CanDespawn()
        {
            float flyLength = Vector3.Distance(_startPos, TF.position);
            return flyLength >= _maxFlyDistance;
        }
    }
}