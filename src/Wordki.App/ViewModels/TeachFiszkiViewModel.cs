﻿using Wordki.ViewModels.LessonStates;

namespace Wordki.ViewModels
{
    public class TeachFiszkiViewModel : TeachViewModelBase
    {
        public override void InitViewModel(object parameter = null)
        {
            base.InitViewModel();
            EnableTextBox = false;
            StateFactory = new FiszkiLessonStateFactory();
            State = StateFactory.GetState(Lesson, LessonStateEnum.BeforeStart);
            State.RefreshView();
        }

        public override void Loaded()
        {
            
        }

        public override void Unloaded()
        {
            
        }

        protected override void Check()
        {
            HintLetters = 0;
            Lesson.Check(State.Translation);
            State = StateFactory.GetState(Lesson, LessonStateEnum.Correct);
            State.RefreshView();

        }

    }
}
