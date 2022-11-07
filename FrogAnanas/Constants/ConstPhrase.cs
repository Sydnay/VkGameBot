using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Constants
{
    public static class ConstPhrase
    {
        public const string start = "/start";
        public const string createHero = "Создать персонажа";
        public const string male = "Мужик";
        public const string female = "Вумен";
        public const string player = "Персонаж";
        public const string playerInfo = "Характеристики";
        public const string playerInventory = "Инвентарь";
    }
    public static class PhrasesType
    {
        public static List<string> registrationPhrases = new List<string> { ConstPhrase.start, ConstPhrase.createHero, ConstPhrase.male, ConstPhrase.female };
        public static List<string> playerInfoPhrases = new List<string> { ConstPhrase.player, ConstPhrase.playerInfo, ConstPhrase.playerInventory};
    }
}
