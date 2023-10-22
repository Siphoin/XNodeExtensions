using UnityEngine;
using XNode;
using SiphoinUnityHelpers.XNodeExtensions.Attributes;
using SiphoinUnityHelpers.XNodeExtensions.Extensions;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SiphoinUnityHelpers.XNodeExtensions
{
    public abstract class VaritableNode : BaseNode
    {
        [SerializeField, ReadOnly(ReadOnlyMode.OnEditor)] private string _name;

        [Space(25)]

        [SerializeField, ReadOnly(ReadOnlyMode.OnEditor)] private Color32 _color = UnityEngine.Color.white;

        public string Name { get => _name; set => _name = value; }
        public Color32 Color { get => _color; set => _color = value; }



        public abstract object GetStartValue();

        public abstract void ResetValue();

        public abstract object GetCurrentValue();

#if UNITY_EDITOR

        protected virtual void Validate()
        {
            if (!Application.isPlaying)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    Name = $"{GetDefaultName()} Varitable";
                }
            }
        }
        protected virtual void OnValidate()
        {
            ValidateName();
        }

        protected virtual void ValidateName()
        {
            name = Color.ToColorTag($"{Name} ({GetDefaultName()})");
        }

        protected virtual new void OnEnable()
        {
            base.OnEnable();

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;

            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;

                ResetValue();
            }
        }
#endif

    }

    public abstract class VaritableNode<T> : VaritableNode
    {
        private T _startValue;

        [SerializeField, Output(ShowBackingValue.Always), ReadOnly(ReadOnlyMode.OnEditor)] private T _value;
        public override object GetStartValue()
        {
           return _startValue;
        }

        public override object GetValue(NodePort port)
        {
            return _value;
        }


        public void SetValue (T value)
        {
            _value = value;
        }

        public override object GetCurrentValue()
        {
            return _value;
        }

        public override void ResetValue()
        {
            _value = _startValue;
        }

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            Validate();
        }

        protected override void Validate()
        {
            base.Validate();

            if (!Application.isPlaying)
            {
                _startValue = _value;
            }
        }

        
#endif

    }




}
