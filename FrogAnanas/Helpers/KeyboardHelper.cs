using FrogAnanas.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Helpers
{
    public static class KeyboardHelper
    {
        public static MessageKeyboard CreateBuilder(KeyboardButtonColor color, params string[] messages)
        {
            var keyboard = new KeyboardBuilder();

            foreach (var msg in messages)
                keyboard.AddButton(msg, "", color);

            return keyboard.Build();
        }
        public static MessageKeyboard CreateMasteryKeyboard(params string[] messages)
        {
            var keyboard = new KeyboardBuilder();

            for (int i = 0; i < messages.Length; i++)
            {
                keyboard.AddButton(messages[i], "", KeyboardButtonColor.Default);
                if (i < messages.Length - 1)
                    keyboard.AddLine();
            }

            return keyboard.Build();
        }
    }
}
