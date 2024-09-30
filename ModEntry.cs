using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI.Events;
using StardewModdingAPI;
using StardewValley;
using Microsoft.Xna.Framework.Graphics;
using System.Security;
using StardewValley.Objects;
using StardewValley.Extensions;
using StardewValley.Quests;
using StardewValley.Buildings;
using Microsoft.Xna.Framework.Input;
using StardewValley.ItemTypeDefinitions;
using StardewValley.GameData;
using StardewValley.Monsters;
using StardewValley.Locations;
using xTile;
using Microsoft.Xna.Framework;
using StardewValley.GameData.Bundles;
using System;

namespace LivsRemasteredBundles
{
    internal class ModEntry : Mod
    {
        // remember to readjust all the bundle sprites once this is deemed complete
        // add remixed
        // add insanity in 2.0
        private BundleData bundles = new BundleData();
        private CurrentUser currentUser = new CurrentUser();
        private bool firstPass = true;

        private Dictionary<string, string> remasteredBundles = new Dictionary<string, string>()
        {
            {"Spring Crops","24 597 Carrot 190 433 248 188 250 192 252 400 591 271" },
            {"Summer Crops","258 270 304 260 254 376 264 266 268 593 SummerSquash 421 256 262" },
            {"Fall Crops", "300 274 284 278 Broccoli 282 272 595 398 276 280" },
            {"Quality Crops", "24 597 Carrot 190 433 248 188 250 192 252 400 591 271 258 270 304 260 254 376 264 266 268 593 SummerSquash 421 256 262 300 274 284 278 Broccoli 282 272 595 398 276 280 Powdermelon" },
            {"Animal", "107 174 176 180 182 305 440 184 186 436 438 430" },
            {"Artisan", "340 348 303 346 459 424 426 419 395 614 350 428 306 307 308 807 247 432 342 344 445 447 DriedFruit DriedMushrooms Raisins SmokedFish" },
            {"Spring Foraging", "404 18 20 22 16 257 296 399" },
            {"Summer Foraging", "404 259 398 396 402 420" },
            {"Fall Foraging", "422 420 410 281 404 408 406" },
            {"Winter Foraging", "412 414 416 418 283" },
            {"Construction", "388 390 709 771 330 80" },
            {"Exotic Foraging", "422 78 88 90 394 397 393 392 259" },
            {"River Fish", "132 137 138 139 140 141 143 144 145 699 704 706 707" },
            {"Lake Fish", "136 138 140 142 698 700 702 707" },
            {"Ocean Fish", "128 129 130 131 146 147 148 149 150 151 154 155 267 701 705 708 798 799 800" },
            {"Night Fishing", "132 148 151 155 269 798 799 800" },
            {"Specialty Fish", "156 158 161 162 164 165 734 Goby" },
            {"Crab Pot", "372 715 716 717 718 719 720 721 722 723" },
            {"Blacksmith's", "334 335 336 337 338" },
            {"Geologist's", "80 82 84 86 60 62 64 66 68 70 72" },
            {"Adventurer's", "766 767 768 769 684 881 814" },
            {"2,500g","" },
            {"5,000g","" },
            {"10,000g","" },
            {"25,000g","" },
            {"Chef's","724 259 430 376 228 194 201 227 234 212 342 271 306 424" },
            {"Field Research", "422 392 702 536 84 254 426 787 698 709" },
            {"Enchanter's","725 724 726 348 350 446 637 638 337 428 308" },
            {"Dye","420 266 397 421 444 62 400 64 815 128 597 414" },
            {"Fodder","262 178 613 Carrot 174 281 300 260" },
            {"The Missing","348 807 74 454 795 445 836 149 417 910 527 (W)29 834 91" }
        };

        private List<BundleStringData> bundleStringData = new List<BundleStringData>()
        {
            new BundleStringData("Spring Crops",4,4,"O 465 20","0"),
            new BundleStringData("Summer Crops",4,4,"O 621 1","3"),
            new BundleStringData("Fall Crops",4,4,"BO 10 1","2"),
            new BundleStringData("Quality Crops",4,3,"BO 15 1","6"),
            new BundleStringData("Animal",6,5,"BO 16 1","4"),
            new BundleStringData("Artisan",12,6,"BO 12 1","1"),
            new BundleStringData("Spring Foraging",4,4,"O 495 30","0"),
            new BundleStringData("Summer Foraging",3,3,"O 496 30","3"),
            new BundleStringData("Fall Foraging",4,4,"O 497 30","2"),
            new BundleStringData("Winter Foraging",4,4,"O 498 30","6"),
            new BundleStringData("Construction",4,4,"BO 114 1","4"),
            new BundleStringData("Exotic Foraging",9,5,"O 235 5","1"),
            new BundleStringData("River Fish",4,4,"O DeluxeBait 30","6"),
            new BundleStringData("Lake Fish",4,4,"O 687 1","0"),
            new BundleStringData("Ocean Fish",4,4,"O 690 5","5"),
            new BundleStringData("Night Fishing",3,3,"R 517 1","1"),
            new BundleStringData("Specialty Fish",4,4,"O 242 5","4"),
            new BundleStringData("Crab Pot",10,5,"O 710 3","1"),
            new BundleStringData("Blacksmith's",3,3,"BO 13 1","2"),
            new BundleStringData("Geologist's",4,4,"O 749 5","1"),
            new BundleStringData("Adventurer's",4,2,"R 518 1","1"),
            new BundleStringData("2,500g",0,0,"O 220 3","4"),
            new BundleStringData("5,000g",0,0,"O 369 30","2"),
            new BundleStringData("10,000g",0,0,"BO 9 1","3"),
            new BundleStringData("25,000g",0,0,"BO 21 1","1"),
            new BundleStringData("Chef's",6,6,"O 221 3","4"),
            new BundleStringData("Field Research",4,4,"BO 20 1","5"),
            new BundleStringData("Enchanter's",4,4,"O 336 5","1"),
            new BundleStringData("Dye",6,6,"BO 25 1","6"),
            new BundleStringData("Fodder",3,3,"BO 104 1","3"),
            new BundleStringData("The Missing",6,5,"","1"),
        };

