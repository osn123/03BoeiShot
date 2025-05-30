using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // DOTween�̖��O��Ԃ�ǉ�

public class TitleManager : MonoBehaviour {
    public RectTransform titleLogo;
    public Button startBtn; // Button�^�ŃA�T�C��
    public RectTransform startBtnRect; // RectTransform�^�ŃA�T�C��

    void Start() {
        // titleLogo�A�j���[�V����
        Vector2 startPos = titleLogo.anchoredPosition;
        titleLogo.anchoredPosition = new Vector2(startPos.x,360);
        titleLogo.DOAnchorPosY(startPos.y,1.0f).SetEase(Ease.OutBounce);

        // startBtn�C�x���g�o�^
        startBtn.onClick.AddListener(OnStartBtnClick);

        // �z�o�[�C�x���g�o�^
        EventTrigger trigger = startBtn.gameObject.AddComponent<EventTrigger>();
        var entryEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        entryEnter.callback.AddListener((_) => OnStartBtnHover(true));
        trigger.triggers.Add(entryEnter);

        var entryExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        entryExit.callback.AddListener((_) => OnStartBtnHover(false));
        trigger.triggers.Add(entryExit);
    }

    void OnStartBtnClick() {
        // �N���b�N�A�j���[�V����
        startBtnRect.DOPunchScale(Vector3.one * 0.2f,0.2f,10,1).OnComplete(() => {
            SceneManager.LoadScene("GameScene");
        });
    }

    void OnStartBtnHover(bool isEnter) {
        // �z�o�[�A�j���[�V����
        if (isEnter) {
            startBtnRect.DOScale(1.1f,0.2f).SetEase(Ease.OutBack);
        } else {
            startBtnRect.DOScale(1.0f,0.2f).SetEase(Ease.OutBack);
        }
    }
}
