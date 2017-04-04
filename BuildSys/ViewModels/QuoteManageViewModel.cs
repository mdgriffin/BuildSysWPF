using BuildSys.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys.ViewModels
{
    class QuoteManageViewModel: BaseViewModel
    {

        MainViewModel parent;

        public QuoteManageViewModel(MainViewModel parent)
        {
            this.parent = parent;

            quoteList = QuoteModel.getQuoteList();

            // Keep a copy of the materialList so that we can restore the list after filtering
            originaQuoteList = new ObservableCollection<QuoteModel>(quoteList);


        }
        private String _quoteFilter;
        public String quoteFilter
        {
            get
            {
                return _quoteFilter;
            }
            set
            {
                if (value != _quoteFilter)
                {
                    _quoteFilter = value;
                    //filterQuotes();
                }
            }
        }

        private ObservableCollection<QuoteModel> originaQuoteList;

        private ObservableCollection<QuoteModel> _quoteList = new ObservableCollection<QuoteModel>();
        public ObservableCollection<QuoteModel> quoteList
        {
            get
            {
                return _quoteList ?? (_quoteList = new ObservableCollection<QuoteModel>());
            }
            set
            {
                if (value == null) return;
                _quoteList = value;
                NotifyPropertyChanged("quoteList");
            }
        }

        /*
        public ICommand editMaterialCmd
        {
            get
            {
                return new RelayCommand((materialId) => editMaterial((int)materialId), param => true);
            }
        }

        public ICommand deleteMaterialCmd
        {
            get
            {
                return new RelayCommand((materialId) => deleteMaterial((int)materialId), param => true);
            }
        }

        public void editMaterial(int materialId)
        {
            parent.ViewModel = new MaterialFormViewModel(parent, materialId);
        }

        public void deleteMaterial(int materialId)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                MaterialModel.deleteMaterial(materialId);
                MessageBox.Show("Material Deleted");
                materialList.Where(mat => mat.materialId == materialId).ToList().All(i => materialList.Remove(i));
                originalMaterialList.Where(mat => mat.materialId == materialId).ToList().All(i => materialList.Remove(i));
            }
        }

        public void filterMaterials()
        {
            materialList = new ObservableCollection<MaterialModel>(originalMaterialList);
            Regex matchName = new Regex(@"^" + materialFilter + @".+", RegexOptions.IgnoreCase);

            if (materialFilter.Length > 0)
            {
                materialList.Where(mat => !matchName.IsMatch(mat.name))
                    .ToList()
                    .All(i => materialList.Remove(i));
            }
        }
        */
    }
}
