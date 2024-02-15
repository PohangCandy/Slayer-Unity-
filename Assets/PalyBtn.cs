using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalyBtn : MonoBehaviour
{
    private Button button;
    private Animator animator;

    private Color normalColors; // ������ ������ �����ϱ� ���� ����

    private void Start()
    {
        // ��ư�� �ִϸ����� ������Ʈ�� ������
        button = GetComponent<Button>();
        animator = GetComponent < Animator>();

        // ���� ��ư�� ������ ����
        normalColors = button.GetComponent<Image>().color;
    }

    public void ResetButtonState()
    {
        // ��ư ���¸� ����
        button.interactable = true;

        // �ִϸ��̼��� �����ϰ� �ʱ� ���·� ����
        animator.SetBool("Highlighted", false);

        // ��ư�� ������ ������ �������� ����
        button.GetComponent<Image>().color = Color.red;
    }
}
