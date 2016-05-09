﻿using App1.Layout;
using App1.REST;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public class App : Application
    {
        private Dictionary<FirstPage, NavigationPage> Pages { get; set; }

        public static ManagerRESTService ManagerRest { get; private set; }

        //the main Application and its functionalities
        public App()
        {
            var users = new Users();
            //ManagerRest = new ManagerRESTService(new RESTService());
            MainPage = new NavigationPage(new LoginPage());
            //await   NavigateAsync(FirstPage.Login);
            NavigateAsync(FirstPage.Login).GetAwaiter();
            UserLoggedIn = false;
            Debug.WriteLine("App testing userloggedIn");
            if (UserLoggedIn)
            {
                //var Info = new AccountInfo();
                ManagerRest.CreateSession(users.User, users.Password);
                Debug.WriteLine("userloggedIn is true");
                MainPage = new NavigationPage(new PrincipalPage());
            }
            else
            {
                Debug.WriteLine("userloggedIn is false");
                MainPage = new NavigationPage(new LoginPage());
            }
            Pages = new Dictionary<FirstPage, NavigationPage>();
            //Master = new LoginPage(this);
            //setup home page
            //NavigateAsync(UserLoggedIn);
        }

        private void SetDetailIfNull(Page page)
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
                            Icon = new FileImageSource { File = "robot.png" }
                        });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;

                    case FirstPage.Contacts:
                        page = new NavigationPage(new ContactPage()
                        {
                            Title = "Contacts",
                            Icon = new FileImageSource { File = "robot.png" }
                        });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;

                    case FirstPage.Bank:
                        page = new NavigationPage(new BalcaoPage()
                        {
                            Title = "Bank Localization",
                            Icon = new FileImageSource { File = "robot.png" }
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
        // public static ITextSpeech Speech { get; set; }

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