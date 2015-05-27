using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using POEApi.Infrastructure;

namespace POEApi.Model
{
    internal class ProxyMapper
    {
        internal const string STACKSIZE = "Stack Size";
        internal const string STASH = "Stash";
        public const string QUALITY = "Quality";
        private static Regex qualityRx = new Regex("[+]{1}(?<quality>[0-9]{1,2}).*");

        #region   Orb Types  
        private static Dictionary<string, OrbType> orbMap = new Dictionary<string, OrbType>()           
        {
            { "Сфера хаоса", OrbType.Chaos },
            { "Божественная сфера", OrbType.Divine },
            { "Сфера царей", OrbType.Regal },
            { "Сфера усиления", OrbType.Augmentation },
            { "Сфера алхимии", OrbType.Alchemy },
            { "Осколок алхимии", OrbType.AlchemyShard },
            { "Цветная сфера", OrbType.Chromatic },
            { "превращения", OrbType.Transmutation },
            { "Сфера очищения", OrbType.Scouring },
            { "Стекольная масса",OrbType.GlassblowersBauble },
            { "Резец картографа", OrbType.Chisel },
            { "Призма камнереза", OrbType.GemCutterPrism },
            { "перемен", OrbType.Alteration },
            { "Сфера удачи", OrbType.Chance },
            { "Сфера раскаяния", OrbType.Regret },
            { "Сфера возвышения", OrbType.Exalted },
            { "Деталь Доспеха", OrbType.ArmourersScrap },
            { "Благодатная сфера", OrbType.Blessed},
            { "Точильный камень", OrbType.BlacksmithsWhetstone },
            { "Обрывок свитка", OrbType.ScrollFragment },
            { "Сфера златокузнеца", OrbType.JewelersOrb },
            { "Свиток мудрости", OrbType.ScrollofWisdom },
            { "Сфера соединения", OrbType.Fusing },
            { "Свиток портала", OrbType.PortalScroll },
            { "Перо белого роа", OrbType.AlbinaRhoaFeather},
            { "Зеркало Каландры", OrbType.Mirror },
            { "Сфера вечного", OrbType.Eternal},
            { "Слепок", OrbType.Imprint },
            { "Сфера ваал", OrbType.VaalOrb }
        };
        #endregion

        private static string getPropertyByName(List<JSONProxy.Property> properties, string name)
        {
            JSONProxy.Property prop = properties.Find(p => p.Name == name);

            if (prop == null)
                return string.Empty;

            return (prop.Values[0] as object[])[0].ToString();
        }
        
        internal static OrbType GetOrbType(JSONProxy.Item item)
        {
            return GetOrbType(item.TypeLine);
        }

        internal static OrbType GetOrbType(string name)
        {
            try
            {
                return orbMap.First(m => name.Contains(m.Key)).Value;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                var message = "ProxyMapper.GetOrbType Failed! ItemType = " + name;
                Logger.Log(message);
                throw new Exception(message);
            }
        }

        internal static List<Property> GetProperties(List<JSONProxy.Property> properties)
        {
            return properties.Select(p => new Property(p)).ToList();
        }

        internal static List<Requirement> GetRequirements(List<JSONProxy.Requirement> requirements)
        {
            if (requirements == null)
                return new List<Requirement>();

            return requirements.Select(r => new Requirement(r)).ToList();
        }

        internal static StackInfo GetStackInfo(List<JSONProxy.Property> list)
        {
            JSONProxy.Property stackSize = list.Find(p => p.Name == STACKSIZE);
            if (stackSize == null)
                return new StackInfo(1, 1);

            string[] stackInfo = getPropertyByName(list, STACKSIZE).Split('/');

            return new StackInfo(Convert.ToInt32(stackInfo[0]), Convert.ToInt32(stackInfo[1]));
        }

        internal static int GetQuality(List<JSONProxy.Property> properties)
        {   
            return Convert.ToInt32(qualityRx.Match(getPropertyByName(properties, QUALITY)).Groups["quality"].Value);
        }

        internal static List<Tab> GetTabs(List<JSONProxy.Tab> tabs)
        {
            try
            {
                return tabs.Select(t => new Tab(t)).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log("Error in ProxyMapper.GetTabs: " + ex.ToString());
                throw;
            }
        }
    }
}
