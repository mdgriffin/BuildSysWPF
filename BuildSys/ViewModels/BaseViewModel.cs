
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    public abstract class BaseViewModel: ObservableObject
    {
        protected BaseViewModel parent;
        private static Stack<BaseViewModel> NavigationStack = new Stack<BaseViewModel>();

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

        public ICommand backCmd
        {
            get
            {
                return new RelayCommand(parent => navigateBack(), param => canNavigateBack());
            }
        }

        // TODO: Fix Navigation

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
