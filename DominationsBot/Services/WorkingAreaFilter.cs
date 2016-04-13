using DominationsBot.Services.GameProcess;
using System.Drawing;

namespace DominationsBot.Services
{
    public class WorkingAreaFilter
    {

        public bool IsInWorkingArea(Point point)
        {
            return !WindowStaticPositions.MainScreen.GoldInfo.Contains(point);
        }
    }
}
