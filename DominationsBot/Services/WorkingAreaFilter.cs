using System.Collections.Generic;
using DominationsBot.Services.GameProcess;
using System.Drawing;
using System.Linq;

namespace DominationsBot.Services
{
    public interface IWorkingAreaFilter
    {
        bool IsInWorkingArea(Point point);
    }

    public class WorkingAreaFilter : IWorkingAreaFilter
    {

        private List<Rectangle> _filters = new List<Rectangle>()
        {
            WindowStaticPositions.MainScreen.Store,
            WindowStaticPositions.MainScreen.Battle,
            WindowStaticPositions.MainScreen.BasesAndGods,
            WindowStaticPositions.MainScreen.Menu,
            WindowStaticPositions.MainScreen.MedalsAndBuy,
            WindowStaticPositions.MainScreen.LevelAgeNickInfo,
            WindowStaticPositions.MainScreen.WorldWarButton,
            WindowStaticPositions.MainScreen.MessagesAndPeace,
            WindowStaticPositions.MainScreen.EventButton,
            WindowStaticPositions.MainScreen.GoalAndAchivements,
            WindowStaticPositions.MainScreen.ChatButton,
            WindowStaticPositions.MainScreen.CitizensInfo,
            WindowStaticPositions.MainScreen.CrownsInfo,
            WindowStaticPositions.MainScreen.FoodInfo,
            WindowStaticPositions.MainScreen.GoldInfo,
            WindowStaticPositions.MainScreen.GoldNumbers,
            WindowStaticPositions.MainScreen.FoodNumbers,
            WindowStaticPositions.EmulatorFooter,
            WindowStaticPositions.EmulatorHeader
        }; 
        public bool IsInWorkingArea(Point point)
        {
            return !_filters.Any(f => f.Contains(point));
        }
    }
    public class BattleWorkingAreaFilter:IWorkingAreaFilter
    {
        private List<Rectangle> _filters = new List<Rectangle>()
        {
            WindowStaticPositions.Battle.TroopsPanel,
            WindowStaticPositions.Battle.EndBattleAndMyMoney,
            WindowStaticPositions.Battle.NextMatchRect,
            WindowStaticPositions.Battle.OpponentInfo,
            WindowStaticPositions.EmulatorFooter,
            WindowStaticPositions.EmulatorHeader
        };
        public bool IsInWorkingArea(Point point)
        {
            return !_filters.Any(f => f.Contains(point));
        }
    }
}
