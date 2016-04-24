 
 

// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
using System.Drawing;
using System.IO;
using System;
namespace DominationsBot{


	public static class BotResources
	{
		public static string ThisPath = Path.Combine(Settings.BasePath, @"BotResources");
			
		public static string PathBattleButton => Path.Combine( ThisPath, @"BattleButton.png");
		private static readonly Lazy<Bitmap> BmpBattleButtonLazy = new Lazy<Bitmap>(()=> new Bitmap(PathBattleButton));
		public static Bitmap BmpBattleButton => BmpBattleButtonLazy.Value;
	 
				
		public static string PathBear => Path.Combine( ThisPath, @"Bear.PNG");
		private static readonly Lazy<Bitmap> BmpBearLazy = new Lazy<Bitmap>(()=> new Bitmap(PathBear));
		public static Bitmap BmpBear => BmpBearLazy.Value;
	 
				
		public static string PathCoin => Path.Combine( ThisPath, @"Coin.PNG");
		private static readonly Lazy<Bitmap> BmpCoinLazy = new Lazy<Bitmap>(()=> new Bitmap(PathCoin));
		public static Bitmap BmpCoin => BmpCoinLazy.Value;
	 
				
		public static string PathDeer => Path.Combine( ThisPath, @"Deer.PNG");
		private static readonly Lazy<Bitmap> BmpDeerLazy = new Lazy<Bitmap>(()=> new Bitmap(PathDeer));
		public static Bitmap BmpDeer => BmpDeerLazy.Value;
	 
				
		public static string PathFood => Path.Combine( ThisPath, @"Food.PNG");
		private static readonly Lazy<Bitmap> BmpFoodLazy = new Lazy<Bitmap>(()=> new Bitmap(PathFood));
		public static Bitmap BmpFood => BmpFoodLazy.Value;
	 
				
		public static string PathFox => Path.Combine( ThisPath, @"Fox.PNG");
		private static readonly Lazy<Bitmap> BmpFoxLazy = new Lazy<Bitmap>(()=> new Bitmap(PathFox));
		public static Bitmap BmpFox => BmpFoxLazy.Value;
	 
				
		public static string PathFruitTree => Path.Combine( ThisPath, @"FruitTree.PNG");
		private static readonly Lazy<Bitmap> BmpFruitTreeLazy = new Lazy<Bitmap>(()=> new Bitmap(PathFruitTree));
		public static Bitmap BmpFruitTree => BmpFruitTreeLazy.Value;
	 
				
		public static string PathGoldMine => Path.Combine( ThisPath, @"GoldMine.PNG");
		private static readonly Lazy<Bitmap> BmpGoldMineLazy = new Lazy<Bitmap>(()=> new Bitmap(PathGoldMine));
		public static Bitmap BmpGoldMine => BmpGoldMineLazy.Value;
	 
				
		public static string PathRabbit => Path.Combine( ThisPath, @"Rabbit.PNG");
		private static readonly Lazy<Bitmap> BmpRabbitLazy = new Lazy<Bitmap>(()=> new Bitmap(PathRabbit));
		public static Bitmap BmpRabbit => BmpRabbitLazy.Value;
	 
				
		public static string PathSleepDialog => Path.Combine( ThisPath, @"SleepDialog.png");
		private static readonly Lazy<Bitmap> BmpSleepDialogLazy = new Lazy<Bitmap>(()=> new Bitmap(PathSleepDialog));
		public static Bitmap BmpSleepDialog => BmpSleepDialogLazy.Value;
	 
				
		public static string PathStoreButton => Path.Combine( ThisPath, @"StoreButton.png");
		private static readonly Lazy<Bitmap> BmpStoreButtonLazy = new Lazy<Bitmap>(()=> new Bitmap(PathStoreButton));
		public static Bitmap BmpStoreButton => BmpStoreButtonLazy.Value;
	 
				
		public static string PathStrenghtGladator => Path.Combine( ThisPath, @"StrenghtGladator.PNG");
		private static readonly Lazy<Bitmap> BmpStrenghtGladatorLazy = new Lazy<Bitmap>(()=> new Bitmap(PathStrenghtGladator));
		public static Bitmap BmpStrenghtGladator => BmpStrenghtGladatorLazy.Value;
	 
			

	public static class Symbols
	{
		public static string ThisPath = Path.Combine(BotResources.ThisPath, @"Symbols");
		

	public static class Ages
	{
		public static string ThisPath = Path.Combine(Symbols.ThisPath, @"Ages");
			
