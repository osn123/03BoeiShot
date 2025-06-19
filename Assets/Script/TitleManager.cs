using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // DOTween�̖��O��Ԃ�ǉ�

public class TitleManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public RectTransform titleLogo;
    public Button startBtn;
    public RectTransform startBtnRect;

    public AudioSource bgmSource;      // BGM�pAudioSource
    public AudioSource seSource;       // ���ʉ��pAudioSource
    public AudioClip bgmClip;          // BGM�N���b�v
    public AudioClip clickSeClip;      // �N���b�N���ʉ��N���b�v
    public AudioClip hoverSeClip;      // �z�o�[���ʉ��N���b�v

    void Start() {
        // titleLogo�A�j���[�V����
        Vector2 startPos = titleLogo.anchoredPosition;
        titleLogo.anchoredPosition = new Vector2(startPos.x,360);
        titleLogo.DOAnchorPosY(startPos.y,1.0f).SetEase(Ease.OutBounce);

        // startBtn�C�x���g�o�^
        startBtn.onClick.AddListener(OnStartBtnClick);

        // BGM�Đ�
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
            Debug.Log("Quit Game."); // �G�f�B�^�œ���m�F�p
        }
    }

    void OnStartBtnClick() {
        // �N���b�N�A�j���[�V����
        startBtnRect.DOPunchScale(Vector3.one * 0.2f,0.2f,10,1).OnComplete(() => {
            SceneManager.LoadScene("GameScene");
        });
        // �N���b�N���ʉ�
        if (seSource != null && clickSeClip != null) {
            seSource.PlayOneShot(clickSeClip);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        startBtnRect.DOScale(1.1f,0.2f).SetEase(Ease.OutBack);
        // �z�o�[���ʉ�
        if (seSource != null && hoverSeClip != null) {
            seSource.PlayOneShot(hoverSeClip);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        startBtnRect.DOScale(1.0f,0.2f).SetEase(Ease.OutBack);
    }
}
