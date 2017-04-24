using BuildSys.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    public class MaterialManageViewModel : BaseViewModel
    {
        //MainViewModel parent;

        public MaterialManageViewModel(BaseViewModel parent)
        {
            this.parent = parent;

            materialList = MaterialModel.getMaterialList();
            deletedMaterialList = MaterialModel.getDeletedMaterialList();

            // Keep a copy of the materialList so that we can restore the list after filtering
            originalMaterialList = new ObservableCollection<MaterialModel>(materialList);
        }

        private String _materialFilter = "";
        public String materialFilter
        {
            get
            {
                return _materialFilter;
            }
            set
            {
                if (value != _materialFilter)
                {
                    _materialFilter = value;
                    filterMaterials();
                }
            }
        }

        public ObservableCollection<MaterialModel> deletedMaterialList { get; set; }

        private ObservableCollection<MaterialModel> originalMaterialList;

        private ObservableCollection<MaterialModel> _materialList = new ObservableCollection<MaterialModel>();
        public ObservableCollection<MaterialModel> materialList
        {
            get { return _materialList ?? (_materialList = new ObservableCollection<MaterialModel>()); }
            set
            {
                if (value == null) return;
                _materialList = value;
                NotifyPropertyChanged("materialList");
            }
        }

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

        public ICommand restoreMaterialCmd
        {
            get
            {
                return new RelayCommand((materialId) => restoreMaterial((int)materialId), param => true);
            }
        }

        public void editMaterial(int materialId)
        {
            navigateTo(new MaterialFormViewModel(parent, materialId));
        }

        public void deleteMaterial(int materialId)
        {
            MaterialModel.deleteMaterial(materialId);

            MaterialModel deletedMaterial = materialList.Where(mat => mat.materialId == materialId).First();

            materialList.Remove(deletedMaterial);
            originalMaterialList.Remove(deletedMaterial);

            deletedMaterialList.Add(deletedMaterial);
        }

        public void restoreMaterial(int materialId)
        {
            MaterialModel.restoreMaterial(materialId);

            MaterialModel restoredMaterial = deletedMaterialList.Where(mat => mat.materialId == materialId).First();
            deletedMaterialList.Remove(restoredMaterial);

            materialList.Add(restoredMaterial);
            originalMaterialList.Add(restoredMaterial);

            filterMaterials();
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

    }

}
