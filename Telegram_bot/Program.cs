using System.Net.Http.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_bot;
using Telegram_bot.Model;
using Game = Telegram_bot.Model.Game;
using Telegram.Bot.Types;

using System.IO;

HttpClient client = new HttpClient();
Random random = new Random();
string[] filters = new string[3];

async Task<List<LocationImage>> GetLocImages()
{
    List<LocationImage>? images = await client.GetFromJsonAsync<List<LocationImage>>("http://localhost:5254/LocationImages");
    return images!;
}
async Task<List<Fact>> GetFacts()
{
    List<Fact>? facts = await client.GetFromJsonAsync<List<Fact>>("http://localhost:5254/Facts");
    return facts!;
}
async Task<List<Game>> GetGames()
{
    List<Game>? games = await client.GetFromJsonAsync<List<Game>>("http://localhost:5254/Games");
    return games!;
}
async Task<List<GeneratorLocation>> GetLocs()
{
    List<GeneratorLocation>? locs = await client.GetFromJsonAsync<List<GeneratorLocation>>("http://localhost:5254/Locations");
    return locs!;
}
async Task<List<GeneratorVibe>> GetVibes()
{
    List<GeneratorVibe>? vibes = await client.GetFromJsonAsync<List<GeneratorVibe>>("http://localhost:5254/Vibes");
    return vibes!;
}
async Task<List<Rule>> GetRules()
{
    List<Rule>? rules = await client.GetFromJsonAsync<List<Rule>>("http://localhost:5254/Rules");
    return rules!;
}
//var bot = new TelegramBotClient("", cancellationToken: cts.Token);
var token = "";
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
        Console.WriteLine($"Received a message of type {msg.Type} {msg.Chat}");
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
    //await OnCommand("/start", "", msg);
    
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
                /help                     - узнайте о боте и его функциях
                """, parseMode: ParseMode.Html, linkPreviewOptions: true,
                replyMarkup: new ReplyKeyboardRemove());
            break;
        case "/fact_of_the_day":
            Task<List<Fact>> task = Task.Run(() => GetFacts());
            List<Fact> facts = task.Result;
            int number = random.Next(0, facts.Count - 1);
            Fact fact_for_today = facts[number]; 
            await bot.SendMessage(msg.Chat, $"""
                <b><u>{fact_for_today.NameFact}</u></b>:
                
                {fact_for_today.DescriptionFact}
                """, parseMode: ParseMode.Html, linkPreviewOptions: true,
                replyMarkup: new ReplyKeyboardRemove());
            break;
        case "/rules":
            await bot.SendMessage(msg.Chat, "Выберите тип правил:", replyMarkup: new InlineKeyboardButton[][] {
                ["Базовые правила"],
                ["Бой"],
                ["Полезности"]
            });
            break;
        case "/games":
            await bot.SendMessage(msg.Chat, "Выберите от кого будет игра:", replyMarkup: new InlineKeyboardButton[][] {
                ["пользовательская"],
                ["системная"]
            });
            await bot.SendMessage(msg.Chat, "Выберите длительность:", replyMarkup: new InlineKeyboardButton[][] {
                ["ваншот"],
                ["2-3 партии"],
                ["модуль"],
                ["кампейн"]
            });
            await bot.SendMessage(msg.Chat, "Выберите, официальная ли игра и ее сеттинг", replyMarkup: new InlineKeyboardButton[][] {
                ["официальная"],
                ["неофициальная"]
            });
            
            break;
        case "/generators":
            await bot.SendMessage(msg.Chat, "Выберите генератор:", replyMarkup: new InlineKeyboardButton[][] {
                ["Генератор локаций"],
                ["Рандом генератор"]
            });
            break;
        case "/help":
            await bot.SendMessage(msg.Chat, """
                <b><u>SillyDndBot</u></b> поможет вам разобраться с механиками и внести что-то в свои игры

                <b><u>Dungeons & Dragons (D&D)</u></b> - - это культовая настольная ролевая игра в жанре фэнтези. 
                В ней группа игроков создаёт персонажей и, под руководством Мастера Подземелий (DM, ведущего), 
                отправляется в приключения, решают задачи, взаимодействуют с игровым миром и сражаются с монстрами. 

                <b><u>Функции бота</u></b>:
                /fact_of_the_day
                    <i>ознакомьтесь с фактом дня</i>
                /rules
                    <i>изучите основные правила днд, информацию о бое, ознакомьтесь с разными полезностями</i>
                /games
                    <i>выберите концепцию для игры</i>
                /generators
                    <i>сгенерируйте элемент игры: локацию или случайную вещь, которую можно будет внедрить в ваши игры</i>
                """, parseMode: ParseMode.Html, linkPreviewOptions: true,
                replyMarkup: new ReplyKeyboardRemove());
            break;
        
    }
}

async Task OnUpdate(Update update)
{
    switch (update)
    {
        case { CallbackQuery: { } callbackQuery }:
            {
                if(callbackQuery.Message!.Text != null)
                {
                    switch (callbackQuery.Message.Text)
                    {
                        case "Выберите генератор:":
                            {
                                await OnCallbackQueryGen(callbackQuery);
                                await OnCallbackQuery(callbackQuery);
                                break;
                            }
                        case "Выберите тип правил:":
                            {
                                await OnCallbackQueryRule(callbackQuery);
                                await OnCallbackQuery(callbackQuery);
                                break;
                            }
                        case "Выберите от кого будет игра:":
                            {
                                filters[0] = callbackQuery.Data!;
                                await OnCallbackQueryGame(callbackQuery);
                                await OnCallbackQuery(callbackQuery);
                                break;
                            }
                        case "Выберите длительность:":
                            {
                                filters[1] = callbackQuery.Data!;
                                await OnCallbackQueryGame(callbackQuery);
                                await OnCallbackQuery(callbackQuery);
                                break;
                            }
                        case "Выберите, официальная ли игра и ее сеттинг":
                            {
                                filters[2] = callbackQuery.Data!;
                                await OnCallbackQueryGame(callbackQuery);
                                await OnCallbackQuery(callbackQuery);
                                break;
                            }
                        default:
                            {
                                await OnCallbackQuery(callbackQuery);
                                break;
                            }

                    }
                }
                break;
            }
        case { PollAnswer: { } pollAnswer }: await OnPollAnswer(pollAnswer); break;
        default: Console.WriteLine($"Received unhandled update {update.Type}"); break;
    }
    ;
}

async Task OnCallbackQueryGame(CallbackQuery callbackQuery)
{
    if (filters.All(x => x != null))
    {
        List<Game> games = await GameGen();
        if (games.Count == 0)
        {
            await bot.SendMessage(callbackQuery.Message!.Chat, "Игр не найдено(((");
        }
        else
        {
            foreach (Game game in games)
            {
                await bot.SendMessage(callbackQuery.Message!.Chat, $"""
                <b><u>{game.NameGame}</u></b>:
                
                <i>{game.Source} * {game.Setting}</i>
                <i>{game.Genre} * {game.Duration}</i>

                ***

                <i>{game.Vibes}</i>

                ***

                {game.DescriptionGame}
                """, parseMode: ParseMode.Html, linkPreviewOptions: true,
                             replyMarkup: new ReplyKeyboardRemove());
            }
        }
    }
    
}

async Task OnCallbackQueryRule(CallbackQuery callbackQuery)
{
    List<Rule> rules = await RuleGen(callbackQuery.Data!);
    if (rules != null)
    {
        foreach (Rule rule in rules)
        {
            await bot.SendMessage(callbackQuery.Message!.Chat, $"""
                <b><u>{rule.NameRule}</u></b>:
                
                {rule.DescriptionRule}

                <a href="{rule.Link}">Ссылка</a>
                """, parseMode: ParseMode.Html, linkPreviewOptions: true,
                         replyMarkup: new ReplyKeyboardRemove());
        }
    }
    else await bot.SendMessage(callbackQuery.Message!.Chat, "Правил такого типа не найдено!");
}
async Task OnCallbackQueryGen(CallbackQuery callbackQuery)
{
    switch (callbackQuery.Data)
    {
        case "Генератор локаций":
            {
                await bot.SendMessage(callbackQuery.Message!.Chat, $"генерируем локацию...");
                GeneratorLocation loca = await LocationGen();
                List<LocationImage> images = await LocationImagesGen(loca.IdLocation);
                List<InputMediaPhoto> img = new List<InputMediaPhoto>();
                foreach (LocationImage image in images)
                {
                    InputMediaPhoto mediaPhoto = ConvertBytesToInputMediaPhoto(image.Source, "***");
                    img.Add(mediaPhoto);
                }
                if(img.Count != 0)
                {
                    await bot.SendMediaGroup(callbackQuery.Message!.Chat, img);
                }
                await bot.SendMessage(callbackQuery.Message!.Chat, $"""
                <b><u>{loca.NameLocation}</u></b>:
                
                {loca.DescriptionLocation}
                """, parseMode: ParseMode.Html, linkPreviewOptions: true,
                     replyMarkup: new ReplyKeyboardRemove());
                break;
            }
        case "Рандом генератор":
            {
                await bot.SendMessage(callbackQuery.Message!.Chat, $"генерируем...");
                GeneratorVibe vibe = await VibeGen();
                await bot.SendMessage(callbackQuery.Message!.Chat, $"""
                <b><u>{vibe.NameVibes}</u></b>:
                
                {vibe.TextVibes}
                """, parseMode: ParseMode.Html, linkPreviewOptions: true,
                     replyMarkup: new ReplyKeyboardRemove());
                break;
            }
    }
}

InputMediaPhoto ConvertBytesToInputMediaPhoto(byte[] imageBytes, string fileName)
{
    var stream = new MemoryStream(imageBytes);
    var inputMediaPhoto = new InputMediaPhoto(stream);
    inputMediaPhoto.Caption = fileName; // Задайте имя файла при необходимости
    return inputMediaPhoto;
}

async Task OnCallbackQuery(CallbackQuery callbackQuery)
{
    await bot.AnswerCallbackQuery(callbackQuery.Id, $"You selected {callbackQuery.Data}");
   
}
async Task<GeneratorLocation> LocationGen()
{
    Task<List<GeneratorLocation>> task = Task.Run(() => GetLocs());
    List<GeneratorLocation> locs = task.Result;
    int number = random.Next(0, locs.Count - 1);
    GeneratorLocation loc_for_today = locs[number];
    return loc_for_today;
}
async Task<List<LocationImage>> LocationImagesGen(int index)
{
    Task<List<LocationImage>> task = Task.Run(() => GetLocImages());
    List<LocationImage> locationImages = task.Result.Where(x => x.IdLocation == index).ToList();

    return locationImages;
}
async Task<GeneratorVibe> VibeGen()
{
    Task<List<GeneratorVibe>> task = Task.Run(() => GetVibes());
    List<GeneratorVibe> vibes = task.Result;
    int number = random.Next(0, vibes.Count - 1);
    GeneratorVibe vibe_for_today = vibes[number];
    return vibe_for_today;
}

async Task<List<Rule>> RuleGen(string filter)
{
    Task<List<Rule>> task = Task.Run(()=> GetRules());
    List<Rule> rules = task.Result.Where(x => x.TypeOfRule == filter).ToList();
    return rules;
}

async Task<List<Game>> GameGen()
{
    Task<List<Game>> task = Task.Run(() => GetGames());
    List<Game> games = task.Result.Where(x => x.FromWho == filters[0] && x.Duration == filters[1] && x.Oficiality == filters[2]).ToList();
    Array.Clear(filters);
    return games;
}

async Task OnPollAnswer(PollAnswer pollAnswer)
{
    if (pollAnswer.User != null)
        await bot.SendMessage(pollAnswer.User.Id, $"You voted for option(s) id [{string.Join(',', pollAnswer.OptionIds)}]");
}
