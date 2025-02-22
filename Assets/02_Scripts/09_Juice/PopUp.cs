using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
   [SerializeField] private TMP_Text _titleText;
   [SerializeField] private TMP_Text _mainText;
   [SerializeField] private AudioClip _notificationSound;
   
   public static PopUp Instance;
   private bool _showing;
   
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }

   public void Setup(string titleText, string mainText)
   {
      _titleText.text = titleText;
      _mainText.text = mainText;
   }

   public void Show()
   {
      if (_showing)
      {
         Invoke(nameof(Show), 3.5f);
         return;
      }
      
      AudioSource.PlayClipAtPoint(_notificationSound, Vector3.zero);
      
      _showing = true;
      transform.DOScale(Vector3.one * 1.25f, 0.1f).OnComplete(() =>
      {
         transform.DOScale(Vector3.one, 0.2f).OnComplete(() =>
         {
            Invoke(nameof(Hide), 2.5f);
         });
      });
   }

   public void Hide()
   {
      transform.DOScale(Vector3.one * 1.25f, 0.1f).OnComplete(() =>
      {
         transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
         {
            _showing = false;
         });
      });
   }
}
