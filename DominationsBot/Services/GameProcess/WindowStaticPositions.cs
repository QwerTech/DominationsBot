using System.Drawing;
using AForge.Math.Geometry;

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

        public static Rectangle GoldInfo = new Rectangle(0, 38, 170, 70);

        public static Point CommonCloseButton = new Point(1210, 90);

        public static class SleepingDialog
        {
            public static LineSegment CheckDialogLine = new LineSegment(new AForge.Point(485,510),new AForge.Point(1130,510) );
            public static Point SleepReloadGame = new Point(830, 550);
            public static Color DialogBackground =  Color.FromArgb(40,40,40);
        }
        public static class MainScreen
        {
            public static Rectangle GoldNumbers = new Rectangle(75,46,145,26);
            public static Rectangle FoodNumbers = new Rectangle(289,46,145,26);
        }
        public static Point ZoomingButton = new Point(1720,500);

        public static class Barracks
        {
            public static Point TrainButton = ActionButtons.ButtonsPositionsBy2.RightOne;
            public static Point CloseButton = CommonCloseButton;
            public static Point TrainSpearmanButton = new Point(136, 485);
        }
    }
}
