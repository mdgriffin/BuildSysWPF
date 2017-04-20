
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    public abstract class BaseViewModel: ObservableObject
    {
        protected BaseViewModel parent;

        private BaseViewModel _ViewModel;
        public BaseViewModel ViewModel
        {
            get
            {
                return _ViewModel;
            }
            set
            {
                _ViewModel = value;
                NotifyPropertyChanged("ViewModel");
            }
        }

        private static Stack<BaseViewModel> NavigationStack = new Stack<BaseViewModel>();

        public ICommand backCmd
        {
            get
            {
                return new RelayCommand(parent => navigateBack(), param => canNavigateBack());
            }
        }

        public void navigateTo (BaseViewModel vm)
        {
            NavigationStack.Push(vm);
            parent.ViewModel = vm;
        }

        public void navigateBack ()
        {
            if (canNavigateBack())
            {
                NavigationStack.Pop();
                parent.ViewModel = NavigationStack.Pop();
            }
        }

        public Boolean canNavigateBack()
        {
            return NavigationStack.Count > 1;
        }
    }
}
