namespace Climber.Forms.Core
{
    public class LabelCell : ICell
    {
        #region Properties

        public string Label { get; }

        #endregion

        #region Constructor

        public LabelCell(string label)
        {
            Label = label;
        }

        #endregion
    }
}