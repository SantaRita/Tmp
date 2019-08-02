using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tmp.Helpers
{
    public class BehaviorPicker : Behavior<Picker>
    {

        Label lblRequiredMessage = new Label();

        public BehaviorPicker(Label parlblRequiredMessage)
        {

            lblRequiredMessage = parlblRequiredMessage;
            lblRequiredMessage.IsVisible = false;
        }

        protected override void OnAttachedTo(Picker Picker)
        {
            Picker.SelectedIndexChanged += OnSelectedIndexChanged;
            base.OnAttachedTo(Picker);
        }

        protected override void OnDetachingFrom(Picker Picker)
        {
            Picker.SelectedIndexChanged -= OnSelectedIndexChanged;
            base.OnDetachingFrom(Picker);
        }

        void OnSelectedIndexChanged(object sender, EventArgs args)
        {

            var picker = (Picker)sender;

            if ( picker.SelectedIndex  >= 0 )
            {
                lblRequiredMessage.IsVisible = true;
                lblRequiredMessage.TextColor = Color.Red;
                ((Picker)sender).BackgroundColor = Color.FromHex("#FBC5D0");
            } else
            {
                lblRequiredMessage.IsVisible = false;
                lblRequiredMessage.TextColor = Color.Default;
                ((Picker)sender).BackgroundColor = Color.Default;
            }


        }
    }

}
