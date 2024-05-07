using UnityEngine;
using TMPro;

public class GameSharedUI : MonoBehaviour
{
	#region Singleton class: GameSharedUI

	//Singleton 인스턴스를 저장할 정적 변수 선언
	public static GameSharedUI Instance;

	//Awake 함수에서 Singleton 패턴구현
	void Awake ()
	{
		//Instance가 null이면 현재 인스턴스를 할당
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	//화면에 동전 수를 나타내는 텍스트 배열
	[SerializeField] TMP_Text[] coinsUIText;
	[SerializeField] TMP_Text[] LevelUIText;
	public int level;

	//게임 시작시 호출되는 함수
	void Start ()
	{
		//화면에 동전 수를 업데이트
		UpdateCoinsUIText ();
		
		
	}

	//동전 텍스트를 업데이트 하는 함수
	public void UpdateCoinsUIText ()
	{
		//동전 텍스트 배열을 반복하여 업데이트
		for (int i = 0; i < coinsUIText.Length; i++) {
			SetCoinsText (coinsUIText [i], GameDataManager.GetCoins ());
		}

		if (level != 0)
		{
			//동전 텍스트 배열을 반복하여 업데이트
			for (int i = 0; i < LevelUIText.Length; i++)
			{
				SetLevelText(LevelUIText[i], level);
			}
		}
    }

   
    void SetLevelText(TMP_Text textMesh, int value)
    {


		textMesh.text = value.ToString();
       
    }

    //텍스트에 동전 수를 설정하는 함수
    void SetCoinsText (TMP_Text textMesh, int value)
	{
		
		//동전 수가 일정수준 이상인 경우 특정 형식으로 표시
		if (value >= 1000)
			textMesh.text = string.Format ("{0}K.{1}", (value / 1000), GetFirstDigitFromNumber (value % 1000));
		else //동전 수가 일정 수준 이하인 경우 그대로 표시
			textMesh.text = value.ToString ();
	}

	//숫자에서 첫 번째 자리 숫자를 가져오는 함수
	int GetFirstDigitFromNumber (int num)
	{
		//숫자를 문자열로 변환한 후 첫번째 문자를 정수로 변환하여 반환
		return int.Parse (num.ToString () [0].ToString ());
	}
}
