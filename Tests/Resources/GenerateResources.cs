 
 

// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
using System.Drawing;
using System.IO;
using System;
using DominationsBot;
using System.Collections.Generic;
namespace Tests
{


	public static class Resources
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {F4_12Bmp ,NormalScreenBmp ,NormalScreen2Bmp ,SleepScreenBmp ,SleepScreen2Bmp}; 
		public static string ThisPath = Path.Combine(Settings.BasePath, @"Resources");
			
		public static string F4_12Path => Path.Combine( ThisPath, @"4.12.png");
		private static readonly Lazy<Bitmap> F4_12BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F4_12Path));
		public static Bitmap F4_12Bmp => F4_12BmpLazy.Value;
	 
				
		public static string NormalScreenPath => Path.Combine( ThisPath, @"NormalScreen.png");
		private static readonly Lazy<Bitmap> NormalScreenBmpLazy = new Lazy<Bitmap>(()=> new Bitmap(NormalScreenPath));
		public static Bitmap NormalScreenBmp => NormalScreenBmpLazy.Value;
	 
				
		public static string NormalScreen2Path => Path.Combine( ThisPath, @"NormalScreen2.png");
		private static readonly Lazy<Bitmap> NormalScreen2BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(NormalScreen2Path));
		public static Bitmap NormalScreen2Bmp => NormalScreen2BmpLazy.Value;
	 
				
		public static string SleepScreenPath => Path.Combine( ThisPath, @"SleepScreen.png");
		private static readonly Lazy<Bitmap> SleepScreenBmpLazy = new Lazy<Bitmap>(()=> new Bitmap(SleepScreenPath));
		public static Bitmap SleepScreenBmp => SleepScreenBmpLazy.Value;
	 
				
		public static string SleepScreen2Path => Path.Combine( ThisPath, @"SleepScreen2.png");
		private static readonly Lazy<Bitmap> SleepScreen2BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(SleepScreen2Path));
		public static Bitmap SleepScreen2Bmp => SleepScreen2BmpLazy.Value;
	 
			

	public static class ImageComparers
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {Battle_1Bmp ,Battle_2Bmp ,Food_1Bmp ,Food_2Bmp ,screen_1Bmp ,screen_2Bmp ,simple_1Bmp ,simple_2Bmp ,Store_1Bmp ,Store_2Bmp}; 
		public static string ThisPath = Path.Combine(Resources.ThisPath, @"ImageComparers");
			
		public static string Battle_1Path => Path.Combine( ThisPath, @"Battle-1.png");
		private static readonly Lazy<Bitmap> Battle_1BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(Battle_1Path));
		public static Bitmap Battle_1Bmp => Battle_1BmpLazy.Value;
	 
				
		public static string Battle_2Path => Path.Combine( ThisPath, @"Battle-2.png");
		private static readonly Lazy<Bitmap> Battle_2BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(Battle_2Path));
		public static Bitmap Battle_2Bmp => Battle_2BmpLazy.Value;
	 
				
		public static string Food_1Path => Path.Combine( ThisPath, @"Food-1.png");
		private static readonly Lazy<Bitmap> Food_1BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(Food_1Path));
		public static Bitmap Food_1Bmp => Food_1BmpLazy.Value;
	 
				
		public static string Food_2Path => Path.Combine( ThisPath, @"Food-2.png");
		private static readonly Lazy<Bitmap> Food_2BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(Food_2Path));
		public static Bitmap Food_2Bmp => Food_2BmpLazy.Value;
	 
				
		public static string screen_1Path => Path.Combine( ThisPath, @"screen-1.png");
		private static readonly Lazy<Bitmap> screen_1BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(screen_1Path));
		public static Bitmap screen_1Bmp => screen_1BmpLazy.Value;
	 
				
		public static string screen_2Path => Path.Combine( ThisPath, @"screen-2.png");
		private static readonly Lazy<Bitmap> screen_2BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(screen_2Path));
		public static Bitmap screen_2Bmp => screen_2BmpLazy.Value;
	 
				
		public static string simple_1Path => Path.Combine( ThisPath, @"simple-1.png");
		private static readonly Lazy<Bitmap> simple_1BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(simple_1Path));
		public static Bitmap simple_1Bmp => simple_1BmpLazy.Value;
	 
				
		public static string simple_2Path => Path.Combine( ThisPath, @"simple-2.png");
		private static readonly Lazy<Bitmap> simple_2BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(simple_2Path));
		public static Bitmap simple_2Bmp => simple_2BmpLazy.Value;
	 
				
		public static string Store_1Path => Path.Combine( ThisPath, @"Store-1.png");
		private static readonly Lazy<Bitmap> Store_1BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(Store_1Path));
		public static Bitmap Store_1Bmp => Store_1BmpLazy.Value;
	 
				
		public static string Store_2Path => Path.Combine( ThisPath, @"Store-2.png");
		private static readonly Lazy<Bitmap> Store_2BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(Store_2Path));
		public static Bitmap Store_2Bmp => Store_2BmpLazy.Value;
	 
				}
		

	public static class NumbersRead
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {F132817Bmp ,F134407Bmp ,F135226Bmp ,F136916Bmp ,F171563Bmp ,F981112Bmp ,F981765Bmp ,F982447Bmp ,F983136Bmp ,F983835Bmp}; 
		public static string ThisPath = Path.Combine(Resources.ThisPath, @"NumbersRead");
			
		public static string F132817Path => Path.Combine( ThisPath, @"132817.png");
		private static readonly Lazy<Bitmap> F132817BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F132817Path));
		public static Bitmap F132817Bmp => F132817BmpLazy.Value;
	 
				
		public static string F134407Path => Path.Combine( ThisPath, @"134407.png");
		private static readonly Lazy<Bitmap> F134407BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F134407Path));
		public static Bitmap F134407Bmp => F134407BmpLazy.Value;
	 
				
		public static string F135226Path => Path.Combine( ThisPath, @"135226.png");
		private static readonly Lazy<Bitmap> F135226BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F135226Path));
		public static Bitmap F135226Bmp => F135226BmpLazy.Value;
	 
				
		public static string F136916Path => Path.Combine( ThisPath, @"136916.png");
		private static readonly Lazy<Bitmap> F136916BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F136916Path));
		public static Bitmap F136916Bmp => F136916BmpLazy.Value;
	 
				
		public static string F171563Path => Path.Combine( ThisPath, @"171563.png");
		private static readonly Lazy<Bitmap> F171563BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F171563Path));
		public static Bitmap F171563Bmp => F171563BmpLazy.Value;
	 
				
		public static string F981112Path => Path.Combine( ThisPath, @"981112.png");
		private static readonly Lazy<Bitmap> F981112BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F981112Path));
		public static Bitmap F981112Bmp => F981112BmpLazy.Value;
	 
				
		public static string F981765Path => Path.Combine( ThisPath, @"981765.png");
		private static readonly Lazy<Bitmap> F981765BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F981765Path));
		public static Bitmap F981765Bmp => F981765BmpLazy.Value;
	 
				
		public static string F982447Path => Path.Combine( ThisPath, @"982447.png");
		private static readonly Lazy<Bitmap> F982447BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F982447Path));
		public static Bitmap F982447Bmp => F982447BmpLazy.Value;
	 
				
		public static string F983136Path => Path.Combine( ThisPath, @"983136.png");
		private static readonly Lazy<Bitmap> F983136BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F983136Path));
		public static Bitmap F983136Bmp => F983136BmpLazy.Value;
	 
				
		public static string F983835Path => Path.Combine( ThisPath, @"983835.png");
		private static readonly Lazy<Bitmap> F983835BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F983835Path));
		public static Bitmap F983835Bmp => F983835BmpLazy.Value;
	 
			

	public static class Level
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {F16Bmp ,F17Bmp ,F20Bmp ,F4Bmp}; 
		public static string ThisPath = Path.Combine(NumbersRead.ThisPath, @"Level");
			
		public static string F16Path => Path.Combine( ThisPath, @"16.png");
		private static readonly Lazy<Bitmap> F16BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F16Path));
		public static Bitmap F16Bmp => F16BmpLazy.Value;
	 
				
		public static string F17Path => Path.Combine( ThisPath, @"17.png");
		private static readonly Lazy<Bitmap> F17BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F17Path));
		public static Bitmap F17Bmp => F17BmpLazy.Value;
	 
				
		public static string F20Path => Path.Combine( ThisPath, @"20.png");
		private static readonly Lazy<Bitmap> F20BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F20Path));
		public static Bitmap F20Bmp => F20BmpLazy.Value;
	 
				
		public static string F4Path => Path.Combine( ThisPath, @"4.png");
		private static readonly Lazy<Bitmap> F4BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F4Path));
		public static Bitmap F4Bmp => F4BmpLazy.Value;
	 
				}
			}
		

	public static class Screens
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {}; 
		public static string ThisPath = Path.Combine(Resources.ThisPath, @"Screens");
		

	public static class Battle
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {F1Bmp ,F2Bmp ,F3Bmp}; 
		public static string ThisPath = Path.Combine(Screens.ThisPath, @"Battle");
			
		public static string F1Path => Path.Combine( ThisPath, @"1.png");
		private static readonly Lazy<Bitmap> F1BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F1Path));
		public static Bitmap F1Bmp => F1BmpLazy.Value;
	 
				
		public static string F2Path => Path.Combine( ThisPath, @"2.png");
		private static readonly Lazy<Bitmap> F2BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F2Path));
		public static Bitmap F2Bmp => F2BmpLazy.Value;
	 
				
		public static string F3Path => Path.Combine( ThisPath, @"3.png");
		private static readonly Lazy<Bitmap> F3BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F3Path));
		public static Bitmap F3Bmp => F3BmpLazy.Value;
	 
			

	public static class AfterBattle
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {F1Bmp ,F2Bmp ,F3Bmp}; 
		public static string ThisPath = Path.Combine(Battle.ThisPath, @"AfterBattle");
			
		public static string F1Path => Path.Combine( ThisPath, @"1.png");
		private static readonly Lazy<Bitmap> F1BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F1Path));
		public static Bitmap F1Bmp => F1BmpLazy.Value;
	 
				
		public static string F2Path => Path.Combine( ThisPath, @"2.png");
		private static readonly Lazy<Bitmap> F2BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F2Path));
		public static Bitmap F2Bmp => F2BmpLazy.Value;
	 
				
		public static string F3Path => Path.Combine( ThisPath, @"3.png");
		private static readonly Lazy<Bitmap> F3BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(F3Path));
		public static Bitmap F3Bmp => F3BmpLazy.Value;
	 
				}
			}
		

	public static class Main
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {}; 
		public static string ThisPath = Path.Combine(Screens.ThisPath, @"Main");
			}
		

	public static class Other
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {PeaceTretyConfirmBmp ,PreBattleBmp ,SurrenderBmp}; 
		public static string ThisPath = Path.Combine(Screens.ThisPath, @"Other");
			
		public static string PeaceTretyConfirmPath => Path.Combine( ThisPath, @"PeaceTretyConfirm.png");
		private static readonly Lazy<Bitmap> PeaceTretyConfirmBmpLazy = new Lazy<Bitmap>(()=> new Bitmap(PeaceTretyConfirmPath));
		public static Bitmap PeaceTretyConfirmBmp => PeaceTretyConfirmBmpLazy.Value;
	 
				
		public static string PreBattlePath => Path.Combine( ThisPath, @"PreBattle.png");
		private static readonly Lazy<Bitmap> PreBattleBmpLazy = new Lazy<Bitmap>(()=> new Bitmap(PreBattlePath));
		public static Bitmap PreBattleBmp => PreBattleBmpLazy.Value;
	 
				
		public static string SurrenderPath => Path.Combine( ThisPath, @"Surrender.png");
		private static readonly Lazy<Bitmap> SurrenderBmpLazy = new Lazy<Bitmap>(()=> new Bitmap(SurrenderPath));
		public static Bitmap SurrenderBmp => SurrenderBmpLazy.Value;
	 
				}
			}
		

	public static class SimpleFind
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {SimpleFindBigBmp ,SimpleFindSmall1Bmp ,SimpleFindSmall2Bmp ,SimpleFindSmall3Bmp}; 
		public static string ThisPath = Path.Combine(Resources.ThisPath, @"SimpleFind");
			
		public static string SimpleFindBigPath => Path.Combine( ThisPath, @"SimpleFindBig.png");
		private static readonly Lazy<Bitmap> SimpleFindBigBmpLazy = new Lazy<Bitmap>(()=> new Bitmap(SimpleFindBigPath));
		public static Bitmap SimpleFindBigBmp => SimpleFindBigBmpLazy.Value;
	 
				
		public static string SimpleFindSmall1Path => Path.Combine( ThisPath, @"SimpleFindSmall1.png");
		private static readonly Lazy<Bitmap> SimpleFindSmall1BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(SimpleFindSmall1Path));
		public static Bitmap SimpleFindSmall1Bmp => SimpleFindSmall1BmpLazy.Value;
	 
				
		public static string SimpleFindSmall2Path => Path.Combine( ThisPath, @"SimpleFindSmall2.png");
		private static readonly Lazy<Bitmap> SimpleFindSmall2BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(SimpleFindSmall2Path));
		public static Bitmap SimpleFindSmall2Bmp => SimpleFindSmall2BmpLazy.Value;
	 
				
		public static string SimpleFindSmall3Path => Path.Combine( ThisPath, @"SimpleFindSmall3.png");
		private static readonly Lazy<Bitmap> SimpleFindSmall3BmpLazy = new Lazy<Bitmap>(()=> new Bitmap(SimpleFindSmall3Path));
		public static Bitmap SimpleFindSmall3Bmp => SimpleFindSmall3BmpLazy.Value;
	 
				}
		

	public static class Zoom
	{
		//public static IList<Bitmap> AllBitmaps = new List<Bitmap>() {falseBmp ,trueBmp}; 
		public static string ThisPath = Path.Combine(Resources.ThisPath, @"Zoom");
			
		public static string falsePath => Path.Combine( ThisPath, @"false.png");
		private static readonly Lazy<Bitmap> falseBmpLazy = new Lazy<Bitmap>(()=> new Bitmap(falsePath));
		public static Bitmap falseBmp => falseBmpLazy.Value;
	 
				
		public static string truePath => Path.Combine( ThisPath, @"true.png");
		private static readonly Lazy<Bitmap> trueBmpLazy = new Lazy<Bitmap>(()=> new Bitmap(truePath));
		public static Bitmap trueBmp => trueBmpLazy.Value;
	 
				}
			}
		}
