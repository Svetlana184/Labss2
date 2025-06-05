using Microsoft.VisualBasic;
using System.Net.Http.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_bot.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

HttpClient client = new HttpClient();
Random random = new Random();

async Task<List<Fact>> GetFacts()
{
    List<Fact>? facts = await client.GetFromJsonAsync<List<Fact>>("http://localhost:5254/Facts");
    return facts!;
}

//var bot = new TelegramBotClient("7859735232:AAFROH90ZnkB79eBKrsn2BdPQGYHcUm3zHc", cancellationToken: cts.Token);
var token = "7859735232:AAFROH90ZnkB79eBKrsn2BdPQGYHcUm3zHc";
using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient(token, cancellationToken: cts.Token);
var me = await bot.GetMe();
await bot.DeleteWebhook();
await bot.DropPendingUpdates();
bot.OnError += OnError;
bot.OnMessage += OnMessage;
bot.OnUpdate += OnUpdate;

Console.WriteLine($"@{me.Username} is running... Press Escape to terminate");
while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
cts.Cancel(); // stop the bot
async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception);
    await Task.Delay(2000, cts.Token);
}
async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text is not { } text)
    {
        Console.WriteLine($"Received a message of type {msg.Type}");
    }
    else if (text.StartsWith("/"))
    {
        var space = text.IndexOf(' ');
        if (space < 0) space = text.Length;
        var command = text[..space].ToLower();
        if (command.LastIndexOf('@') is > 0 and int at)
            if (command[(at + 1)..].Equals(me.Username, StringComparison.OrdinalIgnoreCase))
                command = command[..at];
            else return;
        await OnCommand(command, text[space..].TrimStart(), msg);
    }
    else await OnTextMessage(msg);
}
async Task OnTextMessage(Message msg)
{
    Console.WriteLine($"Received command:{msg.Text} in {msg.Chat}");
    await OnCommand("/start", "", msg);
}
async Task OnCommand(string command, string args, Message msg)
{
    Console.WriteLine($"Received command:{command} in {args}");
    switch (command)
    {
        case "/start":
            await bot.SendMessage(msg.Chat, """
                <b><u>Bot menu</u></b>:
                /fact_of_the_day - ознакомьтесь с фактом дня
                /rules                    - изучите базовые правила днд
                /games                 - выберите концепцию для игры
                /generators         - сгенерируйте элемент игры
                /help                     - узнайте больше о функциях бота
                """, parseMode: ParseMode.Html, linkPreviewOptions: true,
                replyMarkup: new ReplyKeyboardRemove());
            break;
        case "/fact_of_the_day":
            Task<List<Fact>> task = Task.Run(() => GetFacts());
            List<Fact> facts = task.Result;
            int number = random.Next(1, facts.Count - 1);
            Fact fact_for_today = facts[number]; 
            await bot.SendMessage(msg.Chat, $"""
                <b><u>{fact_for_today.NameFact}</u></b>:
                
                {fact_for_today.DescriptionFact}
                """, parseMode: ParseMode.Html, linkPreviewOptions: true,
                replyMarkup: new ReplyKeyboardRemove());
            break;
        case "/rules":
            await bot.SendMessage(msg.Chat, "читай книгу игрока, идиот");
            break;
        case "/games":
            await bot.SendMessage(msg.Chat, "сам придумай");
            break;
        case "/generators":
            await bot.SendMessage(msg.Chat, "человек воин");
            break;
        case "/help":
            await bot.SendMessage(msg.Chat, "спасение утопающих - дело рук самих утопающих");
            break;
        default:
            await bot.SendMessage(msg.Chat, "бот не знает такой команды(");
            break;
    }
}

async Task OnUpdate(Update update)
{
    switch (update)
    {
        case { CallbackQuery: { } callbackQuery }: await OnCallbackQuery(callbackQuery); break;
        case { PollAnswer: { } pollAnswer }: await OnPollAnswer(pollAnswer); break;
        default: Console.WriteLine($"Received unhandled update {update.Type}"); break;
    }
    ;
}
async Task OnCallbackQuery(CallbackQuery callbackQuery)
{
    await bot.AnswerCallbackQuery(callbackQuery.Id, $"You selected {callbackQuery.Data}");
    await bot.SendMessage(callbackQuery.Message!.Chat, $"Received callback from inline button {callbackQuery.Data}");
}
async Task OnPollAnswer(PollAnswer pollAnswer)
{
    if (pollAnswer.User != null)
        await bot.SendMessage(pollAnswer.User.Id, $"You voted for option(s) id [{string.Join(',', pollAnswer.OptionIds)}]");
}