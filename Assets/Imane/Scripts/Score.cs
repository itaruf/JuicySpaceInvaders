using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public AnimationCurve scaleCurve;
    public Vector3 targetB;
    public float scaleLerpTime = 2f;
    private bool _isPlaying = false;

    public Text text;
    private int _currentScore;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        _currentScore = 0;
        text.text = _currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetScore(int score)
    {
        if (!_isPlaying)
            StartCoroutine(PlayAnimation());

        _currentScore += score;
        text.text = _currentScore.ToString();
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
