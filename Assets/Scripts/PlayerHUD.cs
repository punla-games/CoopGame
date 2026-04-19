using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD:SingletonBehaviour<PlayerHUD>
{
    public Player _player;

    public TMP_Text _moneyLabel;

    public TMP_Text _interactLabel;
    public Image _interactProgressImage;

    public TMP_Text _itemLabel;

    public float tipEarnedTimer = 0f;
    public float tipEarnedDuration = 2f;
    public AnimationCurve tipEarnedCurve;
    public Transform _tipEarnedContainer;

    public void Update()
    {
        // money.
        _moneyLabel.text=$"money: ${GameManager.Get.money/100f:0.00}";

        // hovered interactable.
        _interactLabel.text="";

        if(_player.hovered!=null)
        {
            _interactLabel.text=_player.hovered.GetInteractText(_player);
        }

        // active interactable.
        _interactProgressImage.enabled=false;

        if(_player.active!=null)
        {
            if(_player.active.GetInteractDuration(_player)>0f)
            {
                _interactProgressImage.enabled=true;
                float progress = Mathf.Clamp01(_player.interactTime/_player.active.GetInteractDuration(_player));
                _interactProgressImage.fillAmount=progress;
            }
        }

        // item.
        _itemLabel.text="";
        if(_player.HeldItem!=null)
        {
            _itemLabel.text=_player.HeldItem?.Title;
        }

        // tip earned display.
        tipEarnedTimer=Mathf.Max(tipEarnedTimer-Time.deltaTime/tipEarnedDuration,0f);
        float t = Mathf.Clamp01(1f-tipEarnedTimer);
        float scale = tipEarnedCurve.Evaluate(t);
        _tipEarnedContainer.localScale=Vector3.one*scale;
    }

    public void ShowTipEarned()
    {
        tipEarnedTimer=1f;
    }
}
