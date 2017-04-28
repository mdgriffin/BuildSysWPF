
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    /*
     * Base View Model from which all view models are extended
     */

    public abstract class BaseViewModel: ObservableObject
    {
        // A reference to the parent of the view model
        protected BaseViewModel parent;
        // A stack to store the navigation through the app and allow traversing back
        private static Stack<BaseViewModel> NavigationStack = new Stack<BaseViewModel>(5);

        // The current viewModel that is disaplayed to the user
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

        // Command that is called when the back button is pressed
        public ICommand backCmd
        {
            get
            {
                return new RelayCommand(parent => navigateBack(), param => canNavigateBack());
            }
        }

        // Method to nagivate to a specified view model
        public void navigateTo (BaseViewModel vm)
        {
            NavigationStack.Push(vm);
            parent.ViewModel = vm;
        }

        // Pops from the the stack, navigating to the previous view model
        public void navigateBack ()
        {
            if (canNavigateBack())
            {
                NavigationStack.Pop();
                parent.ViewModel = NavigationStack.Peek();
            }
        }

        // Check to see if the back button should be enabled
        public Boolean canNavigateBack()
        {
            return NavigationStack.Count > 1;
        }
    }
}
