using System;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    public class DashboardTemplateSelector : DataTemplateSelector
    {
        #region Properties

        //General
        public DataTemplate TitleTemplate { get; set; }
        public DataTemplate LabelTemplate { get; set; }

        //Subscriptions
        public DataTemplate DurationSubscriptionTemplate { get; set; }
        public DataTemplate QuantitySubscriptionTemplate { get; set; }

        //Sessions
        public DataTemplate ClimbingSessionTemplate { get; set; }

        //Grades
        public DataTemplate ClimbingGradeTemplate { get; set; }

        #endregion

        #region Protected

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ICell cell)
            {
                if (cell is TitleCell)
                    return TitleTemplate;
                if (cell is LabelCell)
                    return LabelTemplate;

                if (cell is DurationSubscriptionDetail)
                    return DurationSubscriptionTemplate;
                if (cell is QuantitySubscriptionDetail)
                    return QuantitySubscriptionTemplate;

                if (cell is ClimbingSession)
                    return ClimbingSessionTemplate;

                if (cell is ClimbingGradeCell)
                    return ClimbingGradeTemplate;

                throw new ArgumentException($"{cell.GetType()} not setup in {nameof(DashboardTemplateSelector)}");
            }
            else
            {
                throw new ArgumentException($"{item.GetType()} does not inherit from {typeof(ICell)} {nameof(ICell)}");
            }
        }

        #endregion
    }
}
