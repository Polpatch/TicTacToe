using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

class ModalInfoAnimator
{
    private RectTransform modalTransform;
    private GameObject modalParent;

    private Vector2 originalSizeModal;

    private Sequence seq;
    public ModalInfoAnimator(GameObject gameObject)
    {
        DOTween.Init();
        this.modalTransform = gameObject.transform.GetComponent<RectTransform>();
        this.modalParent = gameObject.transform.parent.gameObject;
        this.originalSizeModal = modalTransform.sizeDelta + Vector2.zero;

        this.seq = CreateSequence();
        this.seq.Pause();

        this.idlePosition();
    }

    private void idlePosition()
    {
        this.modalParent.SetActive(false);
        this.modalTransform.sizeDelta = new Vector2(originalSizeModal.x * 0.08f, 0);
    }

    private Sequence CreateSequence()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetAutoKill(false)
                // .Append(this.modalTransform.DOSizeDelta(new Vector2(originalSizeModal.x * 0.08f, 0), 0.2f).From())
                .Append(this.modalTransform.DOSizeDelta(new Vector2(originalSizeModal.x * 0.08f, originalSizeModal.y), 0.2f))
                .Append(this.modalTransform.DOSizeDelta(new Vector2(originalSizeModal.x, originalSizeModal.y), 0.8f))
                .OnComplete(() => Debug.Log("aperto"))
                .OnRewind(() =>
                {
                    this.modalParent.SetActive(false);
                    sequence.Flip();
                });
        return sequence;
    }

    public void OpenModalAnimation()
    {
        this.modalParent.SetActive(true);
        // Sequence modalSequence = DOTween.Sequence();

        // modalSequence.Append(this.modalTransform.DOSizeDelta(new Vector2(originalSizeModal.x * 0.08f, originalSizeModal.y), 0.2f));
        // modalSequence.Append(this.modalTransform.DOSizeDelta(new Vector2(originalSizeModal.x, originalSizeModal.y), 0.8f));
        // this.seq.OnComplete(() => Debug.Log("aperto"));
        this.seq.Play();

    }

    public void CloseModalAnimation()
    {
        Debug.Log("Closing Modal");
        // Sequence s = CreateSequence();
        // s.Complete();
        // this.seq.OnRewind(()=> this.modalParent.SetActive(false));
        // s.PlayBackwards();
        this.seq.SmoothRewind();

    }

}