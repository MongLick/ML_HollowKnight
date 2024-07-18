using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] SpriteRenderer render;
	[SerializeField] Animator animator;
	[SerializeField] GameObject[] Attack;

	[SerializeField] float moveSpeed;
	[SerializeField] float jumpPower; // 기본 점프 높이
	[SerializeField] float jumpChargingRate; // 점프 차징 속도
	[SerializeField] float jumpChargingMax; // 최대 점프 높이
	[SerializeField] float currentJumpPower; // 합산 된 점프 높이
	[SerializeField] float attackSpeed;
	[SerializeField] LayerMask groundCheckLayer;

	Vector2 lastMoveDir;
	Vector2 moveDir;
	Coroutine jumpChargingRoutine;
	private bool isGround;
	private bool lookUp;
	private bool lookDown;
	private bool isAttack = true;

	private void Update()
	{
		Move();
		animator.SetFloat("YSpeed", rigid.velocity.y);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (groundCheckLayer.Contain(collision.gameObject.layer))
		{
			isGround = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (groundCheckLayer.Contain(collision.gameObject.layer))
		{
			isGround = false;
		}
	}

	private void OnMove(InputValue value)
	{
		moveDir = value.Get<Vector2>();

		if(moveDir != Vector2.zero)
		{
			lastMoveDir = moveDir;
		}
		if (moveDir.x > 0)
		{
			render.flipX = true;
			animator.SetBool("Run", true);
		}
		else if (moveDir.x < 0)
		{
			render.flipX = false;
			animator.SetBool("Run", true);
		}
		else
		{
			animator.SetBool("Run", false);
		}
	}

	private void OnJump(InputValue value)
	{
		if (isGround)
		{
			Debug.Log("점프");
			if (value.isPressed)
			{
				if (jumpChargingRoutine != null)
				{
					StopCoroutine(jumpChargingRoutine);
				}
				jumpChargingRoutine = StartCoroutine(JumpChargingRoutine());
				Debug.Log("점프 차징");
			}
			else
			{
				if (jumpChargingRoutine != null)
				{
					StopCoroutine(jumpChargingRoutine);
					jumpChargingRoutine = null;
				}
				Jump();
				Debug.Log("일반 점프");
			}
		}
	}

	private void OnAttack(InputValue value)
	{
		if (isAttack)
		{
			isAttack = false;
			Debug.Log("공격");
			StartCoroutine(AttackRoutine());
			if (lookUp)
			{
				animator.SetBool("AttackUp", true);
				Attack[2].gameObject.SetActive(true);
			}
			else if (lookDown && !isGround)
			{
				animator.SetBool("AttackDown", true);
				Attack[3].gameObject.SetActive(true);
			}
			else if(lastMoveDir.x < 0)
			{
				animator.SetBool("Attack", true);
				Attack[0].gameObject.SetActive(true);
			}
			else
			{
				animator.SetBool("Attack", true);
				Attack[1].gameObject.SetActive(true);
			}
		}
	}

	private void OnLookUp(InputValue value)
	{
		lookUp = value.isPressed;
	}

	private void OnLookDown(InputValue value)
	{
		lookDown = value.isPressed;
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
		while (true)
		{
			currentJumpPower += jumpChargingRate * Time.deltaTime;
			if (currentJumpPower >= jumpChargingMax)
			{
				currentJumpPower = jumpChargingMax;
				Jump();
				jumpChargingRoutine = null;
				yield break;
			}
			yield return null;
		}
	}

	IEnumerator AttackRoutine()
	{
		yield return new WaitForSeconds(attackSpeed);
		animator.SetBool("AttackDown", false);
		animator.SetBool("AttackUp", false);
		animator.SetBool("Attack", false);
		isAttack = true;
		for(int i = 0; i < 4; i++)
		{
			Attack[i].gameObject.SetActive(false);
		}
	}
}