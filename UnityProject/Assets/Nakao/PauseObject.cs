﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Rigidbodyの速度を保存しておくクラス
/// </summary>
public class RigidbodyVelocity
{
	public Vector3 velocity;
	public Vector3 angularVeloccity;
	public RigidbodyVelocity(Rigidbody rigidbody)
	{
		velocity = rigidbody.velocity;
		angularVeloccity = rigidbody.angularVelocity;
	}
}

public class PauseObject : MonoBehaviour
{

	/// <summary>
	/// 現在Pause中か？
	/// </summary>
	public bool pausing;

	/// <summary>
	/// 無視するGameObject
	/// </summary>
	public GameObject[] ignoreGameObjects;

	/// <summary>
	/// ポーズ状態が変更された瞬間を調べるため、前回のポーズ状況を記録しておく
	/// </summary>
	bool prevPausing;

	/// <summary>
	/// Rigidbodyのポーズ前の速度の配列
	/// </summary>
	RigidbodyVelocity[] rigidbodyVelocities;

	/// <summary>
	/// ポーズ中のRigidbodyの配列
	/// </summary>
	Rigidbody[] pausingRigidbodies;

	/// <summary>
	/// ポーズ中のMonoBehaviourの配列
	/// </summary>
	MonoBehaviour[] pausingMonoBehaviours;

	/// <summary>
	/// ポーズ中のMonoBehaviourの配列
	/// </summary>
	ParticleSystem[] pausingParticleSystem;

	Animator[] pausingAnimation;

	void Start()
	{
		// スタート時にポーズ処理をする
		prevPausing = pausing;
		if (pausing) Pause();

	}

	/// <summary>
	/// 更新処理
	/// </summary>
	void Update()
	{
		// ポーズ状態が変更されていたら、Pause/Resumeを呼び出す。
		if (prevPausing != pausing)
		{
			if (pausing) Pause();
			else Resume();
		}
		prevPausing = pausing;	// 毎フレーム 前の状態を更新
	}

	public void PushPose()
	{
		pausing = !pausing;
	}

	/// <summary>
	/// 中断
	/// </summary>
	void Pause()
	{
		// Rigidbodyの停止
		// 子要素から、スリープ中でなく、IgnoreGameObjectsに含まれていないRigidbodyを抽出
		Predicate<Rigidbody> rigidbodyPredicate =
			obj => !obj.IsSleeping() &&
				   Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
		pausingRigidbodies = Array.FindAll(transform.GetComponentsInChildren<Rigidbody>(), rigidbodyPredicate);
		rigidbodyVelocities = new RigidbodyVelocity[pausingRigidbodies.Length];
		for (int i = 0; i < pausingRigidbodies.Length; i++)
		{
			// 速度、角速度も保存しておく
			rigidbodyVelocities[i] = new RigidbodyVelocity(pausingRigidbodies[i]);
			pausingRigidbodies[i].Sleep();
		}

		// MonoBehaviourの停止
		// 子要素から、有効かつこのインスタンスでないもの、IgnoreGameObjectsに含まれていないMonoBehaviourを抽出
		Predicate<MonoBehaviour> monoBehaviourPredicate =
			obj => obj.enabled &&
				   obj != this &&
				   Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
		pausingMonoBehaviours = Array.FindAll(transform.GetComponentsInChildren<MonoBehaviour>(), monoBehaviourPredicate);
		foreach (var monoBehaviour in pausingMonoBehaviours)
		{
			monoBehaviour.enabled = false;
		}

		Predicate<ParticleSystem> particlePredicate =
		   obj => obj.enableEmission &&
				  obj != this &&
				  Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
		pausingParticleSystem = Array.FindAll(transform.GetComponentsInChildren<ParticleSystem>(), particlePredicate);
		foreach (var particleSystem in pausingParticleSystem)
		{
			particleSystem.Pause();
		}

		Predicate<Animator> animationPredicate =
		   obj => obj.enabled &&
				  obj != this &&
				  Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
		pausingAnimation = Array.FindAll(transform.GetComponentsInChildren<Animator>(), animationPredicate);
		foreach (var animation in pausingAnimation)
		{
			animation.enabled = false;
		}


	}

	/// <summary>
	/// 再開
	/// </summary>
	void Resume()
	{
		// Rigidbodyの再開
		for (int i = 0; i < pausingRigidbodies.Length; i++)
		{
			pausingRigidbodies[i].WakeUp();
			pausingRigidbodies[i].velocity = rigidbodyVelocities[i].velocity;
			pausingRigidbodies[i].angularVelocity = rigidbodyVelocities[i].angularVeloccity;
		}

		// MonoBehaviourの再開
		foreach (var monoBehaviour in pausingMonoBehaviours)
		{
			monoBehaviour.enabled = true;
		}

		foreach (var particleSystem in pausingParticleSystem)
		{
			particleSystem.Play();
		}

		foreach (var animation in pausingAnimation)
		{
			animation.enabled = true;
		}

	}
}
