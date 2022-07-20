using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SFXComposer)),
 RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    public UnityEvent OnClick;

    public bool Interactable {
        get
        {
            return _button.interactable;
        }
        set
        {
            _button.interactable = value;
        }
    }

    private Button _button;
    private SFXComposer _sfxComposer;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickHandle);

        _sfxComposer = GetComponent<SFXComposer>();
    }

    private void OnClickHandle()
    {
        _sfxComposer.Play(SFXComposer.ClipType.Shot);
        OnClick?.Invoke();
    }

}