		public static string PathBronze => Path.Combine( ThisPath, @"Bronze.png");
		private static readonly Lazy<Bitmap> BmpBronzeLazy = new Lazy<Bitmap>(()=> new Bitmap(PathBronze));
		public static Bitmap BmpBronze => BmpBronzeLazy.Value;
	 
				
		public static string PathClassical => Path.Combine( ThisPath, @"Classical.png");
		private static readonly Lazy<Bitmap> BmpClassicalLazy = new Lazy<Bitmap>(()=> new Bitmap(PathClassical));
		public static Bitmap BmpClassical => BmpClassicalLazy.Value;
	 
				
		public static string PathIron => Path.Combine( ThisPath, @"Iron.png");
		private static readonly Lazy<Bitmap> BmpIronLazy = new Lazy<Bitmap>(()=> new Bitmap(PathIron));
		public static Bitmap BmpIron => BmpIronLazy.Value;
	 
				}
		

	public static class Barracks
	{
		public static string ThisPath = Path.Combine(Symbols.ThisPath, @"Barracks");
			
		public static string Path1 => Path.Combine( ThisPath, @"1.png");
		private static readonly Lazy<Bitmap> Bmp1Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path1));
		public static Bitmap Bmp1 => Bmp1Lazy.Value;
	 
				}
		

	public static class Battle
	{
		public static string ThisPath = Path.Combine(Symbols.ThisPath, @"Battle");
			
		public static string PathCantDeployTroopsThere => Path.Combine( ThisPath, @"CantDeployTroopsThere.png");
		private static readonly Lazy<Bitmap> BmpCantDeployTroopsThereLazy = new Lazy<Bitmap>(()=> new Bitmap(PathCantDeployTroopsThere));
		public static Bitmap BmpCantDeployTroopsThere => BmpCantDeployTroopsThereLazy.Value;
	 
				
		public static string PathEndBattle => Path.Combine( ThisPath, @"EndBattle.png");
		private static readonly Lazy<Bitmap> BmpEndBattleLazy = new Lazy<Bitmap>(()=> new Bitmap(PathEndBattle));
		public static Bitmap BmpEndBattle => BmpEndBattleLazy.Value;
	 
				}
		

	public static class Citizens
	{
		public static string ThisPath = Path.Combine(Symbols.ThisPath, @"Citizens");
			
