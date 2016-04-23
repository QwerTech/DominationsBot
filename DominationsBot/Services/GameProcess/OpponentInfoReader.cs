using DominationsBot.Extensions;
using DominationsBot.Services.ImageProcessing.TextReading;
using StructureMap.Attributes;

namespace DominationsBot.Services.GameProcess
{
    public class OpponentInfoReader
    {
        [SetterProperty]
        public NumberReader NumberReader { get; set; }

        [SetterProperty]
        public IScreenCapture ScreenCapture { get; set; }

        public OpponentInfo Read()
        {
            var snapShot = ScreenCapture.SnapShot();
            var opponentInfo = new OpponentInfo
            {
                Food = NumberReader.Read(snapShot.GetSubImage(WindowStaticPositions.Battle.OpponentFood),
                    NumberResourcesType.Food),
                Level = NumberReader.Read(
                    snapShot.GetSubImage(WindowStaticPositions.Battle.OpponentLevel), NumberResourcesType.Level),
                Gold = NumberReader.Read(snapShot.GetSubImage(WindowStaticPositions.Battle.OpponentGold),
                    NumberResourcesType.Gold)
            };
            
            return opponentInfo;
        }
    }
}