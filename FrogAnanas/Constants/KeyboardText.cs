using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Constants
{
    public static class RegistrationPhrase
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

        public const string accept = "Подтвердить";
        public const string goBack = "Назад";
    }
    public static class PlayerInfoPhrase
    {
        public const string player = "Персонаж";
        public const string playerEquip = "Экипировка";
        public const string playerInventory = "Инвентарь";
        public const string playerSkills = "Список навыков";

        public const string accept = "Подтвердить";
        public const string goBack = "Назад";
    }
    public static class AdventurePhrase
    {
        public const string START_ADVENTURE = "Начать приключение";
        public const string GREHOVKA = "Греховка";
        public const string TOWER = "Башня";
        public const string BAR = "Бар у тети Гали";
        public const string MARKET = "Барахолка \"У саныча\"";
        public const string TOWER_INFO = "Справочкник по башне";
        public const string ENTER_TOWER = "Войти в башню";
        public const string GO_BACK_TOWN = "Вернуться в город";
    }
    public static class PhrasesType
    {
        static PhrasesType()
        {
            SetLists();
        }
        private static void SetLists()
        {
            foreach (FieldInfo field in typeof(RegistrationPhrase).GetFields())
                registrationPhrases.Add(field.GetValue(field.Name) as string??String.Empty);

            foreach (FieldInfo field in typeof(PlayerInfoPhrase).GetFields())
                playerInfoPhrases.Add(field.GetValue(field.Name) as string ?? String.Empty);

            foreach (FieldInfo field in typeof(AdventurePhrase).GetFields())
                adventurePhrases.Add(field.GetValue(field.Name) as string ?? String.Empty);
        }
        public static List<string> registrationPhrases = new List<string>();
        public static List<string> playerInfoPhrases = new List<string>();
        public static List<string> adventurePhrases = new List<string>();
    }
}
