using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // DOTweenの名前空間を追加

public class TitleManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public RectTransform titleLogo;
    public Button startBtn;
    public RectTransform startBtnRect;

    public AudioSource bgmSource;      // BGM用AudioSource
    public AudioSource seSource;       // 効果音用AudioSource
    public AudioClip bgmClip;          // BGMクリップ
    public AudioClip clickSeClip;      // クリック効果音クリップ
    public AudioClip hoverSeClip;      // ホバー効果音クリップ

    void Start() {
        // titleLogoアニメーション
        Vector2 startPos = titleLogo.anchoredPosition;
        titleLogo.anchoredPosition = new Vector2(startPos.x,360);
        titleLogo.DOAnchorPosY(startPos.y,1.0f).SetEase(Ease.OutBounce);

        // startBtnイベント登録
        startBtn.onClick.AddListener(OnStartBtnClick);

        // BGM再生
        if (bgmSource != null && bgmClip != null) {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quit Game."); // エディタで動作確認用
        }
    }

    void OnStartBtnClick() {
        // クリックアニメーション
        startBtnRect.DOPunchScale(Vector3.one * 0.2f,0.2f,10,1).OnComplete(() => {
            SceneManager.LoadScene("GameScene");
        });
        // クリック効果音
        if (seSource != null && clickSeClip != null) {
            seSource.PlayOneShot(clickSeClip);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        startBtnRect.DOScale(1.1f,0.2f).SetEase(Ease.OutBack);
        // ホバー効果音
        if (seSource != null && hoverSeClip != null) {
            seSource.PlayOneShot(hoverSeClip);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        startBtnRect.DOScale(1.0f,0.2f).SetEase(Ease.OutBack);
    }
}