        public override void Entry(IModHelper helper)
        {
            helper.Events.Content.AssetRequested += this.OnAssetRequested;
        }

        private void OnAssetRequested(object? send, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("Data/Bundles"))
            {
                if (firstPass)
                {
                    currentUser = this.Helper.Data.ReadJsonFile<CurrentUser>("CurrentUser.json") ?? new CurrentUser();
                    if(currentUser.farmerName != StardewValley.Game1.player.Name || currentUser.farmName != StardewValley.Game1.player.farmName.Value)
                    {
                        initUser();
                    }
                    else
                    {
                        bundles = Helper.Data.ReadJsonFile<BundleData>("BundleData.json") ?? new BundleData();
                    }
                    firstPass = false;
                    testBundles();
                }
                e.Edit(asset =>
                {
                    var editor = asset.AsDictionary<string, string>();
                    var ii = 0;
                    while (ii < bundles.names.Count)
                    {
                        editor.Data[bundles.locations[ii]] = bundles.bundleStrings[ii];
                        ii++;
                    }
                });
            }
        }

        private void initUser()
        {
            this.currentUser.farmName = StardewValley.Game1.player.Name;
            this.currentUser.farmerName = StardewValley.Game1.player.farmName.Value;

            var type = StardewValley.Game1.bundleType.ToString();

            if (type == "Remixed" && this.currentUser.bundleType == "Remastered") this.currentUser.bundleType = "Remixed Remastered";
            else if (type == "Remixed" && this.currentUser.bundleType == "Ultra Remastered") this.currentUser.bundleType = "Ultra Remixed Remastered";

            Helper.Data.WriteJsonFile<CurrentUser>("CurrentUser.json", this.currentUser);
            initBundles();
            Helper.Data.WriteJsonFile<BundleData>("BundleData.json", this.bundles);

            StardewValley.Game1.chatBox.addMessage("[LRB]: Welcome to Liv's Remastered Bundles! You currently have your Bundle type set to " + this.currentUser.bundleType + ". If you wish to view the bundle types and how to change them, consult the README.", new Microsoft.Xna.Framework.Color(255, 255, 255));
        }

