using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // DOTweenの名前空間を追加

public class TitleManager : MonoBehaviour {
    public RectTransform titleLogo;
    public Button startBtn; // Button型でアサイン
    public RectTransform startBtnRect; // RectTransform型でアサイン

    void Start() {
        // titleLogoアニメーション
        Vector2 startPos = titleLogo.anchoredPosition;
        titleLogo.anchoredPosition = new Vector2(startPos.x,360);
        titleLogo.DOAnchorPosY(startPos.y,1.0f).SetEase(Ease.OutBounce);

        // startBtnイベント登録
        startBtn.onClick.AddListener(OnStartBtnClick);

        // ホバーイベント登録
        EventTrigger trigger = startBtn.gameObject.AddComponent<EventTrigger>();
        var entryEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        entryEnter.callback.AddListener((_) => OnStartBtnHover(true));
        trigger.triggers.Add(entryEnter);

        var entryExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        entryExit.callback.AddListener((_) => OnStartBtnHover(false));
        trigger.triggers.Add(entryExit);
    }

    void OnStartBtnClick() {
        // クリックアニメーション
        startBtnRect.DOPunchScale(Vector3.one * 0.2f,0.2f,10,1).OnComplete(() => {
            SceneManager.LoadScene("GameScene");
        });
    }

    void OnStartBtnHover(bool isEnter) {
        // ホバーアニメーション
        if (isEnter) {
            startBtnRect.DOScale(1.1f,0.2f).SetEase(Ease.OutBack);
        } else {
            startBtnRect.DOScale(1.0f,0.2f).SetEase(Ease.OutBack);
        }
    }
}
