using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogInPanel : MonoBehaviour
{
    [Header("SighUpInfo")]
    [SerializeField] TMP_InputField idText;
    [SerializeField] TMP_InputField nameText;
    [SerializeField] TMP_InputField PWText;
    [SerializeField] TMP_InputField confirmText;
    [SerializeField] Button sighUpBtn;
    // sigh up 버튼을 클릭하면
    // 1. 칸이 비어있으면 에러 창 팝업
    // 2. 다 작성 했으면 클릭 시 유저정보 리스트에로 정보 전달
    //   1) 만약 비밀번호와 비번확인이 일치하지 않으면 에러창 팝업
    // 비밀번호 입력 시 ***으로 표기 (Input Field의 Content Type을 Password로 변경)
    // 취소 버튼 클릭 시 창구만 닫기 (완료)
    // 로그인 에서 회원가입 클릭 시 회원가입 창 나오게 하기 (완료)


    //[Header("SighInInfo")]
}
