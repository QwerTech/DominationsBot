using System;
using AForge.Math.Geometry;
using Castle.DynamicProxy.Internal;
using DominationsBot.Extensions;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace DominationsBot.Services.GameProcess
{
    public static class WindowStaticPositions
    {
        private static readonly int ActionByttonsY = 825;

        public static class ActionButtons
        {
            private static readonly int Step = 167;
            private static readonly int Middle = Dimensions.Width/2;
            public static class ButtonsPositionsBy1
            {
                public static Point Middle = new Point(ActionButtons.Middle, ActionByttonsY);
                public static Point RightOne = new Point(ActionButtons.Middle + Step, ActionByttonsY);
                public static Point LeftOne = new Point(ActionButtons.Middle - Step, ActionByttonsY);
                public static Point RightTwo = new Point(RightTwo.X + Step, ActionByttonsY);
                public static Point LeftTwo = new Point(LeftOne.X - Step, ActionByttonsY);
            }

            public static class ButtonsPositionsBy2
            {
                public static Point RightOne = new Point(Middle - Step / 2, ActionByttonsY);
                public static Point RightTwo = new Point(RightOne.X + Step, ActionByttonsY);
                public static Point LeftOne = new Point(Middle - Step / 2, ActionByttonsY);
                public static Point LeftTwo = new Point(LeftOne.X - Step, ActionByttonsY);
            }
        }



        public static Point CommonCloseButton = new Point(1560, 90);

        public static class SleepingDialog
        {
            public static LineSegment CheckDialogLine = new LineSegment(new AForge.Point(485, 510), new AForge.Point(1130, 510));
            public static Point SleepReloadGame = new Point(825, 530);
            public static Color DialogBackground = Color.FromArgb(40, 40, 40);
        }
        public static Rectangle EmulatorHeader= new Rectangle(0,0,1020,15);
        public static Rectangle EmulatorFooter = new Rectangle(0, 954, 1020, 66);
        public static Rectangle Dimensions = new Rectangle(0, 0, 1649, 1020 );

        public static class Battle
        {
            public static Rectangle EndBattle = new Rectangle(23, 118, 209, 53);
            public static Point FindOpponent = new Point(810,300);
            public static Point NextMatch = new Point(1515, 690);
            public static Rectangle OpponentAge= new Rectangle(1435, 80, 146, 26);
            public static Rectangle OpponentGold = new Rectangle(1434, 174,144,30);
            public static Rectangle OpponentLevel = new Rectangle(1581, 58, 53, 30);
            public static Rectangle OpponentFood = new Rectangle(1434, 221, 144, 30);

        }
        public static class MainScreen
        {
            public static Rectangle Store = new Rectangle(1488, 781, 161, 177);
            public static Rectangle Battle = new Rectangle(0, 785, 152, 168);
            public static Rectangle BasesAndGods = new Rectangle(1529, 542, 120, 234);
            public static Rectangle Menu = new Rectangle(1583, 376, 66, 169);
            public static Rectangle MedalsAndBuy = new Rectangle(1532, 129, 117, 248);
            public static Rectangle LevelAgeNickInfo = new Rectangle(1265, 32, 384, 101);
            public static Rectangle WorldWarButton = new Rectangle(130, 704, 90, 92);
            public static Rectangle MessagesAndPeace = new Rectangle(0, 536, 122, 240);
            public static Rectangle EventButton = new Rectangle(105, 197, 104, 111);
            public static Rectangle GoalAndAchivements = new Rectangle(0, 131, 118, 244);
            public static Rectangle ChatButton = new Rectangle(0, 377, 67, 163);
            public static Rectangle CitizensInfo = new Rectangle(587, 38, 128, 64);
            public static Rectangle CrownsInfo = new Rectangle(442, 38, 114, 64);
            public static Rectangle FoodInfo = new Rectangle(228, 38, 225, 84);
            public static Rectangle GoldInfo = new Rectangle(0, 38, 225, 84);
            public static Rectangle GoldNumbers = new Rectangle(75, 46, 145, 26);
            public static Rectangle FoodNumbers = new Rectangle(289, 46, 145, 26);
        }
        
        public static Rectangle ZoomingButtonRect = new Rectangle(1621, 466,22,21);
        public static Point ZoomingButton = ZoomingButtonRect.Middle();
        public static class Barracks
        {
            public static Point TrainButton = ActionButtons.ButtonsPositionsBy2.RightOne;
            public static Point CloseButton = CommonCloseButton;
            public static Point TrainSpearmanButton = new Point(170, 605);
        }

        public static void View(Bitmap bitmapToDraw, Type type = null,string className = null)
        {
            string text = string.IsNullOrWhiteSpace(className) ? string.Empty : className + ".";
             type = type?? typeof(WindowStaticPositions);
            var nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);
            foreach (var nestedType in nestedTypes)
            {
                View(bitmapToDraw,nestedType, text+nestedType.Name);
            }
            var fieldInfos = type.GetAllFields().Where(f => f.IsStatic && f.IsPublic);
            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.FieldType == typeof(Rectangle))
                {
                    var rectangle = (Rectangle)fieldInfo.GetValue(null);
                    bitmapToDraw.DrawString(text + fieldInfo.Name, rectangle.Location);
                    bitmapToDraw.DrawRectangle(rectangle);
                }
                if (fieldInfo.FieldType == typeof(Point))
                {
                    var point = (Point)fieldInfo.GetValue(null);
                    bitmapToDraw.DrawString(text + fieldInfo.Name, point);
                    bitmapToDraw.DrawPointPosition(point);
                }
            }

        }
    }
}
