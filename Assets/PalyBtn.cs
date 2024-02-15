using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalyBtn : MonoBehaviour
{
    private Button button;
    private Animator animator;

    private Color normalColors; // 원래의 색상을 저장하기 위한 변수

    private void Start()
    {
        // 버튼과 애니메이터 컴포넌트를 가져옴
        button = GetComponent<Button>();
        animator = GetComponent < Animator>();

        // 현재 버튼의 색상을 저장
        normalColors = button.GetComponent<Image>().color;
    }

    public void ResetButtonState()
    {
        // 버튼 상태를 리셋
        button.interactable = true;

        // 애니메이션을 정지하고 초기 상태로 설정
        animator.SetBool("Highlighted", false);

        // 버튼의 색상을 원래의 색상으로 설정
        button.GetComponent<Image>().color = Color.red;
    }
}
