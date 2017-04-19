
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

        public abstract BaseViewModel getInstance(BaseViewModel parent);

        Stack<BaseViewModel> NavigationStack;

        public BaseViewModel ()
        {
            NavigationStack = new Stack<BaseViewModel>();
        }

        public ICommand backCmd
        {
            get
            {
                return new RelayCommand(parent => navigateBack(), param => canNavigateBack());
            }
        }

        public  void addToStack(BaseViewModel vm)
        {
            NavigationStack.Push(vm);
        }

        public BaseViewModel removeFromStack ()
        {
            return NavigationStack.Pop();
        }

        public void navigateTo (BaseViewModel vm)
        {
            addToStack(vm);
            parent.ViewModel = vm;
        }

        public void navigateBack ()
        {
            if (canNavigateBack())
            {
                parent.ViewModel = removeFromStack();
            }
        }

        public Boolean canNavigateBack()
        {
            return NavigationStack.Count > 0;
        }
    }
}
