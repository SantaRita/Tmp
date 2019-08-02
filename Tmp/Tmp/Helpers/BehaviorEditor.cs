using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tmp.Helpers
{
    public class BehaviorEditor : Behavior<Editor>
    {

        Label lblRequiredMessage = new Label();

        public BehaviorEditor(Label parlblRequiredMessage) {

            lblRequiredMessage = parlblRequiredMessage;
            lblRequiredMessage.IsVisible = false;
        }

        protected override void OnAttachedTo(Editor Editor)
        {
            Editor.TextChanged += OnEditorTextChanged;
            base.OnAttachedTo(Editor);
        }

        protected override void OnDetachingFrom(Editor Editor)
        {
            Editor.TextChanged -= OnEditorTextChanged;
            base.OnDetachingFrom(Editor);
        }

        void OnEditorTextChanged(object sender, TextChangedEventArgs args)
        {

            bool isValid = !string.IsNullOrWhiteSpace(args.NewTextValue);

            lblRequiredMessage.IsVisible = isValid ? false : true;
            lblRequiredMessage.TextColor = isValid ? Color.Default : Color.Red;

            ((Editor)sender).TextColor = isValid ? Color.Default : Color.Red;
            ((Editor)sender).BackgroundColor = isValid ? Color.Default : Color.FromHex("#FBC5D0");

        }
    }

}
