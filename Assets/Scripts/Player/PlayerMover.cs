using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
	[SerializeField] Rigidbody2D rigid;

	[SerializeField] float moveSpeed;
	[SerializeField] float jumpPower; // 기본 점프 높이
	[SerializeField] float jumpChargingRate; // 점프 차징 속도
	[SerializeField] float jumpChargingMax; // 최대 점프 높이
	[SerializeField] float currentJumpPower; // 합산 된 점프 높이

	Vector2 moveDir;
	Coroutine jumpChargingRoutine;

	private void Update()
	{
		Move();
	}

	private void OnMove(InputValue value)
	{
		Vector2 input = value.Get<Vector2>();
		moveDir.x = input.x;
		moveDir.y = input.y;
	}

	private void OnJump(InputValue value)
	{
		if (value.isPressed)
		{
			jumpChargingRoutine = StartCoroutine(JumpChargingRoutine());
		}
		else
		{
			StopCoroutine(jumpChargingRoutine);
			Jump();
		}
	}

	private void Move()
	{
		transform.Translate(moveDir * moveSpeed * Time.deltaTime);
	}

	private void Jump()
	{
		Vector2 velocity = rigid.velocity;
		velocity.y = currentJumpPower;
		rigid.velocity = velocity;
		currentJumpPower = jumpPower;
	}

	IEnumerator JumpChargingRoutine()
	{
		currentJumpPower = jumpPower;
		while (Input.GetKey(KeyCode.Z))
		{
			currentJumpPower += jumpChargingRate * Time.deltaTime;
			if (currentJumpPower >= jumpChargingMax)
			{
				currentJumpPower = jumpChargingMax;
				Jump();
				yield break;
			}
			yield return null;
		}
	}
}
