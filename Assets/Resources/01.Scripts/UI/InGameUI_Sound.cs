using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI_Sound : MonoBehaviour
{
    [SerializeField]
    private Slider bgSlider;
    [SerializeField]
    private Slider sfxSlider;

    void Start()
    {
        bgSlider.onValueChanged.AddListener(SoundManager.Instance.BGSoundVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SFXSoundVolume);
    }

    void OnEnable()
    {
        // 현재 믹서 볼륨(dB)을 읽어 슬라이더 초기값으로 설정 (10^(dB/20) 역변환)
        if (SoundManager.Instance.Mixer.GetFloat("BG", out float bgDb))
            bgSlider.SetValueWithoutNotify(Mathf.Pow(10f, bgDb / 20f));

        if (SoundManager.Instance.Mixer.GetFloat("SFX", out float sfxDb))
            sfxSlider.SetValueWithoutNotify(Mathf.Pow(10f, sfxDb / 20f));
    }
}
