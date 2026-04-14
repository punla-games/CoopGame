using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD:MonoBehaviour
{
    public Player _player;

    public TMP_Text _moneyLabel;

    public TMP_Text _interactLabel;
    public Image _interactProgressImage;

    public TMP_Text _itemLabel;

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
    }
}
