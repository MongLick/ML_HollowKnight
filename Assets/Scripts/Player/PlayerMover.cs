using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
	[SerializeField] Rigidbody2D rigid;

	[SerializeField] float moveSpeed;
	[SerializeField] float jumpPower; // �⺻ ���� ����
	[SerializeField] float jumpChargingRate; // ���� ��¡ �ӵ�
	[SerializeField] float jumpChargingMax; // �ִ� ���� ����
	[SerializeField] float currentJumpPower; // �ջ� �� ���� ����

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
