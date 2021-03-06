using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField, Tooltip("パネル")]
    Text _pauseTextPanel = null;

    [SerializeField, Tooltip("HPのスライダー")]
    Slider _hpSlider = null;

    [SerializeField, Tooltip("経験値スライダー")]
    Slider _expSlider = null;

    [SerializeField, Tooltip("タイマー")]
    Text _timerText = null;

    [SerializeField, Tooltip("レベルを表示するテキスト")]
    Text _levelText = null;

    private void Awake()
    {
        GameManager.Instance.SetUIManager(this);
    }

    private void Start()
    {
        _hpSlider.maxValue = GameManager.Instance.HP;
        SetExpMaxValue(GameData.ExpTable[0]);
    }

    public void SetActiveText(bool isActive, string text)
    {
        _pauseTextPanel.text = text;
        _pauseTextPanel.gameObject.SetActive(isActive);
    }

    public void SetExpMaxValue(int value)
    {
        _expSlider.maxValue = (float)value;
    }

    public void UpdateHPSlider(int value)
    {
        _hpSlider.value = value;
    }

    public void UpdateExpSlider(int value)
    {
        _expSlider.value = value;
    }
    public void UpdateTimerText(int time)
    {
        int min = time / 60;
        int sec = time - 60 * min;
        _timerText.text = $"{min.ToString("D2")}:{sec.ToString("D2")}";
    }

    public void UpdateLevelUpText(int value)
    {
        _levelText.text = value.ToString("D2");
    }
}
