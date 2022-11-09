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
        public const string masterySword = "Мастерство меча";
        public const string masteryAxe = "Мастерство секиры";
        public const string masteryDoubleSword = "Мастерство двуручного меча";
        public const string masteryDagger = "Мастерство скрытого оружия";
        public const string masteryPeak = "Мастерство копья";
        public const string masteryHummer = "Мастерство молота";
        public const string player = "Персонаж";
        public const string playerInfo = "Характеристики";
        public const string playerInventory = "Инвентарь";

        public const string accept = "Подтвердить";
        public const string goBack = "Назад";
    }
    public static class PhrasesType
    {
        public static List<string> registrationPhrases = new List<string> { ConstPhrase.start, ConstPhrase.createHero, ConstPhrase.male, ConstPhrase.female, ConstPhrase.accept, ConstPhrase.goBack,
            ConstPhrase.masterySword, ConstPhrase.masteryAxe, ConstPhrase.masteryDoubleSword, ConstPhrase.masteryDagger, ConstPhrase.masteryPeak, ConstPhrase.masteryAxe, ConstPhrase.masteryHummer,  };

        public static List<string> playerInfoPhrases = new List<string> { ConstPhrase.player, ConstPhrase.playerInfo, ConstPhrase.playerInventory};
    }
}
