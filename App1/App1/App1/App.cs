﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Layout;
using Xamarin.Auth;
using Xamarin.Forms;

namespace App1
{
    public class App : Application
    {
        Dictionary<FirstPage, NavigationPage> Pages { get; set; }
        //the main Application and its functionalities
        public App()
        {
            MainPage  = new NavigationPage(new LoginPage());
            NavigateAsync(FirstPage.Login);
            /* if (UserLoggedIn)
            {
                MainPage = new NavigationPage(PrincipalPage);
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }*/
            Pages = new Dictionary<FirstPage, NavigationPage>();
            //Master = new LoginPage(this);
            /*BindingContext = new BaseViewModel(Navigation)
            {
                Title = "Xamarin CRM",
                Icon = "slideout.png"
            };
            //setup home page
            NavigateAsync(UserLoggedIn);*/
        }

        void SetDetailIfNull(Page page)
        {
            if (Detail == null && page != null)
                Detail = page;
        }

        public Page PrincipalPage { get; set; }

        public static bool UserLoggedIn { get; set; }

        public async Task NavigateAsync(FirstPage id)
        {
            Page newPage;
            if (!Pages.ContainsKey(id))
            {
                switch (id)
                {
                    case FirstPage.Login:
                        var page = new NavigationPage(new LoginPage()
                        {
                            Title = "Login",
                            Icon = new FileImageSource {File = "robot.png"}
                        });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case FirstPage.Contacts:
                        page = new NavigationPage(new ContactPage()
                        {
                            Title = "Contacts",
                            Icon = new FileImageSource {File = "robot.png"}
                        });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case FirstPage.Bank:
                        page = new NavigationPage(new BalcaoPage()
                        {
                            Title = "Bank Localization",
                            Icon = new FileImageSource {File = "robot.png"}
                        });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case FirstPage.Atm:
                        page = new NavigationPage(new AtmPage()
                        {
                            Title = "ATM localization",
                            Icon = new FileImageSource { File = "robot.png" },
                        });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case FirstPage.MainPage:
                        page = new NavigationPage(new PrincipalPage()
                        {
                            Title = "Main",
                            Icon = new FileImageSource { File = "robot.png" },
                        });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case FirstPage.Cards:
                        page = new NavigationPage(new cardPage()
                        {
                            Title = "Cards",
                            Icon = new FileImageSource { File = "robot.png" },
                        });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case FirstPage.transaction:
                        page = new NavigationPage(new transactionPage()
                        {
                            Title = "Transactions",
                            Icon = new FileImageSource { File = "robot.png" },
                        });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case FirstPage.Products:
                        page = new NavigationPage(new ProductPage()
                        {
                            Title = "Products",
                            Icon = new FileImageSource { File = "robot.png" },
                        });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                }
            }
            newPage = Pages[id];
            if (newPage == null)
                return;

            //pop to root for Windows Phone
            if (Detail != null && Device.OS == TargetPlatform.WinPhone)
            {
                await Detail.Navigation.PopToRootAsync();
            }

            Detail = newPage;
        }

        public enum FirstPage
        {
            Login,
            Contacts,
            Bank,
            Atm,
            MainPage,
            Products,
            Cards,
            transaction
        }

        public Page Detail { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
