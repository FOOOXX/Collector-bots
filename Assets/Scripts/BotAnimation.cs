using UnityEngine;

[RequireComponent(typeof(Animator))]

public class BotAnimation : MonoBehaviour 
{
	private const string IsWalking = nameof(IsWalking);

	private Animator _animator;
	private int _isWalking;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_isWalking = Animator.StringToHash(IsWalking);
	}

	public void StartAnimation(bool isBusy)
	{
		_animator.SetBool(_isWalking, isBusy != false);
	}
}