		public static string Path1 => Path.Combine( ThisPath, @"1.png");
		private static readonly Lazy<Bitmap> Bmp1Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path1));
		public static Bitmap Bmp1 => Bmp1Lazy.Value;
	 
				
		public static string Path2 => Path.Combine( ThisPath, @"2.png");
		private static readonly Lazy<Bitmap> Bmp2Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path2));
		public static Bitmap Bmp2 => Bmp2Lazy.Value;
	 
				
		public static string Path4 => Path.Combine( ThisPath, @"4.png");
		private static readonly Lazy<Bitmap> Bmp4Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path4));
		public static Bitmap Bmp4 => Bmp4Lazy.Value;
	 
				
		public static string Pathslesh => Path.Combine( ThisPath, @"slesh.png");
		private static readonly Lazy<Bitmap> BmpsleshLazy = new Lazy<Bitmap>(()=> new Bitmap(Pathslesh));
		public static Bitmap Bmpslesh => BmpsleshLazy.Value;
	 
				}
		

	public static class Food
	{
		public static string ThisPath = Path.Combine(Symbols.ThisPath, @"Food");
			
		public static string Path1 => Path.Combine( ThisPath, @"1.png");
		private static readonly Lazy<Bitmap> Bmp1Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path1));
		public static Bitmap Bmp1 => Bmp1Lazy.Value;
	 
				}
		

	public static class FoodAndGold
	{
		public static string ThisPath = Path.Combine(Symbols.ThisPath, @"FoodAndGold");
			
		public static string Path0 => Path.Combine( ThisPath, @"0.png");
		private static readonly Lazy<Bitmap> Bmp0Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path0));
		public static Bitmap Bmp0 => Bmp0Lazy.Value;
	 
				
		public static string Path1 => Path.Combine( ThisPath, @"1.png");
		private static readonly Lazy<Bitmap> Bmp1Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path1));
		public static Bitmap Bmp1 => Bmp1Lazy.Value;
	 
				
		public static string Path2 => Path.Combine( ThisPath, @"2.png");
		private static readonly Lazy<Bitmap> Bmp2Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path2));
		public static Bitmap Bmp2 => Bmp2Lazy.Value;
	 
				
		public static string Path3 => Path.Combine( ThisPath, @"3.png");
		private static readonly Lazy<Bitmap> Bmp3Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path3));
		public static Bitmap Bmp3 => Bmp3Lazy.Value;
	 
				
		public static string Path4 => Path.Combine( ThisPath, @"4.png");
		private static readonly Lazy<Bitmap> Bmp4Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path4));
		public static Bitmap Bmp4 => Bmp4Lazy.Value;
	 
				
		public static string Path5 => Path.Combine( ThisPath, @"5.png");
		private static readonly Lazy<Bitmap> Bmp5Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path5));
		public static Bitmap Bmp5 => Bmp5Lazy.Value;
	 
				
		public static string Path6 => Path.Combine( ThisPath, @"6.png");
		private static readonly Lazy<Bitmap> Bmp6Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path6));
		public static Bitmap Bmp6 => Bmp6Lazy.Value;
	 
				
		public static string Path7 => Path.Combine( ThisPath, @"7.png");
		private static readonly Lazy<Bitmap> Bmp7Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path7));
		public static Bitmap Bmp7 => Bmp7Lazy.Value;
	 
				
		public static string Path8 => Path.Combine( ThisPath, @"8.png");
		private static readonly Lazy<Bitmap> Bmp8Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path8));
		public static Bitmap Bmp8 => Bmp8Lazy.Value;
	 
				
		public static string Path9 => Path.Combine( ThisPath, @"9.png");
		private static readonly Lazy<Bitmap> Bmp9Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path9));
		public static Bitmap Bmp9 => Bmp9Lazy.Value;
	 
				
		public static string Path983136 => Path.Combine( ThisPath, @"983136.png");
		private static readonly Lazy<Bitmap> Bmp983136Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path983136));
		public static Bitmap Bmp983136 => Bmp983136Lazy.Value;
	 
				}
		

	public static class Gold
	{
		public static string ThisPath = Path.Combine(Symbols.ThisPath, @"Gold");
			
		public static string Path1 => Path.Combine( ThisPath, @"1.png");
		private static readonly Lazy<Bitmap> Bmp1Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path1));
		public static Bitmap Bmp1 => Bmp1Lazy.Value;
	 
				}
		

	public static class Level
	{
		public static string ThisPath = Path.Combine(Symbols.ThisPath, @"Level");
			
		public static string Path0 => Path.Combine( ThisPath, @"0.png");
		private static readonly Lazy<Bitmap> Bmp0Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path0));
		public static Bitmap Bmp0 => Bmp0Lazy.Value;
	 
				
		public static string Path1 => Path.Combine( ThisPath, @"1.png");
		private static readonly Lazy<Bitmap> Bmp1Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path1));
		public static Bitmap Bmp1 => Bmp1Lazy.Value;
	 
				
		public static string Path2 => Path.Combine( ThisPath, @"2.png");
		private static readonly Lazy<Bitmap> Bmp2Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path2));
		public static Bitmap Bmp2 => Bmp2Lazy.Value;
	 
				
		public static string Path3 => Path.Combine( ThisPath, @"3.png");
		private static readonly Lazy<Bitmap> Bmp3Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path3));
		public static Bitmap Bmp3 => Bmp3Lazy.Value;
	 
				
		public static string Path4 => Path.Combine( ThisPath, @"4.png");
		private static readonly Lazy<Bitmap> Bmp4Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path4));
		public static Bitmap Bmp4 => Bmp4Lazy.Value;
	 
				
		public static string Path5 => Path.Combine( ThisPath, @"5.png");
		private static readonly Lazy<Bitmap> Bmp5Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path5));
		public static Bitmap Bmp5 => Bmp5Lazy.Value;
	 
				
		public static string Path6 => Path.Combine( ThisPath, @"6.png");
		private static readonly Lazy<Bitmap> Bmp6Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path6));
		public static Bitmap Bmp6 => Bmp6Lazy.Value;
	 
				
		public static string Path7 => Path.Combine( ThisPath, @"7.png");
		private static readonly Lazy<Bitmap> Bmp7Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path7));
		public static Bitmap Bmp7 => Bmp7Lazy.Value;
	 
				
		public static string Path8 => Path.Combine( ThisPath, @"8.png");
		private static readonly Lazy<Bitmap> Bmp8Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path8));
		public static Bitmap Bmp8 => Bmp8Lazy.Value;
	 
				
		public static string Path9 => Path.Combine( ThisPath, @"9.png");
		private static readonly Lazy<Bitmap> Bmp9Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path9));
		public static Bitmap Bmp9 => Bmp9Lazy.Value;
	 
				}
		

	public static class Zoom
	{
		public static string ThisPath = Path.Combine(Symbols.ThisPath, @"Zoom");
			
		public static string Path1 => Path.Combine( ThisPath, @"1.png");
		private static readonly Lazy<Bitmap> Bmp1Lazy = new Lazy<Bitmap>(()=> new Bitmap(Path1));
		public static Bitmap Bmp1 => Bmp1Lazy.Value;
	 
				}
			}
			}
		}
