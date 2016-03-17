using System.Drawing;

namespace DominationsBot.Services.GameProcess
{
    public static class WindowStaticPositions
    {
        private static int ActionByttonsY = 660;

        public static class ActionButtons
        {
            private static int step = 135;
            private static int middle = 641;
            public static class ButtonsPositionsBy1
            {
                public static Point Middle = new Point(middle, ActionByttonsY);
                public static Point RightOne = new Point(middle + step, ActionByttonsY);
                public static Point LeftOne = new Point(middle - step, ActionByttonsY);
                public static Point RightTwo = new Point(RightTwo.X + step, ActionByttonsY);
                public static Point LeftTwo = new Point(LeftOne.X - step, ActionByttonsY);
            }

            public static class ButtonsPositionsBy2
            {
                public static Point RightOne = new Point(middle - step / 2, ActionByttonsY);
                public static Point RightTwo = new Point(RightOne.X + step, ActionByttonsY);
                public static Point LeftOne = new Point(middle - step / 2, ActionByttonsY);
                public static Point LeftTwo = new Point(LeftOne.X - step, ActionByttonsY);
            }
        }

        public static Rectangle GoldInfo = new Rectangle(0, 38, 170, 70);

        public static Point CommonCloseButton = new Point(1210, 90);

        public static Point SleepReloadGame = new Point(645, 465);

        public static class Barracks
        {
            public static Point TrainButton = ActionButtons.ButtonsPositionsBy2.RightOne;
            public static Point CloseButton = CommonCloseButton;
            public static Point TrainSpearmanButton = new Point(136, 485);
        }
    }
}
