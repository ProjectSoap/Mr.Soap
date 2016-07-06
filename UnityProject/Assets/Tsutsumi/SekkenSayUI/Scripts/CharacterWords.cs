using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class CharacterWords : MonoBehaviour
{

	public enum EWordsState
	{
		VANISH,      // 完全に消えた
		APPEARANCE, // 拡大しつつ出現
		STAY,       // 表示し続けとく
		DIMINISH   // 縮小しつつ消す
	}
	CharacterWordsUI.ESayTexName m_words;
	public CharacterWordsUI.ESayTexName Words
	{
		get { return m_words; }
		set { m_words = value; }
	}
	[SerializeField, Tooltip("セリフを表示する時間")]
	private float m_stayTime = 3.0f;
	[SerializeField, Tooltip("セリフが最大サイズに到達するまでの時間")]
	private float m_appearanceTime = 0.5f;

	[SerializeField, Tooltip("セリフが最小サイズに到達するまでの時間")]
	private float m_diminishTime = 0.5f;
	[SerializeField, Tooltip("せっけんのセリフオブジェクトをセットしてほしい")]
	private Image img;
	
	public UnityEngine.RectTransform ImageTransform
	{
		get { return img.rectTransform; }
	}
	EWordsState m_wordsState;
	public CharacterWords.EWordsState WordsState
	{
		get { return m_wordsState; }
		set
		{
			m_wordsState = value;
			switch (m_wordsState)
			{
				case EWordsState.VANISH:
					img.enabled = false;
					break;
				case EWordsState.APPEARANCE:
					img.enabled = true;
					break;
				case EWordsState.STAY:
					img.enabled = true;
					break;
				case EWordsState.DIMINISH:
					img.enabled = true;
					break;
				default:
					break;

			}
		}
	}
	//表示時間保存
	private float drawTimeCount;

	// Use this for initialization
	void Start()
	{
		drawTimeCount = 999.0f;
		img = GetComponent<Image>();
		img.enabled = false;
		WordsState = CharacterWords.EWordsState.VANISH;
	}

	// Update is called once per frame
	void Update()
	{
		switch (m_wordsState)
		{
			case EWordsState.VANISH:
				//指定時間が経過していれば表示を止める
				if (drawTimeCount > m_stayTime)
				{
					return;
				}
				break;
			case EWordsState.APPEARANCE:

				//タイマを進める
				drawTimeCount += Time.deltaTime;
				//サイズ計算
				if (drawTimeCount < m_appearanceTime)
				{
					float percent;
					Vector3 scaleTemp;
					percent = drawTimeCount / m_appearanceTime;
					scaleTemp.x = 1.0f;
					scaleTemp.y = percent * 0.9f + 0.1f;
					scaleTemp.z = 1.0f;
					transform.localScale = scaleTemp;
				}
				else
				{
					Vector3 scaleTemp;
					scaleTemp.x = scaleTemp.y = scaleTemp.z = 1.0f;
					transform.localScale = scaleTemp;
					WordsState = EWordsState.STAY;
					drawTimeCount = 0;
				}
				break;
			case EWordsState.STAY:
				//タイマを進める
				drawTimeCount += Time.deltaTime;
				if (drawTimeCount > m_stayTime)
				{
					WordsState = EWordsState.DIMINISH;
					drawTimeCount = 0;
				}
					break;
			case EWordsState.DIMINISH:
				//タイマを進める
				drawTimeCount += Time.deltaTime;
				//サイズ計算
				if (drawTimeCount < m_appearanceTime)
				{
					float percent;
					Vector3 scaleTemp;
					percent = drawTimeCount / m_appearanceTime;
					scaleTemp.x = 1.0f;
					scaleTemp.y = 1.0f - percent;
					scaleTemp.z = 1.0f;
					transform.localScale = scaleTemp;
				}
				else
				{
					Vector3 scaleTemp;
					scaleTemp.x = scaleTemp.y = scaleTemp.z = 0.0f;
					transform.localScale = scaleTemp;
					WordsState = EWordsState.VANISH;
					drawTimeCount = 0;
				}
				break;
			default:
				break;
		}


		
	}




	//せっけんくんのセリフを表示するにはここを一度呼び出して
	public void SetWordsTexture(Sprite eSayTexNo)
	{
		//表示カウント初期化
		drawTimeCount = 0.0f;

		img.sprite = eSayTexNo;
		WordsState = EWordsState.APPEARANCE;

	}
}