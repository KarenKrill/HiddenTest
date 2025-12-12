using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KarenKrill.UniCore.UI.Views.Abstractions;
using KarenKrill.UniCore.UI.Views;

namespace HiddenTest.UI.Views
{
    using Abstractions;

    public class TaskView : ViewBehaviour, ITaskView, IView
    {
        public string Name { set => _nameText.text = value; }
        public Sprite Icon { set => _iconImage.sprite = value; }
        public TaskViewType Type
        {
            set
            {
                switch (value)
                {
                    case TaskViewType.NameOnly:
                        _nameText.gameObject.SetActive(true);
                        _iconImage.gameObject.SetActive(false);
                        break;
                    case TaskViewType.IconOnly:
                        _nameText.gameObject.SetActive(false);
                        _iconImage.gameObject.SetActive(true);
                        break;
                    case TaskViewType.NameAndIcon:
                    default:
                        _nameText.gameObject.SetActive(true);
                        _iconImage.gameObject.SetActive(true);
                        break;
                }
            }
        }

        [SerializeField]
        private TextMeshProUGUI _nameText;
        [SerializeField]
        private Image _iconImage;
    }
}
