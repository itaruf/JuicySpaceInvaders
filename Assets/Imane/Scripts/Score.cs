using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public AnimationCurve scaleCurve;
    public Vector3 targetB;
    public float scaleLerpTime = 2f;
    private bool _isPlaying = false;


    public TextMeshProUGUI textPro;


    public static Score instance;
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        textPro = GetComponent<TextMeshProUGUI>();

        textPro.text = GameStateManager.Instance.currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetScore(int score)
    {
        if (!_isPlaying)
            StartCoroutine(PlayAnimation());

        GameStateManager.Instance.currentScore += score;
        textPro.text = GameStateManager.Instance.currentScore.ToString();
    }

    IEnumerator PlayAnimation()
    {
        _isPlaying = true;
        float i = 0;
        float rate = 1 / scaleLerpTime;
        while (i < scaleLerpTime)
        {
            i += rate * Time.deltaTime;
            transform.localScale = new Vector3(scaleCurve.Evaluate(i), scaleCurve.Evaluate(i), scaleCurve.Evaluate(i));
            yield return 0;
        }
        _isPlaying = false;
    }
}
