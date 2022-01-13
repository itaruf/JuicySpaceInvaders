using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    //public AnimationCurve scaleCurve;
    public AnimationCurve scaleCurve;
    private float scaleLerpTime;
    private bool _isPlaying = false;
    public List<Vector2> keyFrames;

    public TextMeshProUGUI textPro;


    public static Score instance;
    private void Awake()
    {
        instance = this;

        foreach(Vector2 keyframe in keyFrames)
        {
            scaleCurve.AddKey(keyframe.x, keyframe.y);
        }
        if (keyFrames.Count > 0)
            scaleLerpTime = keyFrames[keyFrames.Count - 1].x;
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
        if (!_isPlaying && scaleCurve.length != 0)
            StartCoroutine(PlayAnimation());

        GameStateManager.Instance.currentScore += score;
        textPro.text = GameStateManager.Instance.currentScore.ToString();
    }

    IEnumerator PlayAnimation()
    {
        _isPlaying = true;
        textPro.color = Color.Lerp(Color.white, Color.red, 1f);

        float i = 0;
        float rate = 1 / scaleLerpTime;
        while (i < scaleLerpTime)
        {
            i += rate * Time.deltaTime;
            transform.localScale = new Vector3(scaleCurve.Evaluate(i), scaleCurve.Evaluate(i), scaleCurve.Evaluate(i));
            yield return 0;
        }
        textPro.color = Color.Lerp(Color.red, Color.white, 1f);
        _isPlaying = false;
    }
}
