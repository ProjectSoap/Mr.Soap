using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterWordsUI 
	:
	MonoBehaviour
{
	[SerializeField]
	GameObject m_wordsSprite;
	[SerializeField,Header("セリフの最大表示数")]
	uint m_displayMax = 2;
	GameObject[] m_gameObjectArray;

	Queue<CharacterWords> m_activeList = new Queue<CharacterWords>(); // 表示中のセリフクラス
	CharacterWords[] m_activeArray; // 表示中のセリフクラス配列
	Queue<CharacterWords> m_unactiveList = new Queue<CharacterWords>();    // 使っていないセリフクラス
	Queue<ESayTexName> m_waitWordsList = new Queue<ESayTexName>(); // 表示限界で表示待ちのセリフ

	Image m_characterIconUIImage;	// キャラアイコンスプライト
	//せっけんのセリフの種類
	public enum ESayTexName
	{
		SEKKEN_POP, //せっけんがでてきた
		RECOVERY,   //回復した
		RAIN,       //あめだ
		WIND,       //かぜがふいてきた
		FOG,        //まえがみえない
		CRASH,      //いてて
		BARRICADE,   //これいじょうすすめないよ
		WASH_REALITY,	// レアやぞ
		WASH_CHAIN,		// ウォッシュチェイン
		DEAD			// ゲーム終了時
	};
	//選択したセッケンキャラ
	public enum ESekkenNo
	{
		SekkenKun,
		SekkenChan,
		SekkenHero
	};

	[SerializeField, Tooltip("セリフを表示する時間")]
	private float drawTime = 3.0f;
	[SerializeField, Tooltip("セリフが最大サイズに到達するまでの時間")]
	private float sizeMaxTime = 0.5f;
	
	[SerializeField, Tooltip("せっけんくんのセリフテクスチャのリスト。ESayTexNameの順番にセットしてほしい")]
	private List<Sprite> sekkenKunSpriteList;
	[SerializeField, Tooltip("せっけんちゃんのセリフテクスチャのリスト。ESayTexNameの順番にセットしてほしい")]
	private List<Sprite> sekkenChanSpriteList;
	[SerializeField, Tooltip("せっけんヒーローのセリフテクスチャのリスト。ESayTexNameの順番にセットしてほしい")]
	private List<Sprite> sekkenHeroSpriteList;

	//表示時間保存
	private float drawTimeCount;
	//選択したセッケンキャラクタ番号
	private ESekkenNo sekkenNo = ESekkenNo.SekkenKun;

	// Use this for initialization
	void Start () {
		drawTimeCount = 999.0f;
		m_characterIconUIImage = GameObject.Find("SizeIcon").GetComponent<Image>();
		m_gameObjectArray = new GameObject[m_displayMax];
		for (int i = 0; i < m_displayMax; i++)
		{
			m_gameObjectArray[i] = GameObject.Instantiate(m_wordsSprite,transform.position,transform.rotation) as GameObject;
			m_unactiveList.Enqueue(m_gameObjectArray[i].GetComponent<CharacterWords>());
			m_gameObjectArray[i].transform.parent = transform;
			
		}
		m_activeArray = m_activeList.ToArray();
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool isUpdateArray = false;
		for (int i = 0; i < m_activeArray.Length; i++)
		{
			if (m_activeArray[i].WordsState == CharacterWords.EWordsState.VANISH)
			{

				CharacterWords words = m_activeList.Dequeue();
				m_unactiveList.Enqueue(words);
				isUpdateArray = true;
			}
		}
		if (isUpdateArray)
		{
			m_activeArray = m_activeList.ToArray();
		}
		OptimizePosition();
	}

	//せっけんくんのセリフを表示するにはここを一度呼び出して
	public void DrawSayTexture(ESayTexName eSayTexNo){
		//表示カウント初期化
		drawTimeCount = 0.0f;
		if (m_unactiveList.Count > 0)
		{
			for (int i = 0; i < m_activeArray.Length; i++)
			{
				if (m_activeArray[i].Words == eSayTexNo)
				{
					// 既に表示済みなら実行中止
					return;
				}
			}

			CharacterWords words = m_unactiveList.Dequeue();
			if (words)
			{
				//表示テクスチャの切り替え
				switch (sekkenNo)
				{
					case ESekkenNo.SekkenKun:
						words.SetWordsTexture(sekkenKunSpriteList[(int)eSayTexNo]);
						break;
					case ESekkenNo.SekkenChan:
						words.SetWordsTexture(sekkenChanSpriteList[(int)eSayTexNo]);
						break;
					case ESekkenNo.SekkenHero:
						words.SetWordsTexture(sekkenHeroSpriteList[(int)eSayTexNo]);
						break;
				}
				words.Words = (eSayTexNo);
				m_activeList.Enqueue(words);
				m_activeArray = m_activeList.ToArray();
				words.gameObject.transform.parent = null;
				words.gameObject.transform.parent = transform;

			}
		}

	}

	//セリフをせっけん毎に切り替える処理
	public void ChangeSekken(ESekkenNo _no)
	{
		sekkenNo = _no;
	}

	// activeになってるセリフの位置最適化
	void OptimizePosition()
	{
		Vector3 CenterPosition = transform.position;
		CenterPosition.y = m_characterIconUIImage.rectTransform.position.y;
		//CenterPosition.y -= m_characterIconUIImage.rectTransform.rect.height * m_characterIconUIImage.rectTransform.localScale.y ;	// 下端を求める
		for (int i = 0; i < m_activeArray.Length; i++)
		{
			CenterPosition.y -= m_activeArray[i].ImageTransform.rect.height * m_activeArray[i].ImageTransform.localScale.y;
			m_activeArray[i].ImageTransform.position = CenterPosition;
		}
	}
}

