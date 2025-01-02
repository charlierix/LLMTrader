using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LLMTrader_WPF
{
    /// <summary>
    /// Interaction logic for InventoryTestWindow.xaml
    /// </summary>
    public partial class InventoryTestWindow : Window
    {
        public InventoryTestWindow()
        {
            InitializeComponent();

            Background = SystemColors.ControlBrush;
        }

        private void PickLoot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Loot[] items =
                [
                    new Loot
                    {
                        Name = "Steel Sword",
                        Description = "A sturdy steel sword.",
                        UsageHistory =
                        [
                            "Used to defend the kingdom during the war of 2034.",
                            "Inherited from a knight named Sir Cedric."
                        ],
                        RelativeScalePercent = 1.0,
                        InitialQuality = ItemQuality.Good,
                        ConditionPercent = 85,
                        MainColor = "Gray",
                        AccentColor = "Silver",
                        Tags = [ "Weapon", "Melee" ]
                    },

                    new Loot
                    {
                        Name = "Golden Apple",
                        Description = "A rare and delicious golden apple.",
                        UsageHistory =
                        [
                            "Grown in the enchanted orchard of Eldoria.",
                            "Brought back by an adventurer from a distant land."
                        ],
                        RelativeScalePercent = 1.2,
                        InitialQuality = ItemQuality.Excellent,
                        ConditionPercent = 90,
                        MainColor = "Golden",
                        AccentColor = "",
                        Tags = [ "Food", "Magical" ]
                    },

                    new Loot
                    {
                        Name = "Healing Potion",
                        Description = "A vial of glowing green potion that heals minor wounds.",
                        UsageHistory =
                        [
                            "Crafted by the alchemist Elara in the village apothecary."
                        ],
                        RelativeScalePercent = 0.95,
                        InitialQuality = ItemQuality.Good,
                        ConditionPercent = 95,
                        MainColor = "Green",
                        AccentColor = "",
                        Tags = [ "Potion", "Healing" ]
                    },

                    new Loot
                    {
                        Name = "Leather Armor",
                        Description = "A set of light armor made from tough leather.",
                        UsageHistory =
                        [
                            "Worn by a group of scouts during the forest patrol."
                        ],
                        RelativeScalePercent = 1.5,
                        InitialQuality = ItemQuality.Fair,
                        ConditionPercent = 70,
                        MainColor = "Brown",
                        AccentColor = "",
                        Tags = [ "Armor", "Leather" ]
                    },

                    new Loot
                    {
                        Name = "Ring of Strength",
                        Description = "A golden ring that enhances the wearer's strength.",
                        UsageHistory =
                        [
                            "Forged by the legendary blacksmith Gromm in the Iron Forge."
                        ],
                        RelativeScalePercent = 1,
                        InitialQuality = ItemQuality.Legendary,
                        ConditionPercent = 98,
                        MainColor = "Golden",
                        AccentColor = "",
                        Tags = [ "Ring", "Magic" ]
                    },

                    new Loot
                    {
                        Name = "Scroll of Fireball",
                        Description = "A magical scroll that unleashes a fireball spell.",
                        UsageHistory =
                        [
                            "Written by the mage Thalor in the Arcane Library."
                        ],
                        RelativeScalePercent = 1.1,
                        InitialQuality = ItemQuality.Excellent,
                        ConditionPercent = 85,
                        MainColor = "Red",
                        AccentColor = "",
                        Tags = [ "Scroll", "Magic" ]
                    },

                    new Loot
                    {
                        Name = "Iron Shield",
                        Description = "A heavy iron shield with a wooden core.",
                        UsageHistory =
                        [
                            "Used by a veteran soldier in the Battle of Stormpeak."
                        ],
                        RelativeScalePercent = 1.2,
                        InitialQuality = ItemQuality.Good,
                        ConditionPercent = 80,
                        MainColor = "Gray",
                        AccentColor = "",
                        Tags = [ "Shield", "Defense" ]
                    },

                    new Loot
                    {
                        Name = "Wooden Arrow",
                        Description = "A simple arrow made from sturdy wood.",
                        UsageHistory =
                        [
                            "Crafted by an archer in the village."
                        ],
                        RelativeScalePercent = 0.8,
                        InitialQuality = ItemQuality.Fair,
                        ConditionPercent = 90,
                        MainColor = "Brown",
                        AccentColor = "",
                        Tags = [ "Arrow", "Projectile" ]
                    },

                    new Loot
                    {
                        Name = "Copper Coin",
                        Description = "A common copper coin used in trade.",
                        UsageHistory =
                        [
                            "Minted in the kingdom's treasury."
                        ],
                        RelativeScalePercent = 1,
                        InitialQuality = ItemQuality.Fair,
                        ConditionPercent = 95,
                        MainColor = "Copper",
                        AccentColor = "",
                        Tags = [ "Coin", "Currency" ]
                    },

                    new Loot
                    {
                        Name = "Elven Dagger",
                        Description = "A sleek dagger crafted by elven artisans.",
                        UsageHistory =
                        [
                            "Carried by an elf scout on a mission to the Dark Forest."
                        ],
                        RelativeScalePercent = 0.65,
                        InitialQuality = ItemQuality.Good,
                        ConditionPercent = 85,
                        MainColor = "Silver",
                        AccentColor = "",
                        Tags = [ "Dagger", "Melee" ]
                    },

                    new Loot
                    {
                        Name = "Wool Cloak",
                        Description = "A warm cloak made from fine wool.",
                        UsageHistory =
                        [
                            "Given as a gift to a traveler by the village elder."
                        ],
                        RelativeScalePercent = 1.0,
                        InitialQuality = ItemQuality.Fair,
                        ConditionPercent = 75,
                        MainColor = "Gray",
                        AccentColor = "",
                        Tags = [ "Cloak", "Warmth" ]
                    },

                    new Loot
                    {
                        Name = "Potion of Invisibility",
                        Description = "A dark blue potion that makes the drinker invisible.",
                        UsageHistory =
                        [
                            "Brewed by the reclusive alchemist in the mountains."
                        ],
                        RelativeScalePercent = 0.25,
                        InitialQuality = ItemQuality.Excellent,
                        ConditionPercent = 90,
                        MainColor = "Blue",
                        AccentColor = "",
                        Tags = [ "Potion", "Magic" ]
                    },
                ];





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
