﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VSStatsEditor
{
    public class GameData
    {
        /* Paths */
        public static string ZndToAsmTool = String.Format("{0}{1}VSTools{1}znddasm.exe", System.Environment.CurrentDirectory, Path.DirectorySeparatorChar);
        public static string AsmToZndTool = String.Format("{0}{1}FASM{1}FASM.exe", System.Environment.CurrentDirectory, Path.DirectorySeparatorChar);
        public static string DefaultZonesDir = String.Format("{0}{1}Zones", System.Environment.CurrentDirectory, Path.DirectorySeparatorChar);

        /* Stat headers */
        public static string EnemyNameHeader = ".CharacterDataName:";
        public static string HPHeader = ".CharacterDataHP";
        public static string MPHeader = ".CharacterDataMP";
        public static string STRHeader = ".CharacterDataSTR";
        public static string INTHeader = ".CharacterDataINT";
        public static string AGIHeader = ".CharacterDataAGL";
        public static string RunSpeedHeader = ".CharacterDataSPD";

        /* Equipment headers */
        public static string WeaponNameHeader = ".WeaponBladeItemName";
        public static string WeaponGripNameHeader = ".WeaponGripItemName";
        public static string WeaponGem1Header = ".WeaponGem1ItemName";
        public static string WeaponGem2Header = ".WeaponGem2ItemName";
        public static string WeaponGem3Header = ".WeaponGem3ItemName";
        public static string WeaponMatHeader = ".WeaponMaterialID";
        public static string WeaponDropChanceHeader = ".WeaponDropChance";

        public static string ShieldNameHeader = ".ShieldItemName";
        public static string ShieldGem1Header = ".ShieldGem1ItemName";
        public static string ShieldGem2Header = ".ShieldGem2ItemName";
        public static string ShieldGem3Header = ".ShieldGem3ItemName";
        public static string ShieldMatHeader = ".ShieldMaterialID";
        public static string ShieldDropChanceHeader = ".ShieldDropChance";

        public static string AccNameHeader = ".AccessoryItemName";
        public static string AccDropChanceHeader = ".AccessoryDropChance";

        public static string BodyPart0HPHeader = ".BodyPart0DataHP:";
        public static string BodyPart1HPHeader = ".BodyPart1DataHP:";
        public static string BodyPart2HPHeader = ".BodyPart2DataHP:";
        public static string BodyPart3HPHeader = ".BodyPart3DataHP:";
        public static string BodyPart4HPHeader = ".BodyPart4DataHP:";
        public static string BodyPart5HPHeader = ".BodyPart5DataHP:";
        public static string BodyPart0NameHeader = ".BodyPart0ArmourItemName";
        public static string BodyPart1NameHeader = ".BodyPart1ArmourItemName";
        public static string BodyPart2NameHeader = ".BodyPart2ArmourItemName";
        public static string BodyPart3NameHeader = ".BodyPart3ArmourItemName";
        public static string BodyPart4NameHeader = ".BodyPart4ArmourItemName";
        public static string BodyPart5NameHeader = ".BodyPart5ArmourItemName";
        public static string BodyPart0DropChanceHeader = ".BodyPart0ArmourDropChance";
        public static string BodyPart1DropChanceHeader = ".BodyPart1ArmourDropChance";
        public static string BodyPart2DropChanceHeader = ".BodyPart2ArmourDropChance";
        public static string BodyPart3DropChanceHeader = ".BodyPart3ArmourDropChance";
        public static string BodyPart4DropChanceHeader = ".BodyPart4ArmourDropChance";
        public static string BodyPart5DropChanceHeader = ".BodyPart5ArmourDropChance";
        public static string BodyPart0MatHeader = ".BodyPart0ArmourMaterialID";
        public static string BodyPart1MatHeader = ".BodyPart1ArmourMaterialID";
        public static string BodyPart2MatHeader = ".BodyPart2ArmourMaterialID";
        public static string BodyPart3MatHeader = ".BodyPart3ArmourMaterialID";
        public static string BodyPart4MatHeader = ".BodyPart4ArmourMaterialID";
        public static string BodyPart5MatHeader = ".BodyPart5ArmourMaterialID";

        public static string EndStatsSection = "TIMSection:";

        public static Dictionary<string, string> Materials = new Dictionary<string, string>
        {
            {"00", "None"},
            {"01", "Wood"},
            {"02", "Leather"},
            {"03", "Bronze"},
            {"04", "Iron"},
            {"05", "Hagane"},
            {"06", "Silver"},
            {"07", "Damascus"}
        };

        public static Dictionary<string, string> Blades = new Dictionary<string, string>
        {
            {"0000", "None"},
            {"DAG", "---DAGGERS---"},
            {"0001", "Battle Knife"},
            {"0002", "Scramasax"},
            {"0003", "Dirk"},
            {"0004", "Throwing Knife"},
            {"0005", "Kudi"},
            {"0006", "Cinquedea"},
            {"0007", "Kris"},
            {"0008", "Hatchet"},
            {"0009", "Khukuri"},
            {"000A", "Baselard"},
            {"000B", "Stiletto"},
            {"000C", "Jamadhar"},
            {"SWD", "---SWORDS---"},
            {"000D", "Spatha"},
            {"000E", "Scimitar"},
            {"000F", "Rapier"},
            {"0010", "Short Sword"},
            {"0011", "Firangi"},
            {"0012", "Shamshir"},
            {"0013", "Falchion"},
            {"0014", "Shotel"},
            {"0015", "Khora"},
            {"0016", "Khopesh"},
            {"0017", "Wakizashi"},
            {"0018", "Rhomphaia"},
            {"GSWD", "---GREAT SWORDS---"},
            {"0019", "Broad Sword"},
            {"001A", "Norse Sword"},
            {"001B", "Katana"},
            {"001C", "Executioner"},
            {"001D", "Claymore"},
            {"001E", "Schiavona"},
            {"001F", "Bastard Sword"},
            {"0020", "Nodachi"},
            {"0021", "Rune Blade"},
            {"0022", "Holy Win"},
            {"AXMC", "---AXES/MACES---"},
            {"0023", "Hand Axe"},
            {"0024", "Battle Axe"},
            {"0025", "Francisca"},
            {"0026", "Tabarzin"},
            {"0027", "Chamkaq"},
            {"0028", "Tabar"},
            {"0029", "Bullova"},
            {"002A", "Crescent"},
            {"002B", "Goblin Club"},
            {"002C", "Spiked Club"},
            {"002D", "Ball Mace"},
            {"002E", "Footman's Mace"},
            {"002F", "Morning Star"},
            {"0030", "War Hammer"},
            {"0031", "Bec de Corbin"},
            {"0032", "War Maul"},
            {"GAXE", "---GREAT AXES---"},
            {"0033", "Guisarme"},
            {"0034", "Large Crescent"},
            {"0035", "Sabre Halberd"},
            {"0036", "Balbriggan"},
            {"0037", "Double Blade"},
            {"0038", "Halberd"},
            {"STAF", "---STAVES---"},
            {"0039", "Wizard Staff"},
            {"003A", "Clergy Rod"},
            {"003B", "Summoner Baton"},
            {"003C", "Shamanic Staff"},
            {"003D", "Bishop's Crosier"},
            {"003E", "Sage's Cane"},
            {"GMACE", "---GREAT MACES---"},
            {"003F", "Langdebeve"},
            {"0040", "Sabre Mace"},
            {"0041", "Footman's Mace"},
            {"0042", "Gloomwing"},
            {"0043", "Mjolnir"},
            {"0044", "Griever"},
            {"0045", "Destroyer"},
            {"0046", "Hand of Light"},
            {"POL", "---POLEARMS---"},
            {"0047", "Spear"},
            {"0048", "Glaive"},
            {"0049", "Scorpion"},
            {"004A", "Corcesca"},
            {"004B", "Trident"},
            {"004C", "Awl Pike"},
            {"004D", "Boar Spear"},
            {"004E", "Fauchard"},
            {"004F", "Voulge"},
            {"0050", "Pole Axe"},
            {"0051", "Bardysh"},
            {"0052", "Brandestoc"},
            {"XBOX", "---CROSSBOWS---"},
            {"0053", "Gastraph Bow"},
            {"0054", "Light Crossbow"},
            {"0055", "Target Bow"},
            {"0056", "Windlass"},
            {"0057", "Cranequin"},
            {"0058", "Lug Crossbow"},
            {"0059", "Siege Bow"},
            {"005A", "Arbalest"},
            //{"005B", "untitled"},
            //{"005C", "untitled"},
            //{"005D", "untitled"},
            //{"005E", "untitled"},
            //{"005F", "untitled"},
        };

        public static Dictionary<string, string> Grips = new Dictionary<string, string>
        {
            {"0000", "None"},
            { "SWDG", "---SWORD GRIPS---"},
            {"0060", "Short Hilt"},
            {"0061", "Swept Hilt"},
            {"0062", "Cross Guard"},
            {"0063", "Knuckle Guard"},
            {"0064", "Counter Guard"},
            {"0065", "Side Ring"},
            {"0066", "Power Palm"},
            {"0067", "Murderer's Hilt"},
            {"0068", "Spiral Hilt"},
            {"AXMCSTVGP", "---AXE/MACE/STAFF GRIPS---"},
            {"0069", "Wooden Grip"},
            {"006A", "Sand Face"},
            {"006B", "Czekan Type"},
            {"006C", "Sarissa Grip"},
            {"006D", "Gendarme"},
            {"006E", "Heavy Grip"},
            {"006F", "Runkasyle"},
            {"0070", "Bhuj Type"},
            {"0071", "Grimoire Grip"},
            {"0072", "Elephant"},
            {"POLG", "---POLEARM GRIPS---"},
            {"0073", "Wooden Pole"},
            {"0074", "Spiculum Pole"},
            {"0075", "Winged Pole"},
            {"0076", "Framea Pole"},
            {"0077", "Ahlspies"},
            {"0078", "Spiral Pole"},
            {"XBOXG", "---CROSSBOW GRIPS---"},
            {"0079", "Simple Bolt"},
            {"007A", "Steel Bolt"},
            {"007B", "Javelin Bolt"},
            {"007C", "Falarica Bolt"},
            {"007D", "Stone Bullet"},
            {"007E", "Sonic Bullet"},
        };

        public static Dictionary<string, string> Shields = new Dictionary<string, string>
        {
            {"0000", "None"},
            {"007F", "Buckler"},
            {"0080", "Pelta Shield"},
            {"0081", "Targe"},
            {"0082", "Quad Shield"},
            {"0083", "Circle Shield"},
            {"0084", "Tower Shield"},
            {"0085", "Spiked Shield"},
            {"0086", "Round Shield"},
            {"0087", "Kite Shield"},
            {"0088", "Casserole Shield"},
            {"0089", "Heater Shield"},
            {"008A", "Oval Shield"},
            {"008B", "Knight Shield"},
            {"008C", "Hoplite Shield"},
            {"008D", "Jazeraint Shield"},
            {"008E", "Dread Shield"},
        };

        public static Dictionary<string, string> OtherItems = new Dictionary<string, string>
        {
            {"0000", "None"},
            {"HEAD", "---HELMETS---"},
            {"008F", "Bandana"},
            {"0090", "Bear Mask"},
            {"0091", "Wizard Hat"},
            {"0092", "Bone Helm"},
            {"0093", "Chain Coif"},
            {"0094", "Spangenhelm"},
            {"0095", "Cabasset"},
            {"0096", "Sallet"},
            {"0097", "Barbut"},
            {"0098", "Basinet"},
            {"0099", "Armet"},
            {"009A", "Close Helm"},
            {"009B", "Burgonet"},
            {"009C", "Hoplite Helm"},
            {"009D", "Jazeraint Helm"},
            {"009E", "Dread Helm"},
            {"BODY", "---BODY ARMOR---"},
            {"009F", "Jerkin"},
            {"00A0", "Hauberk"},
            {"00A1", "Wizard Robe"},
            {"00A2", "Cuirass"},
            {"00A3", "Banded Mail"},
            {"00A4", "Ring Mail"},
            {"00A5", "Chain Mail"},
            {"00A6", "Breastplate"},
            {"00A7", "Segmentata"},
            {"00A8", "Scale Armor"},
            {"00A9", "Brigandine"},
            {"00AA", "Plate Mail"},
            {"00AB", "Fluted Armor"},
            {"00AC", "Hoplite Armor"},
            {"00AD", "Jazeraint Armor"},
            {"00AE", "Dread Armor"},
            {"LEG", "---LEGGINGS---"},
            {"00AF", "Sandals"},
            {"00B0", "Boots"},
            {"00B1", "Long Boots"},
            {"00B2", "Cuisse"},
            {"00B3", "Light Greave"},
            {"00B4", "Ring Leggings"},
            {"00B5", "Chain Leggings"},
            {"00B6", "Fusskampf"},
            {"00B7", "Poleyn"},
            {"00B8", "Jambeau"},
            {"00B9", "Missaglia"},
            {"00BA", "Plate Leggings"},
            {"00BB", "Fluted Leggings"},
            {"00BC", "Hoplite Leggings"},
            {"00BD", "Jazeraint Leggings"},
            {"00BE", "Dread Leggings"},
            {"ARM", "---ARMLETS---"},
            {"00BF", "Bandage"},
            {"00C0", "Leather Glove"},
            {"00C1", "Reinforced Glove"},
            {"00C2", "Knuckles"},
            {"00C3", "Ring Sleeve"},
            {"00C4", "Chain Sleeve"},
            {"00C5", "Gauntlet"},
            {"00C6", "Vambrace"},
            {"00C7", "Plate Glove"},
            {"00C8", "Rondanche"},
            {"00C9", "Tilt Glove"},
            {"00CA", "Freiturnier"},
            {"00CB", "Fluted Glove"},
            {"00CC", "Hoplite Glove"},
            {"00CD", "Jazeraint Glove"},
            {"00CE", "Dread Glove"},
            //{"00CF", "untitled"},
            //{"00D0", "untitled"},
            //{"00D1", "untitled"},
            //{"00D2", "untitled"},
            //{"00D3", "untitled"},
            //{"00D4", "untitled"},
            //{"00D5", "untitled"},
            //{"00D6", "untitled"},
            //{"00D7", "untitled"},
            //{"00D8", "untitled"},
            //{"00D9", "untitled"},
            //{"00DA", "untitled"},
            //{"00DB", "untitled"},
            //{"00DC", "untitled"},
            //{"00DD", "untitled"},
            //{"00DE", "untitled"},
            //{"00FE", "Wood"},
            //{"00FF", "Leather"},
            //{"0100", "Bronze"},
            //{"0101", "Iron"},
            //{"0102", "Hagane"},
            //{"0103", "Silver"},
            //{"0104", "Damascus"},
            {"MISC", "---MISC---"},
            {"0143", "Cure Root"},
            {"0144", "Cure Bulb"},
            {"0145", "Cure Tonic"},
            {"0146", "Cure Potion"},
            {"0147", "Mana Root"},
            {"0148", "Mana Bulb"},
            {"0149", "Mana Tonic"},
            {"014A", "Mana Potion"},
            {"014B", "Vera Root"},
            {"014C", "Vera Bulb"},
            {"014D", "Vera Tonic"},
            {"014E", "Vera Potion"},
            {"014F", "Acolyte's Nostrum"},
            {"0150", "Saint's Nostrum"},
            {"0151", "Alchemist's Reagent"},
            {"0152", "Sorcerer's Reagent"},
            {"0153", "Yggdrasil's Tears"},
            {"0154", "Faerie Chortle"},
            {"0155", "Spirit Orison"},
            {"0156", "Angelic Paean"},
            {"0157", "Panacea"},
            {"0158", "Snowfly Draught"},
            {"0159", "Faerie Wing"},
            {"015A", "Elixir of Kings"},
            {"015B", "Elixir of Sages"},
            {"015C", "Elixir of Dragoons"},
            {"015D", "Elixir of Queens"},
            {"015E", "Elixir of Mages"},
            {"015F", "Valens"},
            {"0160", "Prudens"},
            {"0161", "Volare"},
            {"0162", "Audentia"},
            {"0163", "Virtus"},
            {"0164", "Eye of Argon"},
            //{"0165", "untitled"},
            //{"0166", "untitled"},
            //{"0167", "untitled"},
            //{"0168", "untitled"},
            //{"0169", "untitled"},
            //{"016A", "untitled"},
            //{"016B", "untitled"},
            //{"016C", "untitled"},
            //{"016D", "untitled"},
            //{"016E", "untitled"},
            //{"016F", "untitled"},
            //{"0170", "untitled"},
            //{"0171", "untitled"},
            //{"0172", "untitled"},
            //{"0173", "untitled"},
            //{"0174", "untitled"},
            //{"0175", "untitled"},
            //{"0176", "untitled"},
            //{"0177", "untitled"},
            //{"0178", "untitled"},
            //{"0179", "untitled"},
            //{"017A", "untitled"},
            //{"017B", "untitled"},
            //{"017C", "untitled"},
            //{"017D", "untitled"},
            //{"017E", "untitled"},
            //{"017F", "untitled"},
            //{"0180", "untitled"},
            //{"0181", "untitled"},
            {"GRIM", "---GRIMOIRES---"},
            {"0182", "Grimoire Zephyr"},
            {"0183", "Grimoire Teslae"},
            {"0184", "Grimoire Incendie"},
            {"0185", "Grimoire Terre"},
            {"0186", "Grimoire Glace"},
            {"0187", "Grimoire Lux"},
            {"0188", "Grimoire Patir"},
            {"0189", "Grimoire Exsorcer"},
            {"018A", "Grimoire Banish"},
            {"018B", "Grimoire Demolir"},
            //{"018C", "untitled"},
            //{"018D", "untitled"},
            //{"018E", "untitled"},
            {"018F", "Grimoire Foudre"},
            //{"0190", "untitled"},
            //{"0191", "untitled"},
            //{"0192", "untitled"},
            {"0193", "Grimoire Flamme"},
            //{"0194", "untitled"},
            //{"0195", "untitled"},
            //{"0196", "untitled"},
            {"0197", "Grimoire Gaea"},
            //{"0198", "untitled"},
            //{"0199", "untitled"},
            //{"019A", "untitled"},
            {"019B", "Grimoire Avalanche"},
            //{"019C", "untitled"},
            //{"019D", "untitled"},
            //{"019E", "untitled"},
            {"019F", "Grimoire Radius"},
            //{"01A0", "untitled"},
            //{"01A1", "untitled"},
            //{"01A2", "untitled"},
            {"01A3", "Grimoire Meteore"},
            //{"01A4", "untitled"},
            //{"01A5", "untitled"},
            //{"01A6", "untitled"},
            {"01A7", "Grimoire Egout"},
            {"01A8", "Grimoire Demance"},
            {"01A9", "Grimoire Guerir"},
            {"01AA", "Grimoire Mollesse"},
            {"01AB", "Grimoire Antidote"},
            {"01AC", "Grimoire Benir"},
            {"01AD", "Grimoire Purifier"},
            {"01AE", "Grimoire Vie"},
            {"01AF", "Grimoire Intensite"},
            {"01B0", "Grimoire Debile"},
            {"01B1", "Grimoire Eclairer"},
            {"01B2", "Grimoire Nuageux"},
            {"01B3", "Grimoire Agilite"},
            {"01B4", "Grimoire Tardif"},
            {"01B5", "Grimoire Ameliorer"},
            {"01B6", "Grimoire Deteriorer"},
            {"01B7", "Grimoire Muet"},
            {"01B8", "Grimoire Annuler"},
            {"01B9", "Grimoire Paralysie"},
            {"01BA", "Grimoire Venin"},
            {"01BB", "Grimoire Fleau"},
            {"01BC", "Grimoire Halte"},
            {"01BD", "Grimoire Dissiper"},
            {"01BE", "Grimoire Clef"},
            {"01BF", "Grimoire Visible"},
            {"01C0", "Grimoire Analyse"},
            {"01C1", "Grimoire Sylphe"},
            {"01C2", "Grimoire Salamandre"},
            {"01C3", "Grimoire Gnome"},
            {"01C4", "Grimoire Undine"},
            {"01C5", "Grimoire Parebrise"},
            {"01C6", "Grimoire Ignifuge"},
            {"01C7", "Grimoire Rempart"},
            {"01C8", "Grimoire Barrer"},
            //{"01C9", "untitled"},
            {"KEYS", "---KEYS/SIGILS---"},
            {"01CA", "Bronze Key"},
            {"01CB", "Iron Key"},
            {"01CC", "Silver Key"},
            {"01CD", "Gold Key"},
            {"01CE", "Platinum Key"},
            {"01CF", "Steel Key"},
            {"01D0", "Crimson Key"},
            {"01D1", "Chest Key"},
            {"01D2", "Chamomile Sigil"},
            {"01D3", "Lily Sigil"},
            {"01D4", "Tearose Sigil"},
            {"01D5", "Clematis Sigil"},
            {"01D6", "Hyacinth Sigil"},
            {"01D7", "Fern Sigil"},
            {"01D8", "Aster Sigil"},
            {"01D9", "Eulelia Sigil"},
            {"01DA", "Melissa Sigil"},
            {"01DB", "Calla Sigil"},
            {"01DC", "Laurel Sigil"},
            {"01DD", "Acacia Sigil"},
            {"01DE", "Palm Sigil"},
            {"01DF", "Kalmia Sigil"},
            {"01E0", "Colombine Sigil"},
            {"01E1", "Anemone Sigil"},
            {"01E2", "Verbena Sigil"},
            {"01E3", "Schirra Sigil"},
            {"01E4", "Marigold Sigil"},
            {"01E5", "Azalea Sigil"},
            {"01E6", "Tigertail Sigil"},
            {"01E7", "Stock Sigil"},
            {"01E8", "Cattleya Sigil"},
            {"01E9", "Mandrake Sigil"},
            //{"01EA", "untitled"},
            //{"01EB", "untitled"},
            //{"01EC", "untitled"},
            //{"01ED", "untitled"},
            //{"01EE", "untitled"},
            //{"01EF", "untitled"},
            //{"01F0", "untitled"},
            //{"01F1", "untitled"},
            //{"01F2", "untitled"},
            //{"01F3", "untitled"},
            //{"01F4", "untitled"},
            //{"01F5", "untitled"},
            //{"01F6", "untitled"},
            //{"01F7", "untitled"},
            //{"01F8", "untitled"},
            //{"01F9", "untitled"},
            //{"01FA", "untitled"},
            //{"01FB", "untitled"},
            //{"01FC", "untitled"},
            //{"01FD", "untitled"},
            //{"01FE", "untitled"},
            //{"01FF", "untitled"}
        };

        public static Dictionary<string, string> Acc = new Dictionary<string, string>
        {
            {"0000", "None"},
            {"00DF", "Rood Necklace"},
            {"00E0", "Rune Earrings"},
            {"00E1", "Lionhead"},
            {"00E2", "Rusted Nails"},
            {"00E3", "Sylphid Ring"},
            {"00E4", "Marduk"},
            {"00E5", "Salamander Ring"},
            {"00E6", "Tamulis Tongue"},
            {"00E7", "Gnome Bracelet"},
            {"00E8", "Palolo's Ring"},
            {"00E9", "Undine Bracelet"},
            {"00EA", "Talian Ring"},
            {"00EB", "Agrias's Balm"},
            {"00EC", "Kadesh Ring"},
            {"00ED", "Agrippa's Choker"},
            {"00EE", "Diadra's Earring"},
            {"00EF", "Titan's Ring"},
            {"00F0", "Lau Fei's Armlet"},
            {"00F1", "Swan Song"},
            {"00F2", "Pushpaka"},
            {"00F3", "Edgar's Earrings"},
            {"00F4", "Cross Choker"},
            {"00F5", "Ghost Hound"},
            {"00F6", "Beaded Anklet"},
            {"00F7", "Dragonhead"},
            {"00F8", "Faufnir's Tear"},
            {"00F9", "Agales's Chain"},
            {"00FA", "Balam Ring"},
            {"00FB", "Nimje Coif"},
            {"00FC", "Morgan's Nails"},
            {"00FD", "Marlene's Ring"},
        };

        public static Dictionary<string, string> Gems = new Dictionary<string, string>
        {
            {"0000", "None"},
            {"0105", "Talos Feldspar"},
            {"0106", "Titan Malachite"},
            {"0107", "Sylphid Topaz"},
            {"0108", "Djinn Amber"},
            {"0109", "Salamander Ruby"},
            {"010A", "Ifrit Carnelian"},
            {"010B", "Gnome Emerald"},
            {"010C", "Dao Moonstone"},
            {"010D", "Undine Jasper"},
            {"010E", "Marid Aquamarine"},
            {"010F", "Angel Pearl"},
            {"0110", "Seraphim Diamond"},
            {"0111", "Morlock Jet"},
            {"0112", "Berial Blackpearl"},
            {"0113", "Haeralis"},
            {"0114", "Orlandu"},
            {"0115", "Orion"},
            {"0116", "Ogmius"},
            {"0117", "Iocus"},
            {"0118", "Balvus"},
            {"0119", "Trinity"},
            {"011A", "Beowulf"},
            {"011B", "Dragonite"},
            {"011C", "Sigguld"},
            {"011D", "Demonia"},
            {"011E", "Altema"},
            {"011F", "Polaris"},
            {"0120", "Basivalen"},
            {"0121", "Galerian"},
            {"0122", "Vedivier"},
            {"0123", "Berion"},
            {"0124", "Gervin"},
            {"0125", "Tertia"},
            {"0126", "Lancer"},
            {"0127", "Arturos"},
            {"0128", "Braveheart"},
            {"0129", "Hellraiser"},
            {"012A", "Nightkiller"},
            {"012B", "Manabreaker"},
            {"012C", "Powerfist"},
            {"012D", "Brainshield"},
            {"012E", "Speedster"},
            //{"012F", "untitled"},
            {"0130", "Silent Queen"},
            {"0131", "Dark Queen"},
            {"0132", "Death Queen"},
            {"0133", "White Queen"},
            //{"0134", "untitled"},
            //{"0135", "untitled"},
            //{"0136", "untitled"},
            //{"0137", "untitled"},
            //{"0138", "untitled"},
            //{"0139", "untitled"},
            //{"013A", "untitled"},
            //{"013B", "untitled"},
            //{"013C", "untitled"},
            //{"013D", "untitled"},
            //{"013E", "untitled"},
            //{"013F", "untitled"},
            //{"0140", "untitled"},
            //{"0141", "untitled"},
            //{"0142", "untitled"},
        };

        public static Dictionary<string, string> CharSet = new Dictionary<string, string>
        {
            {"EB", ""},
            {"E7", ""},
            {"E8", "\r"},
            {"F801", "«p1»"},
            {"F802", "«p2»"},
            {"F808", "«p8»"},
            {"F80A", "«p10»"},
            {"FA06", " "},
            {"8F", " "},
            {"00", "0"},
            {"01", "1"},
            {"02", "2"},
            {"03", "3"},
            {"04", "4"},
            {"05", "5"},
            {"06", "6"},
            {"07", "7"},
            {"08", "8"},
            {"09", "9"},
            {"0A", "A"},
            {"0B", "B"},
            {"0C", "C"},
            {"0D", "D"},
            {"0E", "E"},
            {"0F", "F"},
            {"10", "G"},
            {"11", "H"},
            {"12", "I"},
            {"13", "J"},
            {"14", "K"},
            {"15", "L"},
            {"16", "M"},
            {"17", "N"},
            {"18", "O"},
            {"19", "P"},
            {"1A", "Q"},
            {"1B", "R"},
            {"1C", "S"},
            {"1D", "T"},
            {"1E", "U"},
            {"1F", "V"},
            {"20", "W"},
            {"21", "X"},
            {"22", "Y"},
            {"23", "Z"},
            {"24", "a"},
            {"25", "b"},
            {"26", "c"},
            {"27", "d"},
            {"28", "e"},
            {"29", "f"},
            {"2A", "g"},
            {"2B", "h"},
            {"2C", "i"},
            {"2D", "j"},
            {"2E", "k"},
            {"2F", "l"},
            {"30", "m"},
            {"31", "n"},
            {"32", "o"},
            {"33", "p"},
            {"34", "q"},
            {"35", "r"},
            {"36", "s"},
            {"37", "t"},
            {"38", "u"},
            {"39", "v"},
            {"3A", "w"},
            {"3B", "x"},
            {"3C", "y"},
            {"3D", "z"},
            {"40", "�?"},
            {"41", "Â"},
            {"42", "Ä"},
            {"43", "Ç"},
            {"44", "È"},
            {"45", "É"},
            {"46", "Ê"},
            {"47", "Ë"},
            {"48", "Ì"},
            {"49", "�?"},
            {"4A", "Î"},
            {"4B", "�?"},
            {"4C", "Ò"},
            {"4D", "Ó"},
            {"4E", "Ô"},
            {"4F", "Ö"},
            {"50", "Ù"},
            {"51", "Ú"},
            {"52", "Û"},
            {"53", "Ü"},
            {"54", "ß"},
            {"55", "æ"},
            {"56", "à"},
            {"57", "á"},
            {"58", "â"},
            {"59", "ä"},
            {"5A", "ç"},
            {"5B", "è"},
            {"5C", "é"},
            {"5D", "ê"},
            {"5E", "ë"},
            {"5F", "ì"},
            {"60", "í"},
            {"61", "î"},
            {"62", "ï"},
            {"63", "ò"},
            {"64", "ó"},
            {"65", "ô"},
            {"66", "ö"},
            {"67", "ù"},
            {"68", "ú"},
            {"69", "û"},
            {"6A", "ü"},
            {"90", "!"},
            {"91", "\""},
            {"94", "%"},
            {"96", "'"},
            {"97", "("},
            {"98", ")"},
            {"9B", "["},
            {"9C", "]"},
            {"9D", ";"},
            {"9E", ":"},
            {"9F", ","},
            {"A0", "."},
            {"A1", "/"},
            {"A2", "\\"},
            {"A3", "<"},
            {"A4", ">"},
            {"A5", "?"},
            {"A7", "-"},
            {"A8", "+"},
            {"B6", "Lv."}
        };

        public static string TranslateVSText(string vsInput)
        {
            int i = 0;
            bool done = false;
            string englishOutput = String.Empty;
            string curByte, nextByte;
            while (!done)
            {
                curByte = String.Format("{0}{1}", vsInput[i], vsInput[i + 1]);

                if (curByte == "E7" || curByte == "00")
                {
                    done = true;
                    break;
                }
                else
                {
                    /* Two byte character */
                    if (curByte[0] == 'F')
                    {
                        nextByte = String.Format("{0}{1}", vsInput[i + 5], vsInput[i + 6]);
                        englishOutput += GameData.CharSet[curByte + nextByte];
                        i += 10;
                    }
                    /* One byte character */
                    else
                    {
                        englishOutput += GameData.CharSet[curByte];
                        i += 5;
                    }
                }
            }

            return englishOutput;
        }

    }
}
