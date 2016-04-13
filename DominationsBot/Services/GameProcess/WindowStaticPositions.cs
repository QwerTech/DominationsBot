using AForge.Math.Geometry;
using System.Drawing;

namespace DominationsBot.Services.GameProcess
{
    public static class WindowStaticPositions
    {
        private static readonly int ActionByttonsY = 660;

        public static class ActionButtons
        {
            private static readonly int Step = 135;
            private static readonly int Middle = 641;
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



        public static Point CommonCloseButton = new Point(1210, 90);

        public static class SleepingDialog
        {
            public static LineSegment CheckDialogLine = new LineSegment(new AForge.Point(485, 510), new AForge.Point(1130, 510));
            public static Point SleepReloadGame = new Point(825, 530);
            public static Color DialogBackground = Color.FromArgb(40, 40, 40);
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
            public static Rectangle EventButton = new Rectangle(105, 197, 104, 197);
            public static Rectangle GoalAndAchivements = new Rectangle(0, 131, 118, 21);
            public static Rectangle ChatButton = new Rectangle(0, 377, 67, 163);
            public static Rectangle GoldInfo = new Rectangle(0, 38, 170, 70);
            public static Rectangle GoldNumbers = new Rectangle(75, 46, 145, 26);
            public static Rectangle FoodNumbers = new Rectangle(289, 46, 145, 26);
        }
        public static Point ZoomingButton = new Point(1630, 475);

        public static class Barracks
        {
            public static Point TrainButton = ActionButtons.ButtonsPositionsBy2.RightOne;
            public static Point CloseButton = CommonCloseButton;
            public static Point TrainSpearmanButton = new Point(136, 485);
        }
    }
}
