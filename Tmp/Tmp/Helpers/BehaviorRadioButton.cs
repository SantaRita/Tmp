using Plugin.InputKit.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tmp.Helpers
{
    public class BehaviorRadioButton : Behavior<RadioButtonGroupView>
    {

        Label lblRequiredMessage = new Label();

        public BehaviorRadioButton(Label parlblRequiredMessage)
        {

            lblRequiredMessage = parlblRequiredMessage;
            lblRequiredMessage.IsVisible = false;
        }

        protected override void OnAttachedTo(RadioButtonGroupView RadioButton)
        {
            RadioButton.SelectedItemChanged += OnSelectedIndexChanged;
            base.OnAttachedTo(RadioButton);
        }

        protected override void OnDetachingFrom(RadioButtonGroupView RadioButton)
        {
            RadioButton.SelectedItemChanged -= OnSelectedIndexChanged;
            base.OnDetachingFrom(RadioButton);
        }

        void OnSelectedIndexChanged(object sender, EventArgs args)
        {

            var picker = (RadioButtonGroupView)sender;

            if (picker.SelectedIndex >= 0)
            {
                lblRequiredMessage.IsVisible = true;
                lblRequiredMessage.TextColor = Color.Red;
                ((RadioButtonGroupView)sender).BackgroundColor = Color.FromHex("#FBC5D0");
            }
            else
            {
                lblRequiredMessage.IsVisible = false;
                lblRequiredMessage.TextColor = Color.Default;
                ((RadioButtonGroupView)sender).BackgroundColor = Color.Default;
            }


        }
    }

}
