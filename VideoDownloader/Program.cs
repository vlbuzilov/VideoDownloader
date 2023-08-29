using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Telegram.Bot;
using Telegram.Bot.Args;
using VideoDownloader.SeleniumWebDriver;

namespace VideoDownloader
{
    internal class Program
    {
        private static TelegramBotClient _client;
        private static string token = SettingsData.token;
        private static IWebDriver _webDriver;
        private static SeleniumVideoDownloader seleniumVideoDownloader;

        public static void Main(string[] args)
        {
            _client = new TelegramBotClient(token);
            _webDriver = new ChromeDriver();
            
            _client.StartReceiving();
            _client.OnMessage += OnMessageHandler;
            Console.ReadLine();
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message != null && message.Text != null)
            {
                if (message.Text == "/start")
                {
                    await _client.SendTextMessageAsync(message.Chat.Id, "Please, put here link on your video:");
                }
                else if (message.Text.Contains("https://www.youtube.com"))
                {
                    seleniumVideoDownloader = new SeleniumVideoDownloader(_webDriver);
                    seleniumVideoDownloader.PasteVideoLinkAndDownload(message.Text);
                    await _client.SendTextMessageAsync(message.Chat.Id, "video is downloading");
                }
                else
                {
                    await _client.SendTextMessageAsync(message.Chat.Id, "Please, put correct link");
                }
            }
        }
        
    }
}