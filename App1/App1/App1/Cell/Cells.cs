﻿using Xamarin.Forms;

namespace App1.Cell
{
    internal class Cells : ViewCell
    {
        private Label idLabel, nameLabel, fullLabel, LogoLabel, webLabel;

        public static readonly BindableProperty IDProperty =
    BindableProperty.Create("id", typeof(string), typeof(Cells), "");

        public string id
        {
            get { return (string)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly BindableProperty NameProperty =
    BindableProperty.Create("short_name", typeof(string), typeof(Cells), "");

        public string short_name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly BindableProperty full_nameProperty =
          BindableProperty.Create("full_name", typeof(string), typeof(Cells), "");

        public string full_name
        {
            get { return (string)GetValue(full_nameProperty); }
            set { SetValue(full_nameProperty, value); }
        }

        public static readonly BindableProperty LogoProperty =
          BindableProperty.Create("logo", typeof(string), typeof(Cells), "");

        public string Logo
        {
            get { return (string)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        public static readonly BindableProperty WebsiteProperty =
          BindableProperty.Create("website", typeof(string), typeof(Cells), "");

        public string website
        {
            get { return (string)GetValue(WebsiteProperty); }
            set { SetValue(WebsiteProperty, value); }
        }

        public static readonly BindableProperty MenuTitleProperty =
          BindableProperty.Create("menu", typeof(string), typeof(Cells), "");

        public string MenuTitle
        {
            get { return (string)GetValue(MenuTitleProperty); }
            set { SetValue(MenuTitleProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                idLabel.Text = id;
                nameLabel.Text = short_name;
                fullLabel.Text = full_name;
                LogoLabel.Text = Logo;
                webLabel.Text = website;
            }
        }
    }
}