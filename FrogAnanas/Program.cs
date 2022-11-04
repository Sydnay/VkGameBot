// See https://aka.ms/new-console-template for more information
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

const string token = "vk1.a.7KyPdYKqp5ANTIBuBFlNKW3wXvLJFE3AsVoc8zm-mmU8KcInZ6-EgrhXxbdp17HSt6Q52gllyfFp2JynQ6ZGBWWYhDyfsE9S2ir9APQNNtL_dglHec77iUB2fSFBd7cRmRhpxeoVnQvTbdIa225E8PSPb6YNytUNiybnK9aIyjN6Q-oHT_F3bopuEOE6_81t5x82gbgr3tlkmYbkoPHvlA";
const string groupUri = "https://vk.com/club216986922";
VkBot bot = new VkBot(token, groupUri);
bot.OnMessageReceived += HandleStart;

Console.WriteLine("SstartReceiveng");
bot.Start();
Console.ReadLine();
void HandleStart(object? sender, MessageReceivedEventArgs e)
{

    var msg = e.Message.Text;
    if (msg != "/start")
    {
        var keyboard = new KeyboardBuilder(true);
        keyboard.AddButton("/start","", KeyboardButtonColor.Default);
        bot.Api.Messages.SendAsync(new MessagesSendParams
        {
            Message = "Нажмите /start чтобы начать",
            PeerId = e.Message.PeerId,
            RandomId = Math.Abs(Environment.TickCount),
            Keyboard = keyboard.Build()
        });
        Console.WriteLine($"Сообщение отправлено");
    }
    else
    {
        bot.Api.Messages.SendAsync(new MessagesSendParams
        {
            Message = "Добро пожаловать узбек",
            PeerId = e.Message.PeerId,
            RandomId = Math.Abs(Environment.TickCount),
        });
        bot.OnMessageReceived -= HandleStart;
        bot.OnMessageReceived += HandleMessage;
        bot.OnMessageReceived += HandleMessage1;
        Console.WriteLine($"Сообщение отправлено");
    }

}

void HandleMessage(object? sender, MessageReceivedEventArgs e)
{
    var msg = e.Message.Text;
    Console.WriteLine($"{msg}");
    if(msg == "ping")
    {
        bot.Api.Messages.SendAsync(new MessagesSendParams
        {
            Message = $"Message: {e.Message.Text} \nChatId: {e.Message.ChatId} \n UserId:{e.Message.UserId} \n UserId:{e.Message.Id} \n UserId:{e.Message.PeerId}",
            PeerId = e.Message.PeerId,
            RandomId = Math.Abs(Environment.TickCount),
        });
    }

    Console.WriteLine($"Сообщение {msg} отправлено");
}

void HandleMessage1(object? sender, MessageReceivedEventArgs e)
{
    var msg = e.Message.Text;
    Console.WriteLine($"{msg}");
    if(msg == "Добить босса")
    {
        bot.Api.Messages.SendAsync(new MessagesSendParams
        {
            Message = $"второй хендлер",
            PeerId = e.Message.PeerId,
            RandomId = Math.Abs(Environment.TickCount),
        });
    }

    Console.WriteLine($"Сообщение {msg} отправлено");
}





//static void MateshaNumberHandler(VkBot sender, MessageReceivedEventArgs args)
//{
//    int userAnswer = int.Parse(Regex.Match(args.Message.Text, MateshaNumberCommand).Value);
//    int validAnswer = args.PeerContext.Vars["validAnswer"];args.Message.PeerId.Value

//    var keyboard = new KeyboardBuilder()
//        .AddButton("+матеша", "", KeyboardButtonColor.Positive)
//        .AddButton("-матеша", "", KeyboardButtonColor.Negative);

//    sender.TemplateManager.Unregister(MateshaNumberCommand, args.Message.PeerId.Value);
//    args.PeerContext.Vars.Remove("validAnswer");
//    sender.Api.Messages.Send(new MessagesSendParams()
//    {
//        RandomId = Math.Abs(Environment.TickCount),
//        PeerId = args.Message.PeerId,
//        Message = (userAnswer == validAnswer)
//            ? "верный ответ! держи печенюху"
//            : $"ответ {userAnswer} невернен! верный ответ был: {validAnswer}, попробуйте еще раз",
//        Keyboard = keyboard.Build()
//    });
//}