        private void initBundles()
        {

            var bb = 0;

            foreach (var val in StardewValley.Game1.netWorldState.Value.BundleData)
            {
                bundles.locations.Add(val.Key);

                var tempBundleString = val.Value.Split("/");
                bundles.names.Add(tempBundleString[0]);

                bb++;
            }

            if (currentUser.bundleType.Contains("Insanity"))
            {
                //custom

            }
            else
            {
                var ii = 0;
                List<string> keys = new List<string>(remasteredBundles.Keys);
                List<string> values = new List<string>(remasteredBundles.Values);
                string[] fishExceptions = { "Seaweed", "Green Algae", "White Algae", "Sea Jelly", "River Jelly", "Cave Jelly", "Lobster", "Clam", "Crab", "Cockle", "Mussel", "Shrimp", "Oyster", "Crayfish", "Snail", "Periwinkle" };

                while (ii < bundles.names.Count)
                {
                    var bundleString = "";
                    List<string> numItems = new List<string>();

                    int index = keys.IndexOf(bundles.names[ii]);
                    string[] tempItems = values[index].Split(" ");
                    string[] currentSelectRequirement = getBundleInfoByName(bundles.names[ii]);

                    bundleString = bundles.names[ii] + "/" + currentSelectRequirement[2] + "/";

                    Random rnd = new Random();
                    var jj = 0;
                    while(jj < Int32.Parse(currentSelectRequirement[0]))
                    {
                        var jjIndex = rnd.Next(0, tempItems.Length);
                        if (!numItems.Contains(tempItems[jjIndex]))
                        {
                            var quality = 0;
                            var amount = 1;
                            //add additional amount exceptions from remixed
                            if (currentUser.bundleType.Contains("Ultra"))
                            {
                                amount = 10;

                                int num = 0;
                                int num1 = 0;
                                int num2 = 0;
                                int num3 = 0;
                                //amount diffs greater than 10 (sap 500, fiber 200, roe 15, aged roe 15, salmonberry/blackberry 50, wild plum on foragers 15)
                                if ((Int32.TryParse(tempItems[jjIndex], out num) && num == 388) ||
                                    (Int32.TryParse(tempItems[jjIndex], out num1) && num1 == 390) ||
                                    (Int32.TryParse(tempItems[jjIndex], out num2) && num2 == 766))
                                {
                                    amount = 99;
                                }
                                StardewValley.ItemTypeDefinitions.ParsedItemData info = ItemRegistry.GetData(tempItems[jjIndex]);
                                if ((info.Category.ToString() == "-4" && !fishExceptions.Contains(info.DisplayName)) || info.Category.ToString() == "-79" ||
                                    info.Category.ToString() == "-81" || info.Category.ToString() == "-75" ||
                                    info.Category.ToString() == "-80" || info.Category.ToString() == "-5" || info.Category.ToString() == "-6" ||
                                    info.Category.ToString() == "-18" || (Int32.TryParse(tempItems[jjIndex], out num3) && num3 == 348))
                                {
                                    quality = 2;
                                }
                            }else if (bundles.names[ii] == "Quality Crops")
                            {
                                quality = 2;
                                amount = 5;
                            }else if(bundles.names[ii] == "Construction")
                            {
                                int num = 0;
                                int num1 = 0;
                                if ((Int32.TryParse(tempItems[jjIndex], out num) && num == 388) || (Int32.TryParse(tempItems[jjIndex], out num) && num1 == 390))
                                {
                                    amount = 99;
                                }
                                else
                                {
                                    amount = 10;
                                }
                            }else if (bundles.names[ii] == "Adventurer's")
                            {
                                int num = 0;
                                int num1 = 0;
                                if(Int32.TryParse(tempItems[jjIndex], out num) && num == 766)
                                {
                                    amount = 99;
                                }else if(Int32.TryParse(tempItems[jjIndex], out num1) && num1 == 767)
                                {
                                    amount = 10;
                                }
                            }else if (bundles.names[ii] == "Fodder")
                            {
                                int num = 0;
                                int num1 = 0;
                                if((Int32.TryParse(tempItems[jjIndex], out num) && num == 262) || (Int32.TryParse(tempItems[jjIndex], out num1) && num1 == 178))
                                {
                                    amount = 10;
                                }
                                else
                                {
                                    amount = 3;
                                }
                            }else if (bundles.names[ii] == "The Missing")
                            {
                                int[] goldItems = {348, 454, 795, 836, 149, 417, 834, 91 };
                                int[] multof5 = {454,834,91 };
                                int[] multof3 = { 836, 795 };

                                int num = 0;

                                if (Int32.TryParse(tempItems[jjIndex], out num) && goldItems.Contains(Int32.Parse(tempItems[jjIndex])))
                                {
                                    quality = 2;
                                }

                                if (Int32.TryParse(tempItems[jjIndex], out num) && multof5.Contains((Int32.Parse(tempItems[jjIndex])))){
                                    amount = 5;
                                }else if (Int32.TryParse(tempItems[jjIndex], out num) && multof3.Contains((Int32.Parse(tempItems[jjIndex]))))
                                {
                                    amount = 3;
                                }
                            }

                            if (jj + 1 == Int32.Parse(currentSelectRequirement[0])) bundleString += tempItems[jjIndex] + " " + amount + " " + quality + "/";
                            else bundleString += tempItems[jjIndex] + " " + amount + " " + quality + " ";

                            numItems.Add(tempItems[jjIndex]);
                            jj++;
                        }
                    }

                    if(Int32.Parse(currentSelectRequirement[0]) == 0)
                    {
                        var amount = bundles.names[ii].Replace("g","");
                        bundleString += "-1 " + amount + " " + amount + "/";
                    }

                    bundleString += currentSelectRequirement[3] + "/";
                    if (currentSelectRequirement[0] != currentSelectRequirement[1]) bundleString += currentSelectRequirement[1];
                    bundleString += "//" + bundles.names[ii];

                    bundles.bundleStrings.Add(bundleString);
                    ii++;
                }
            }
        }

        private string[] getBundleInfoByName(string bundleName)
        {
            var selection = "";
            var required = "";
            var reward = "";
            var location = "";

            var ii = 0;
            while(ii < bundleStringData.Count)
            {
                if(bundleStringData[ii].name == bundleName)
                {
                    selection = bundleStringData[ii].selection.ToString();
                    required = bundleStringData[ii].required.ToString();
                    reward = bundleStringData[ii].reward;
                    location = bundleStringData[ii].location;
                    break;
                }

                ii++;
            }

            string[] result = { selection, required, reward, location };

            return result;
        }

        public void testBundles()
        {
            //
        }

    }
}
