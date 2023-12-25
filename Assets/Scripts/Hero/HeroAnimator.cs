using System;
using SimpleRPG.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleRPG.Hero
{
  public class HeroAnimator : MonoBehaviour, IAnimationStateReader
  {
    
    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;
    
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    
    public AnimatorState State { get; private set; }
    
    private readonly int _moveHash = Animator.StringToHash("Walking");
    private readonly int _attackHash = Animator.StringToHash("AttackNormal");
    private readonly int _hitHash = Animator.StringToHash("Hit");
    private readonly int _dieHash = Animator.StringToHash("Die");
    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _idleStateFullHash = Animator.StringToHash("Base Layer.Idle");
    private readonly int _attackStateHash = Animator.StringToHash("Attack Normal");
    private readonly int _walkingStateHash = Animator.StringToHash("Run");
    private readonly int _deathStateHash = Animator.StringToHash("Die");
    
    private void Update()
    {
      _animator.SetFloat(_moveHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
    }

    public bool IsAttacking => State == AnimatorState.Attack;
    

    public void PlayHit() => _animator.SetTrigger(_hitHash);
    
    public void PlayAttack() => _animator.SetTrigger(_attackHash);

    public void PlayDeath() =>  _animator.SetTrigger(_dieHash);

    public void ResetToIdle() => _animator.Play(_idleStateHash, -1);
    
    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExited?.Invoke(StateFor(stateHash));
    
    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
        state = AnimatorState.Idle;
      else if (stateHash == _attackStateHash)
        state = AnimatorState.Attack;
      else if (stateHash == _walkingStateHash)
        state = AnimatorState.Walking;
      else if (stateHash == _deathStateHash)
        state = AnimatorState.Died;
      else
        state = AnimatorState.Unknown;
      
      return state;
    }
  }
}