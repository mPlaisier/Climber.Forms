namespace Climber.Forms.Core
{
    public class TitleCell : ICell
    {
        #region Properties

        public string Title { get; }

        #endregion

        #region Constructor

        public TitleCell(string title)
        {
            Title = title;
        }

        #endregion
    }
}