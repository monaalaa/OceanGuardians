using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEffects : MonoBehaviour
{
    [SerializeField]
    private Animator jumpButtonAnim;

    [Header("Scriptable Objects")]
    [SerializeField]
    private GameEvent AfterStars;

    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI coinsText;

    [SerializeField]
    private TextMeshProUGUI LitterText;

    [Header("Font Variables")]
    [SerializeField]
    private float textGrowSize;

    [SerializeField]
    private float textNormalSize, textResizeTime;

    [Header("Stars")]
    [SerializeField]
    private RectTransform[] stars;

    [SerializeField]
    private float starResizeTime;

    private int currentStarIndex = 0;

    public void PlayJumpButtonAnim()
    {
        if (PlayerController.Instance.LocalPlayerInstance.playerCharacter != CharacterStatus.Jump)
        {
            jumpButtonAnim.Play("JumpButtonAnim");
            PlayerController.Instance.LocalPlayerInstance.PreformAction("Jump");
        }
    }

    public void CoinCollected(int value)
    {
        coinsText.text = value.ToString();
        coinsText.fontSize = textGrowSize;
        StartCoroutine(ResizeFont(0));
    }

    public void LitterCollected(int value)
    {
        LitterText.text = value.ToString();
        LitterText.fontSize = textGrowSize;
        StartCoroutine(ResizeFont(1));
    }

    private IEnumerator ResizeFont(int val)
    {
        TextMeshProUGUI myText = (val == 0) ? coinsText : LitterText;

        float size = textGrowSize - textNormalSize;

        while (coinsText.fontSize != textNormalSize || LitterText.fontSize != textNormalSize)
        {
            yield return new WaitForFixedUpdate();
            myText.fontSize = Mathf.MoveTowards(myText.fontSize, textNormalSize, (size / textResizeTime) * Time.deltaTime);
        }
    }

    public void ShowStars(int value)
    {
        StartCoroutine(GrowStar(value));
    }

    private IEnumerator GrowStar(int value)
    {
        float rot = 0;

        if (value > 0 && stars.Length > currentStarIndex)
        {
            while (stars[currentStarIndex].localScale.x != 1)
            {
                yield return new WaitForFixedUpdate();

                stars[currentStarIndex].localScale = Vector3.MoveTowards(stars[currentStarIndex].localScale, Vector3.one, (1 / starResizeTime) * Time.deltaTime);

                rot = Mathf.MoveTowards(rot, 360, (360 / starResizeTime) * Time.deltaTime);
                stars[currentStarIndex].rotation = Quaternion.Euler(new Vector3(0, 0, rot));
            }

            currentStarIndex++;
            StartCoroutine(GrowStar(value - 1));
        }
        else
        {
            AfterStars.Raise();
        }
    }
}